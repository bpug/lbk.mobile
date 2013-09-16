//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="PluginLoader.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Plugin.DocumentViewer
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
            var manager = Mvx.Resolve<IMvxPluginManager>();
            manager.EnsurePlatformAdaptionLoaded<PluginLoader>();
        }
    }
}