//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MvxAndroidDeviceIdentifier.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Plugin.DeviceIdentifier.Droid
{
    using System;

    using Android.Provider;

    using Cirrious.CrossCore;
    using Cirrious.CrossCore.Droid;

    public class MvxAndroidDeviceIdentifier : IDeviceUidService
    {
        public void GetDeviceUid(Action<string> onSuccess, Action<Exception> onError)
        {
            try
            {
                var uid = this.GetDeviceUid();
                onSuccess(uid);
            }
            catch (Exception ex)
            {
                onError(ex);
            }
        }

        public string GetDeviceUid()
        {
            var globals = Mvx.Resolve<IMvxAndroidGlobals>();
            var uid = Settings.Secure.GetString(
                globals.ApplicationContext.ContentResolver,
                Settings.Secure.AndroidId);
            return uid;
        }
    }
}