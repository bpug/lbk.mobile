using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;
using Lbk.Mobile.Plugin.DeviceIdentifier;


namespace Lbk.Mobile.Plugin.DeviceIdentifier.Droid
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.RegisterSingleton<IDeviceUidService>(new MvxAndroidDeviceIdentifier());
        }
    }
}
