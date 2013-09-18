//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TodaysMenuListAdapter.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.TodaysMenu
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;

    using Cirrious.MvvmCross.Binding.Droid.BindingContext;
    using Cirrious.MvvmCross.Binding.Droid.Views;

    using Android.Content;
    using Android.Views;
    using Android.Widget;

    using Lbk.Mobile.Model;
    using Lbk.Mobile.UI.Droid.Controls;

    using Object = Java.Lang.Object;
    using String = Java.Lang.String;

    public class TodaysMenuListAdapter : MvxAdapter, ISectionIndexer
    {

        private List<Model.MenuCategory> categories;
        
        private List<int> reverseSectionLookup;

        private Object[] objSectionHeaders;

        private List<int> sectionLookup;

        public TodaysMenuListAdapter(Context context, IMvxAndroidBindingContext bindingContext)
            : base(context, bindingContext)
        {
        }

        public int GetPositionForSection(int section)
        {
            if (this.sectionLookup == null)
            {
                return 0;
            }

            return this.sectionLookup[section];
        }

        public int GetSectionForPosition(int position)
        {
            if (this.reverseSectionLookup == null)
            {
                return 0;
            }

            return this.reverseSectionLookup[position];
        }

        public Object[] GetSections()
        {
            return this.objSectionHeaders;
        }


        private void SetRoundedCorners(Dish dish, ref View view)
        {
            var info = this.GetDishInfo(dish);

            if (info.Index == 0 && info.Count == 1)
            {
                view.SetBackgroundResource(Resource.Drawable.runde_ecken);
            }
            else if (info.Index == 0)
            {
                view.SetBackgroundResource(Resource.Drawable.list_rounded_item_top);
            }
            else if (info.Index == info.Count - 1)
            {
                view.SetBackgroundResource(Resource.Drawable.runde_ecken_bottom);
            }
            else
            {
                view.SetBackgroundResource(Resource.Drawable.list_item_middle);
            }
        }

        protected override View GetBindableView(View convertView, object dataContext, int templateId)
        {
            if (dataContext is Dish)
            {
                var view =  base.GetBindableView(convertView, dataContext, Resource.Layout.TodaysMenu_ListItem);
                SetRoundedCorners(dataContext as Dish, ref view);
                return view;
            }
            if (dataContext is SectionFooter)
            {
                return base.GetBindableView(convertView, dataContext, Resource.Layout.TodaysMenu_ListItem_Footer);
            }
            else
            {
                return base.GetBindableView(convertView, dataContext, Resource.Layout.TodaysMenu_ListItem_Header);
            }
        }


        private DishInfo GetDishInfo(Dish dish)
        {
            foreach (var category in categories)
            {
                var index = category.Dishes.FindIndex(d => d.Equals(dish));
                if (index > -1)
                {
                    return new DishInfo
                    {
                        Index = index,
                        Count = category.Dishes.Count
                    };
                }
            }
            return null;
        }

        private class DishInfo
        {
            public int Index { get; set; }
            public int Count { get; set; }
        }

        protected override void SetItemsSource(IEnumerable list)
        {
            categories = list as List<Model.MenuCategory>;

            
            if (categories == null)
            {
                this.objSectionHeaders = null;
                this.sectionLookup = null;
                this.reverseSectionLookup = null;
                base.SetItemsSource(null);
                return;
            }

            var flattened = new List<object>();
            this.sectionLookup = new List<int>();
            this.reverseSectionLookup = new List<int>();
            var sectionHeaders = new List<string>();

            int categoryCounter = 0;
            foreach (var category in categories)
            {
                this.sectionLookup.Add(flattened.Count);
                string groupHeader = this.GetGroupHeader(category);
                sectionHeaders.Add(groupHeader);

                var groupFooter = this.GetGroupFooter(category);

                for (var i = 0; i <= category.Dishes.Count; i++)
                {
                    this.reverseSectionLookup.Add(categoryCounter);
                }

                flattened.Add(groupHeader);
                flattened.AddRange(category.Dishes.Select(x => (object)x));
                flattened.Add(groupFooter);

                categoryCounter++;
            }

            this.objSectionHeaders =
                CreateJavaStringArray(sectionHeaders.Select(x => x).ToList());

            base.SetItemsSource(flattened);
        }

        private static Object[] CreateJavaStringArray(IReadOnlyList<string> inputList)
        {
            if (inputList == null)
            {
                return null;
            }

            var toReturn = new Object[inputList.Count];
            for (int i = 0; i < inputList.Count; i++)
            {
                toReturn[i] = new String(inputList[i]);
            }

            return toReturn;
        }

        private SectionFooter GetGroupFooter(Model.MenuCategory category)
        {
            return new SectionFooter
            {
                Footer = category.Subtitle
            };
        }

        private string GetGroupHeader(Model.MenuCategory category)
        {
            return category.Title;
        }
    }
}