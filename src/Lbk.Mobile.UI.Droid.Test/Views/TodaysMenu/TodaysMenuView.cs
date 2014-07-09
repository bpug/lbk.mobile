//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TodaysMenuView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Test.Views.TodaysMenu
{
    using System;

    using Android.App;
    using Android.OS;

    using Cirrious.MvvmCross.Binding.Droid.BindingContext;
    using Cirrious.MvvmCross.Binding.Droid.Views;

    using Lbk.Mobile.Core.ViewModels.TodaysMenu;

    [Activity(Label = "Tageskarte", Icon = "@drawable/ic_launcher")]
    public class TodaysMenuView : BaseView<TodaysMenuViewModel>
    {

       
        protected override void OnCreate(Bundle bundle)
        {
           base.OnCreate(bundle);

           this.Title = DateTime.Now.ToShortDateString();
            this.SetContentView(Resource.Layout.TodaysMenu_ListView);

            //var sessionListView = this.FindViewById<MvxListView>(Resource.Id.todaysmenu_list);
            //sessionListView.Adapter = new TodaysMenuListAdapter(this, (IMvxAndroidBindingContext)this.BindingContext);
        }
    }
}