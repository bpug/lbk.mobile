//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Plugin.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Plugin.UserInteraction.Droid
{
    using Cirrious.CrossCore;
    using Cirrious.CrossCore.Plugins;

    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.RegisterSingleton<IMessageBoxService>(new MessageBoxService());
        }
    }
}