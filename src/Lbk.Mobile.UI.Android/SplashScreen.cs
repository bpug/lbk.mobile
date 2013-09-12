//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="SplashScreen.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Android
{
    using Cirrious.MvvmCross.Droid.Views;

    using global::Android.App;
    using global::Android.Content.PM;

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