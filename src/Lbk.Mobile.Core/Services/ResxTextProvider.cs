﻿
namespace Lbk.Mobile.Core.Services
{
    using System.Globalization;
    using System.Resources;
    using System.Threading;

    using Cirrious.MvvmCross.Localization;

    public class ResxTextProvider : IMvxTextProvider
    {
        private readonly ResourceManager resourceManager;

        public ResxTextProvider(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
            CurrentLanguage = Thread.CurrentThread.CurrentUICulture;
        }

        public CultureInfo CurrentLanguage { get; set; }

        public string GetText(string namespaceKey, string typeKey, string name)
        {
            string resolvedKey = name;

            if (!string.IsNullOrEmpty(typeKey))
            {
                resolvedKey = string.Format("{0}.{1}", typeKey, resolvedKey);
            }

            if (!string.IsNullOrEmpty(namespaceKey))
            {
                resolvedKey = string.Format("{0}.{1}", namespaceKey, resolvedKey);
            }

            return this.resourceManager.GetString(resolvedKey, CurrentLanguage);
        }

        public string GetText(string namespaceKey, string typeKey, string name, params object[] formatArgs)
        {
            string baseText = GetText(namespaceKey, typeKey, name);

            if (string.IsNullOrEmpty(baseText))
            {
                return baseText;
            }

            return string.Format(baseText, formatArgs);
        }
    }
}
