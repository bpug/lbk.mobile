//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="PluginLoader.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Cirrious.MvvmCross.Plugins.DeviceIdentifier
{
    using Cirrious.CrossCore;
    using Cirrious.CrossCore.Plugins;

    public class PluginLoader : IMvxPluginLoader
    {
        public static readonly PluginLoader Instance = new PluginLoader();

        static PluginLoader()
        {
        }

        public void EnsureLoaded()
        {
            Mvx.Resolve<IMvxPluginManager>().EnsurePlatformAdaptionLoaded<PluginLoader>();
        }
    }
}