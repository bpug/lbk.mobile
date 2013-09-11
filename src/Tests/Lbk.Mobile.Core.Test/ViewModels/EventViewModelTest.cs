//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EventViewModelTest.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Test.ViewModels
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Lbk.Mobile.Core.Test.Implementation;
    using Lbk.Mobile.Core.ViewModels.Event;
    using Lbk.Mobile.Data.LbkMobileService;
    using Lbk.Mobile.Plugin.Reachability;

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

            var tcs = new TaskCompletionSource<List<Model.Event>>();
            tcs.SetResult(result);
            mockService.Setup(s => s.GetEventsAsync()).Returns(tcs.Task);

            var eventViewModel = new EventListViewModel(mockService.Object);

            eventViewModel.PropertyChanged += (sender, args) =>
            {
                var vm = (EventListViewModel)sender;
                switch (args.PropertyName)
                {
                    case "Events":
                        Assert.AreEqual(2, vm.Events.Count);
                        break;
                }
            };
            eventViewModel.Start();
            mockService.Verify(s => s.GetEventsAsync(), Times.Once());
            Assert.AreEqual(2, eventViewModel.Events.Count);
        }

        protected override void AdditionalSetup()
        {
            this.Ioc.RegisterType<IReachability, MvxTestReachability>();
        }

        private List<Model.Event> GetEventsData()
        {
            var result = new List<Model.Event>
            {
                new Model.Event(),
                new Model.Event()
            };
            return result;
        }
    }
}