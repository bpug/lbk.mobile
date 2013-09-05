//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Setup.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Android
{
    using Cirrious.CrossCore.Platform;
    using Cirrious.MvvmCross.Droid.Platform;
    using Cirrious.MvvmCross.ViewModels;

    using global::Android.Content;

    using Lbk.Mobile.Core;

    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new LbkApp();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override void InitializeLastChance()
        {
            var errorDisplayer = new ErrorDisplayer(base.ApplicationContext);
            base.InitializeLastChance();
        }
    }
}