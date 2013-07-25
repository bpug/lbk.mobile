//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="AppStart.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ApplicationObjects
{
    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Core.ViewModels.Home;

    public class AppStart : MvxNavigatingObject, IMvxAppStart
    {
        private readonly bool showSplashScreen;

        public AppStart(bool showSplashScreen = true)
        {
            this.showSplashScreen = showSplashScreen;
        }

        public bool ApplicationCanOpenBookmarks
        {
            get
            {
                return true;
            }
        }

        public void Start(object hint = null)
        {
            if (this.showSplashScreen)
            {
                this.ShowViewModel<SplashScreenViewModel>();
            }
            else
            {
                this.ShowViewModel<HomeViewModel>();
            }
        }
    }
}