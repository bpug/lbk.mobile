//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LbkAppBase.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core
{
    using Cirrious.CrossCore;
    using Cirrious.CrossCore.IoC;
    using Cirrious.MvvmCross.Localization;
    using Cirrious.MvvmCross.Plugins.Messenger;
    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Core.ApplicationObjects;
    using Lbk.Mobile.Core.AutoMapper;
    using Lbk.Mobile.Core.Interfaces.Errors;
    using Lbk.Mobile.Core.Services;
    using Lbk.Mobile.Data.Service.Interfaces;
    using Lbk.Mobile.Data.Service.Service;
    using Lbk.Mobile.Localization;

    public abstract class LbkAppBase : MvxApplication
    {
        protected LbkAppBase()
        {
            this.InitaliseErrorReporting();
            this.InitialisePlugins();
            this.InitaliseServices();
            AutoMapperConfiguration.Configure();
        }

        protected abstract void InitialiseStartNavigation();

        private void InitaliseErrorReporting()
        {
            var errorService = new ErrorApplicationObject();
            Mvx.RegisterSingleton<IErrorReporter>(errorService);
            Mvx.RegisterSingleton<IErrorSource>(errorService);
        }

        private void InitaliseServices()
        {
            Mvx.RegisterSingleton<IMvxTextProvider>(new ResxTextProvider(Strings.ResourceManager));

            CreatableTypes()
               .EndingWith("Service")
               .AsInterfaces()
               .RegisterAsLazySingleton();

            //Mvx.RegisterType<IXmlDataService, XmlDataService>();
            //Mvx.RegisterType<ILbkMobileService, LbkMobileService>();
            
        }

        private void InitialisePlugins()
        {
            // initialise any plugins where are required at app startup
            // e.g. Cirrious.MvvmCross.Plugins.Visibility.PluginLoader.Instance.EnsureLoaded();
            //PluginLoader.Instance.EnsureLoaded();
            Cirrious.MvvmCross.Plugins.File.PluginLoader.Instance.EnsureLoaded();
            Cirrious.MvvmCross.Plugins.Sqlite.PluginLoader.Instance.EnsureLoaded();
            Cirrious.MvvmCross.Plugins.Email.PluginLoader.Instance.EnsureLoaded();
            Cirrious.MvvmCross.Plugins.DeviceIdentifier.PluginLoader.Instance.EnsureLoaded();
            Cirrious.MvvmCross.Plugins.Network.PluginLoader.Instance.EnsureLoaded();
        }
    }
}