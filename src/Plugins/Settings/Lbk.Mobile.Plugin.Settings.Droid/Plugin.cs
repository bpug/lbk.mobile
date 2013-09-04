using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;

namespace Lbk.Mobile.Plugin.Settings.Droid
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.RegisterSingleton<ISettings>(new MvxAndroidSettings());
        }
    }
}
