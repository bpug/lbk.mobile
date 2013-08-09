//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TestBase.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Test
{
    using System;

    using Cirrious.CrossCore.Core;
    using Cirrious.MvvmCross.Test.Core;
    using Cirrious.MvvmCross.Views;

    using Lbk.Mobile.Core.Test.Mocks;
    using Lbk.Mobile.Data.Service;
    using Lbk.Mobile.Plugin.DeviceIdentifier;

    using Moq;

    using NUnit.Framework;

    public class TestBase : MvxIoCSupportingTest
    {
        public Action<Exception> OnDeviceUidError = null;

        public Action<string> OnDeviceUidSuccess = null;

        [SetUp]
        public void Init()
        {
            this.Setup();
        }

        protected MockMvxViewDispatcher CreateMockNavigation()
        {
            var viewDispatcherMock = new Mock<IMvxViewDispatcher>();
            var dispatcher = new MockMvxViewDispatcher(viewDispatcherMock.Object);
            //var dispatcher = new MockMvxViewDispatcher();
            this.Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(dispatcher);
            this.Ioc.RegisterSingleton<IMvxViewDispatcher>(dispatcher);
            return dispatcher;
        }

        protected void InitLbkMobileService()
        {
            var mock = new Mock<IDeviceUidService>();
            mock.Setup(s => s.GetDeviceUid(It.IsAny<Action<string>>(), It.IsAny<Action<Exception>>()))
                .Callback(
                    (Action<string> onSuccess, Action<Exception> onError) =>
                    {
                        this.OnDeviceUidSuccess = onSuccess;
                        this.OnDeviceUidError = onError;
                    });

            var service = new LbkMobileService(mock.Object);
            this.Ioc.RegisterSingleton<ILbkMobileService>(service);
            
        }

        protected Mock<LbkMobileService> CreateMockMobileService()
        {
            var mock = new Mock<IDeviceUidService>();
            mock.Setup(s => s.GetDeviceUid(It.IsAny<Action<string>>(), It.IsAny<Action<Exception>>()))
                .Callback(
                    (Action<string> onSuccess, Action<Exception> onError) =>
                    {
                        this.OnDeviceUidSuccess = onSuccess;
                        this.OnDeviceUidError = onError;
                    });
            Ioc.RegisterSingleton<IDeviceUidService>(mock.Object);

            //var service = new LbkMobileService(mock.Object);
            //this.Ioc.RegisterSingleton<ILbkMobileService>(service);

            var service = new Mock<LbkMobileService>(mock.Object);
            Ioc.RegisterSingleton(service.Object);

            return service;
        }
    }
}