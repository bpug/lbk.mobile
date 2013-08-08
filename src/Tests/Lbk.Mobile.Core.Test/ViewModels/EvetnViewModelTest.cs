//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EvetnViewModelTest.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Test.ViewModels
{
    using System;

    using Cirrious.MvvmCross.Plugins.Network.Reachability;

    using Lbk.Mobile.Core.Test.Services;
    using Lbk.Mobile.Core.ViewModels.Event;
    using Lbk.Mobile.Data.Service.Service;
    using Lbk.Mobile.Plugin.DeviceIdentifier;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class EvetnViewModelTest : TestBase
    {
        [Test]
        public async void LodEvents()
        {
            Action<string> onDeviceUidSuccess = null;
            Action<Exception> onDeviceUidError = null;

            var mock = new Mock<IDeviceUidService>();
            mock.Setup(s => s.GetDeviceUid(It.IsAny<Action<string>>(), It.IsAny<Action<Exception>>()))
                .Callback(
                    (Action<string> id, Action<Exception> error) =>
                    {
                        onDeviceUidSuccess = id;
                        onDeviceUidError = error;
                    });

            var service = new LbkMobileService(mock.Object);

            var eventViewModel = new ListViewModel(service);
            onDeviceUidSuccess(Constants.DeviceUidTest);

            //var result = await eventViewModel.service.GetEventsAsync();

            //eventViewModel.LoadCommand.Execute(null);

            var test = eventViewModel.Events;
        }

        protected override void AdditionalSetup()
        {
            this.Ioc.RegisterType<IMvxReachability, MvxTestReachability>();
        }
    }
}