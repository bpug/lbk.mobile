//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LbkApp.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core
{
    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Core.ApplicationObjects;

    public class LbkApp : LbkAppBase
    {
        public LbkApp()
        {
            this.InitialiseStartNavigation();
        }

        protected override sealed void InitialiseStartNavigation()
        {
            var startApplicationObject = new AppStart(true);
            Mvx.RegisterSingleton<IMvxAppStart>(startApplicationObject);
        }
    }
}