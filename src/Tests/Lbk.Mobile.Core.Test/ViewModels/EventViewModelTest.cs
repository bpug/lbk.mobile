//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EvetnViewModelTest.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Test.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;

    using Cirrious.CrossCore.Core;
    using Cirrious.MvvmCross.Plugins.Network.Reachability;
    using Cirrious.MvvmCross.Views;

    using Lbk.Mobile.Core.Test.Implementation;
    using Lbk.Mobile.Core.Test.Mocks;
    using Lbk.Mobile.Core.ViewModels.Event;
    using Lbk.Mobile.Data.LbkMobileService;
    using Lbk.Mobile.Data.Service;

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

            var result = new List<Event>
            {
                new Event(),
                new Event()
            };

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
                       Assert.AreEqual(2 , vm.Events.Count);
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
    }
}