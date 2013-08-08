//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="UserSettings.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Test.Implementation
{
    using System;
    using System.Collections.Generic;

    using Lbk.Mobile.Core.Test.Properties;
    using Lbk.Mobile.Plugin.Settings;

    public class UserSettings : ISettings
    {
        public bool AddOrUpdateValue(string key, object value)
        {
            bool test = Settings.Default.YouthProtection;
            try
            {
                Settings.Default.PropertyValues[key].PropertyValue = value;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public T GetValueOrDefault<T>(string key, T defaultValue) where T : IComparable
        {
            T value;

            try
            {
                bool test = Settings.Default.YouthProtection;
                //Settings.Default.Reload();

                value = (T)(Settings.Default.PropertyValues[key].PropertyValue);
            }
            catch (Exception)
            {
                value = defaultValue;
            }

            return value;
        }

        public void Save()
        {
            Settings.Default.Save();
        }

        public void Setup(Dictionary<string, object> defaultValues)
        {
        }
    }
}