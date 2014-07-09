//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="HomeView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Test.Views
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;

    using Cirrious.MvvmCross.Droid.Views;

    using Lbk.Mobile.Core.ViewModels.Home;
    using Lbk.Mobile.UI.Droid.Test;

    [Activity(Label = "Löwenbräukeller",  ScreenOrientation = ScreenOrientation.Portrait)]
    public class HomeView : BaseView<HomeViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Home);
        }
    }
}