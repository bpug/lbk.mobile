//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TodaysMenuListAdapter.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Android.Views
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Cirrious.MvvmCross.Binding.Droid.BindingContext;
    using Cirrious.MvvmCross.Binding.Droid.Views;

    using global::Android.Content;
    using global::Android.Views;
    using global::Android.Widget;

    using Java.Lang;

    using Lbk.Mobile.Data.LbkMobileService;
    using Lbk.Mobile.UI.Android.Controls;

    public class TodaysMenuListAdapter : MvxAdapter, ISectionIndexer
    {
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

        protected override View GetBindableView(View convertView, object dataContext, int templateId)
        {
            if (dataContext is dish)
            {
                return base.GetBindableView(convertView, dataContext, Resource.Layout.TodaysMenu_ListItem);
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

        protected override void SetItemsSource(IEnumerable list)
        {
            var groupedList = list as List<category>;

            if (groupedList == null)
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

            int groupsSoFar = 0;
            foreach (var group in groupedList)
            {
                this.sectionLookup.Add(flattened.Count);
                string groupHeader = this.GetGroupHeader(group);
                sectionHeaders.Add(groupHeader);

                var groupFooter = this.GetGroupFooter(group);

                for (int i = 0; i <= group.Dishes.Length; i++)
                {
                    this.reverseSectionLookup.Add(groupsSoFar);
                }

                flattened.Add(groupHeader);
                flattened.AddRange(group.Dishes.Select(x => (object)x));
                flattened.Add(groupFooter);

                groupsSoFar++;
            }

            this.objSectionHeaders =
                CreateJavaStringArray(sectionHeaders.Select(x => x.Length > 10 ? x.Substring(0, 10) : x).ToList());

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

        private SectionFooter GetGroupFooter(category category)
        {
            return new SectionFooter
            {
                Footer = category.Subtitle
            };
        }

        private string GetGroupHeader(category category)
        {
            return category.Title;
        }
    }
}