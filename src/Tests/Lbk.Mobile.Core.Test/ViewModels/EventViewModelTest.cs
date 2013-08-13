//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EventViewModelTest.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Test.ViewModels
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Cirrious.MvvmCross.Plugins.Network.Reachability;

    using Lbk.Mobile.Core.Test.Implementation;
    using Lbk.Mobile.Core.ViewModels.Event;
    using Lbk.Mobile.Data.LbkMobileService;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class EventViewModelTest : TestBase
    {
        [Test]
        public void LodEvents()
        {
            this.CreateMockDispatcher();
            var mockService = this.CreateMockLbkMobileService();

            var result = this.GetEventsData();

            var tcs = new TaskCompletionSource<List<Event>>();
            tcs.SetResult(result);
            mockService.Setup(s => s.GetEventsAsync()).Returns(tcs.Task);

            var eventViewModel = new ListViewModel(mockService.Object);

            eventViewModel.PropertyChanged += (sender, args) =>
            {
                var vm = (ListViewModel)sender;
                switch (args.PropertyName)
                {
                    case "Events":
                        Assert.AreEqual(2, vm.Events.Count);
                        break;
                }
            };
            eventViewModel.Init();
            mockService.Verify(s => s.GetEventsAsync(), Times.Once());
            Assert.AreEqual(2, eventViewModel.Events.Count);
        }

        protected override void AdditionalSetup()
        {
            this.Ioc.RegisterType<IMvxReachability, MvxTestReachability>();
        }

        private List<Event> GetEventsData()
        {
            var result = new List<Event>
            {
                new Event(),
                new Event()
            };
            return result;
        }
    }
}