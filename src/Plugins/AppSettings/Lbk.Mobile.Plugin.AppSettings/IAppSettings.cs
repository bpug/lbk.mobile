//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IAppSettings.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Plugin.AppSettings
{
    public interface IAppSettings
    {
        string MenuFilePath { get; }
        string SharedRootFolder { get; }
    }
}