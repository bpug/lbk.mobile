//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EvetnViewModelTest.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Test.ViewModels
{
    using Cirrious.MvvmCross.Plugins.Network.Reachability;

    using Lbk.Mobile.Core.Test.Implementation;
    using Lbk.Mobile.Core.ViewModels.Event;
    using Lbk.Mobile.Data.Service;

    using NUnit.Framework;

    [TestFixture]
    public class EventViewModelTest : TestBase
    {
        [Test]
        public async void LodEvents()
        {
            this.InitLbkMobileService();
            var service = this.Ioc.Resolve<ILbkMobileService>();

            var eventViewModel = new ListViewModel(service);
            this.OnDeviceUidSuccess(Constants.DeviceUidTest);

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