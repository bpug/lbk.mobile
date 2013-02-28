// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StartApplication.cs" company="ip-connect GmbH">
//   Copyright (c) ip-connect GmbH. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Portable.Core.ApplicationObjects
{
    using Cirrious.MvvmCross.Interfaces.ViewModels;
    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Portable.Core.ViewModels;

    public class StartApplication : MvxApplicationObject, IMvxStartNavigation
    {
        public bool ApplicationCanOpenBookmarks
        {
            get
            {
                return true;
            }
        }

        public void Start()
        {
            this.RequestNavigate<HomeViewModel>();
        }
    }
}