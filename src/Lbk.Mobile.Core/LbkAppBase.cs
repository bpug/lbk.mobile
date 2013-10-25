//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LbkAppBase.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core
{
    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.Localization;
    using Cirrious.MvvmCross.ViewModels;
    
    using Lbk.Mobile.Core.Services;
    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Data.Services;
    using Lbk.Mobile.Localization;

    public abstract class LbkAppBase : MvxApplication
    {
        protected LbkAppBase()
        {
            this.InitaliseServices();
            //AutoMapperConfiguration.Configure();
        }

        
        protected abstract void InitialiseStartNavigation();

        private void InitaliseServices()
        {
            //var errorService = Mvx.IocConstruct<ErrorService>();
            //Mvx.RegisterSingleton<IErrorService>(errorService);

            //var errorService = new ErrorApplicationObject();
            //Mvx.RegisterSingleton<IErrorReporter>(errorService);
            //Mvx.RegisterSingleton<IErrorSource>(errorService);

            // use dynamic:
            Mvx.RegisterType<ILbkMobileService, LbkMobileService>();
            Mvx.RegisterType<IRoomRepository, RoomRepository>();
            Mvx.RegisterType<IHistoryRepository, HistoryRepository>();
            Mvx.RegisterType<IReservationRepository, ReservationRepository>();
            Mvx.RegisterType<IQuizVoucherRepository, QuizVoucherRepository>();
            Mvx.RegisterType<IGalleryRepository, GalleryRepository>();

            // use lazy:
            //Mvx.RegisterSingleton<ILbkMobileService>(() => new LbkMobileService());

            //CreatableTypes()
            //    .EndingWith("Repository")
            //    .AsInterfaces()
            //    .RegisterAsLazySingleton();

            //CreatableTypes()
            //    .EndingWith("Service")
            //    .AsInterfaces()
            //    .RegisterAsLazySingleton();

            Mvx.RegisterSingleton<IMvxTextProvider>(new ResxTextProvider(Strings.ResourceManager));
        }

        //private void InitialisePlugins()
        //{
        //    PluginLoader.Instance.EnsureLoaded();
        //    Cirrious.MvvmCross.Plugins.Email.PluginLoader.Instance.EnsureLoaded();
        //    //Cirrious.MvvmCross.Plugins.Network.PluginLoader.Instance.EnsureLoaded();
        //    Cirrious.MvvmCross.Plugins.WebBrowser.PluginLoader.Instance.EnsureLoaded();
        //    Cirrious.MvvmCross.Plugins.PhoneCall.PluginLoader.Instance.EnsureLoaded();
        //    Plugin.Settings.PluginLoader.Instance.EnsureLoaded();
        //}
    }
}