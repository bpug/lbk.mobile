//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ISettings.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Plugin.Settings
{
    using System;
    using System.Collections.Generic;

    public interface ISettings
    {
        bool AddOrUpdateValue(string key, Object value);

        T GetValueOrDefault<T>(string key, T defaultValue) where T : IComparable;

        void Save();

        void Setup(Dictionary<string, object> defaultValues);
    }
}