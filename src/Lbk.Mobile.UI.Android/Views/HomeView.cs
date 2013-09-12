//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="HomeView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Android.Views
{
    using Cirrious.MvvmCross.Droid.Views;

    using global::Android.App;
    using global::Android.OS;

    using Lbk.Mobile.Core.ViewModels.Home;

    [Activity(Label = "L�wenbr�ukeller", Icon = "@drawable/ic_launcher")]
    public class HomeView : BaseView<HomeViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Home);
        }
    }
}