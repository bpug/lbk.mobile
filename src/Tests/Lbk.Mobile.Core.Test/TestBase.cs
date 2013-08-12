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

        protected Mock<ILbkMobileService> CreateMockLbkMobileService()
        {
            var mock = new Mock<IDeviceUidService>();
            //mock.Setup(s => s.GetDeviceUid()).Returns(DeviceUidTest);
            mock.Setup(s => s.GetDeviceUid(It.IsAny<Action<string>>(), It.IsAny<Action<Exception>>()))
                .Callback(
                    (Action<string> onSuccess, Action<Exception> onError) =>
                    {
                        //this.OnDeviceUidSuccess = onSuccess;
                        onSuccess(Constants.DeviceUidTest);
                        this.OnDeviceUidError = onError;
                    });
            this.Ioc.RegisterSingleton<IDeviceUidService>(mock.Object);

            var serviceMock = new Mock<ILbkMobileService>();
            this.Ioc.RegisterSingleton(serviceMock.Object);
            return serviceMock;
        }

        protected MockMvxViewDispatcher CreateMocMvxNavigation()
        {
            var viewDispatcherMock = new Mock<IMvxViewDispatcher>();
            var dispatcher = new MockMvxViewDispatcher(viewDispatcherMock.Object);
            //var dispatcher = new MockMvxViewDispatcher();
            this.Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(dispatcher);
            this.Ioc.RegisterSingleton<IMvxViewDispatcher>(dispatcher);
            return dispatcher;
        }

        protected MockDispatcher CreateMockDispatcher()
        {
            var dispatcher = new MockDispatcher();
            Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(dispatcher);
            Ioc.RegisterSingleton<IMvxViewDispatcher>(dispatcher);
            return dispatcher;
        }

        protected void InitLbkMobileService()
        {
            var mock = new Mock<IDeviceUidService>();
            mock.Setup(s => s.GetDeviceUid(It.IsAny<Action<string>>(), It.IsAny<Action<Exception>>()))
                .Callback(
                    (Action<string> onSuccess, Action<Exception> onError) =>
                    {
                        //this.OnDeviceUidSuccess = onSuccess;
                        onSuccess(Constants.DeviceUidTest);
                        this.OnDeviceUidError = onError;
                    });

            var service = new LbkMobileService(mock.Object);
            this.Ioc.RegisterSingleton<ILbkMobileService>(service);
        }
    }
}