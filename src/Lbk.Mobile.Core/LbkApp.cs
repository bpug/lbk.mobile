//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LbkApp.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core
{
    using Cirrious.CrossCore;
    using Cirrious.CrossCore.IoC;
    using Cirrious.MvvmCross.Localization;
    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Core.ApplicationObjects;
    using Lbk.Mobile.Core.Services;
    using Lbk.Mobile.Core.Services.Error;
    using Lbk.Mobile.Core.ViewModels.Home;
    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Data.Services;

    public class LbkApp : LbkAppBase
    {
        public LbkApp()
        {
            this.InitialiseStartNavigation();
        }

        protected override sealed void InitialiseStartNavigation()
        {
            var startApplicationObject = new AppStart(false);
            Mvx.RegisterSingleton<IMvxAppStart>(startApplicationObject);
        }
    }
    
}