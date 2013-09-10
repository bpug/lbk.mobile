//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TodaysMenuView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using Android.App;
using Android.OS;


namespace Lbk.Mobile.UI.Android.Views
{
    using System.Collections.Generic;
    using System.Linq;

    using Cirrious.MvvmCross.Binding.Droid.BindingContext;
    using Cirrious.MvvmCross.Binding.Droid.Views;

    using Lbk.Mobile.Core.ViewModels.TodaysMenu;
    using Lbk.Mobile.Data.LbkMobileService;

    [Activity(Label = "Tageskarte")]
    public class TodaysMenuView : BaseView<TodaysMenuViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            this.Title = DateTime.Now.ToShortDateString();
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.TodaysMenu);

            //Find our list and set its adapter
            var sessionListView = FindViewById<MvxListView>(global::Android.Resource.Id.List);
            sessionListView.Adapter = new TodaysMenuListAdapter(this, (IMvxAndroidBindingContext)BindingContext);

            //ViewModel.PropertyChanged += (sender, args) =>
            //{
            //    var vm = (TodaysMenuViewModel)sender;
            //    switch (args.PropertyName)
            //    {
            //        case "MenuCategories":
            //            BindDishes(vm.MenuCategories);
            //            break;
            //    }
            //};
            
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