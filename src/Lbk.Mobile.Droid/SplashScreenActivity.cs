// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SplashScreenActivity.cs" company="ip-connect GmbH">
//   Copyright (c) ip-connect GmbH. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid
{
    using Android.App;

    using Cirrious.MvvmCross.Droid.Views;

    [Activity(Label = "Löwenbräukeller", MainLauncher = true, NoHistory = true, Icon = "@drawable/icon")]
    public class SplashScreenActivity : MvxBaseSplashScreenActivity
    {
        public SplashScreenActivity()
            : base(Resource.Layout.SplashScreen)
        {
        }

        protected override void OnViewModelSet()
        {
            // ignored
        }
    }
}