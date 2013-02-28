// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.cs" company="ip-connect GmbH">
//   Copyright (c) ip-connect GmbH. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Portable.Core
{
    using Cirrious.MvvmCross.Application;
    using Cirrious.MvvmCross.ExtensionMethods;
    using Cirrious.MvvmCross.Interfaces.ServiceProvider;
    using Cirrious.MvvmCross.Interfaces.ViewModels;

    using Lbk.Mobile.Portable.Core.ApplicationObjects;
    using Lbk.Mobile.Portable.Core.Interfaces.Errors;

    public class App : MvxApplication, IMvxServiceProducer
    {
        public App()
        {
            this.InitaliseErrorReporting();
            this.InitialisePlugins();
            this.InitaliseServices();
            this.InitialiseStartNavigation();
        }

        private void InitaliseErrorReporting()
        {
            var errorService = new ErrorApplicationObject();
            this.RegisterServiceInstance<IErrorReporter>(errorService);
            this.RegisterServiceInstance<IErrorSource>(errorService);
        }

        private void InitaliseServices()
        {
            //this.RegisterServiceInstance<IFirstService>(new FirstService());
        }

        private void InitialisePlugins()
        {
            // initialise any plugins where are required at app startup
            // e.g. Cirrious.MvvmCross.Plugins.Visibility.PluginLoader.Instance.EnsureLoaded();
        }

        private void InitialiseStartNavigation()
        {
            var startApplicationObject = new StartApplication();
            this.RegisterServiceInstance<IMvxStartNavigation>(startApplicationObject);
        }
    }
}