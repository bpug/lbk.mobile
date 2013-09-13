//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="SplashScreen.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid
{
    using Cirrious.MvvmCross.Droid.Views;

    using Android.App;
    using Android.Content.PM;

    [Activity(Label = "Löwenbräukeller", MainLauncher = true, Icon = "@drawable/ic_launcher",
        Theme = "@style/Theme.Splash", NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}