//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TodaysMenuView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Android.Views
{
    using System;

    using Cirrious.MvvmCross.Binding.Droid.BindingContext;
    using Cirrious.MvvmCross.Binding.Droid.Views;

    using global::Android;
    using global::Android.App;
    using global::Android.OS;

    using Lbk.Mobile.Core.ViewModels.TodaysMenu;

    [Activity(Label = "Tageskarte")]
    public class TodaysMenuView : BaseView<TodaysMenuViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            this.Title = DateTime.Now.ToShortDateString();
            base.OnCreate(bundle);
            this.SetContentView(Android.Resource.Layout.TodaysMenu);

            //Find our list and set its adapter
            var sessionListView = this.FindViewById<MvxListView>(Resource.Id.List);
            sessionListView.Adapter = new TodaysMenuListAdapter(this, (IMvxAndroidBindingContext)this.BindingContext);
        }

        //private void BindDishes(IEnumerable<category> categories)
        //{
        //    //var bindings = this.CreateInlineBindingTarget<TodaysMenuViewModel>();
        //    var root = new RootElement(Title){
        //        from cat in categories
        //            select new Section (cat.Title, cat.Subtitle){
        //                from  dish in cat.Dishes select (Element)new StringElement(dish.Description)
        //            }
        //    };
        //    Root = root;
        //}
    }
}