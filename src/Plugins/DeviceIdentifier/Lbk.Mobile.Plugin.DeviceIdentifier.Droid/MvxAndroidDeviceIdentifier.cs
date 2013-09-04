using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Lbk.Mobile.Plugin.DeviceIdentifier;
using Android.Provider;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid;


namespace Lbk.Mobile.Plugin.DeviceIdentifier.Droid
{
    public class MvxAndroidDeviceIdentifier : IDeviceUidService
    {
    #region IDeviceUidService Members

    public void GetDeviceUid(Action<string> onSuccess, Action<Exception> onError)
    {
        string uid;
        try
        {
            uid = GetDeviceUid();
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
        var uid = Settings.Secure.GetString(globals.ApplicationContext.ContentResolver, Settings.Secure.AndroidId);
        return uid;
    }

    #endregion
    }
}