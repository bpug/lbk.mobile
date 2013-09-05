//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MvxReachability.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Plugin.Reachability.Droid
{
    using Android.App;
    using Android.Net;

    using Cirrious.CrossCore;
    using Cirrious.CrossCore.Droid;
    

    public class MvxReachability : IReachability
    {
        private ConnectivityManager connectivityManager;

        protected ConnectivityManager ConnectivityManager
        {
            get
            {
                return this.connectivityManager
                       ?? (this.connectivityManager =
                           Mvx.Resolve<IMvxAndroidGlobals>()
                               .ApplicationContext.GetSystemService(Application.ConnectivityService) as
                               ConnectivityManager);
            }
        }

        public bool IsHostReachable(string host)
        {
            var activeNetwork = this.ConnectivityManager.ActiveNetworkInfo;
            return activeNetwork != null && activeNetwork.IsConnected;
        }
    }
}