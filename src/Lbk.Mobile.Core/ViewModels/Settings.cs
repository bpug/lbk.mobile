//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Settings.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels
{
    using System.Collections.Generic;

    using Cirrious.CrossCore;

    using Lbk.Mobile.Plugin.Settings;

    public static class Settings
    {
        private const bool YouthProtectionDefault = false;

        private const string YouthProtectionKey = "YouthProtection";

        private static ISettings settings;

        private static bool setup;

        public static bool YouthProtection
        {
            get
            {
                return UserSettings.GetValueOrDefault(YouthProtectionKey, YouthProtectionDefault);
            }
            set
            {
                if (UserSettings.AddOrUpdateValue(YouthProtectionKey, value))
                {
                    UserSettings.Save();
                }
            }
        }

        private static ISettings UserSettings
        {
            get
            {
                if (settings == null && !setup)
                {
                    Setup();
                    setup = true;
                }

                return settings;
            }
        }

        /// <summary>
        ///     Setup required for IOS only.
        /// </summary>
        private static void Setup()
        {
            settings = Mvx.GetSingleton<ISettings>();
            settings.Setup(
                new Dictionary<string, object>
                {
                    {
                        YouthProtectionKey, YouthProtectionDefault
                    },
                });
        }
    }
}