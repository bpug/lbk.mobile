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

    using Lbk.Mobile.Core.Services;
    using Lbk.Mobile.Core.Services.Error;
    using Lbk.Mobile.Localization;

    public abstract class LbkAppBase : MvxApplication
    {
        protected LbkAppBase()
        {
            this.InitaliseServices();
            this.InitaliseErrorReporting();
            //this.InitialisePlugins();
            //AutoMapperConfiguration.Configure();
        }

        protected abstract void InitialiseStartNavigation();

        private void InitaliseErrorReporting()
        {
            var errorService = Mvx.IocConstruct<ErrorService>();
            Mvx.RegisterSingleton<IErrorService>(errorService);

            //var errorService = new ErrorApplicationObject();
            //Mvx.RegisterSingleton<IErrorReporter>(errorService);
            //Mvx.RegisterSingleton<IErrorSource>(errorService);
        }

        private void InitaliseServices()
        {
            Mvx.RegisterSingleton<IMvxTextProvider>(new ResxTextProvider(Strings.ResourceManager));

            this.CreatableTypes().EndingWith("Repository").AsInterfaces().RegisterAsLazySingleton();

            this.CreatableTypes().EndingWith("Service").AsInterfaces().RegisterAsLazySingleton();

            //Mvx.RegisterType<IXmlDataService, XmlDataService>();
            //Mvx.RegisterType<ILbkMobileService, LbkMobileService>();
        }

        private void InitialisePlugins()
        {
            //PluginLoader.Instance.EnsureLoaded();

            PluginLoader.Instance.EnsureLoaded();
            Cirrious.MvvmCross.Plugins.Sqlite.PluginLoader.Instance.EnsureLoaded();
            Cirrious.MvvmCross.Plugins.Email.PluginLoader.Instance.EnsureLoaded();
            Cirrious.MvvmCross.Plugins.Network.PluginLoader.Instance.EnsureLoaded();
            Cirrious.MvvmCross.Plugins.WebBrowser.PluginLoader.Instance.EnsureLoaded();
            Cirrious.MvvmCross.Plugins.PhoneCall.PluginLoader.Instance.EnsureLoaded();
            Plugin.DeviceIdentifier.PluginLoader.Instance.EnsureLoaded();
            Plugin.Settings.PluginLoader.Instance.EnsureLoaded();
        }
    }
}