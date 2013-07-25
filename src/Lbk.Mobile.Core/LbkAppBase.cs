// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.cs" company="ip-connect GmbH">
//   Copyright (c) ip-connect GmbH. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core
{
    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.Localization;
    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Core.ApplicationObjects;
    using Lbk.Mobile.Core.Interfaces.Errors;
    using Lbk.Mobile.Core.Services;
    using Lbk.Mobile.Localization;

    public abstract class LbkAppBase : MvxApplication
    {
        protected LbkAppBase()
        {
            this.InitaliseErrorReporting();
            this.InitialisePlugins();
            this.InitaliseServices();
        }

        private void InitaliseErrorReporting()
        {
            var errorService = new ErrorApplicationObject();
            Mvx.RegisterSingleton<IErrorReporter>(errorService);
            Mvx.RegisterSingleton<IErrorSource>(errorService);
        }

        private void InitaliseServices()
        {
            Mvx.RegisterSingleton<IMvxTextProvider>(new ResxTextProvider(Strings.ResourceManager));
            //this.RegisterServiceInstance<IFirstService>(new FirstService());
        }

        private void InitialisePlugins()
        {
            // initialise any plugins where are required at app startup
            // e.g. Cirrious.MvvmCross.Plugins.Visibility.PluginLoader.Instance.EnsureLoaded();
            Cirrious.MvvmCross.Plugins.Messenger.PluginLoader.Instance.EnsureLoaded();
            Cirrious.MvvmCross.Plugins.Sqlite.PluginLoader.Instance.EnsureLoaded();
        }

        protected abstract void InitialiseStartNavigation();
    }
}