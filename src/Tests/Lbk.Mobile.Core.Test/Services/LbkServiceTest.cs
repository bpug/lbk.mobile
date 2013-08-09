//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LbkServiceTest.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Test.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Lbk.Mobile.Data.LbkMobileService;
    using Lbk.Mobile.Data.Service;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class LbkServiceTest : TestBase
    {
        //[Test]
        //public async void TestGetEventsAsync()
        //{
        //    base.ClearAll();

        //    var mock = new Mock<ILbkMobileService>();
        //    Ioc.RegisterSingleton<ILbkMobileService>(mock.Object);
        //    mock.Setup(s => s.GetEventsAsync("test"));
        //    mock.Verify(s => s.GetEventsAsync("test"));
        //    var model = new EventListViewModel(mock.Object);
        //}

        [Test]
        public async void GetDishesOfTheDayTest()
        {
            //this.InitLbkMobileService();
            //var service = this.Ioc.Resolve<ILbkMobileService>();

            var mockService = this.CreateMockMobileService();
            var service = mockService.Object;

            //DishesOfTheDay result = null;
            //mockService.Setup(m => m.GetTodaysMenuAsync(It.IsAny<DateTime>())).Callback( async (Task<DishesOfTheDay> e) => { result = await e; });

            //mock.Setup(s => s.GetDeviceUid()).Returns(DeviceUidTest);

            this.OnDeviceUidSuccess(Constants.DeviceUidTest);

            var dishOfTheDay = await service.GetTodaysMenuAsync(DateTime.Now);

            Assert.NotNull(dishOfTheDay);
            Assert.Greater(dishOfTheDay.DishOfTheDay.Count(), 0);

            //mockService.Verify(s => s.GetTodaysMenuAsync(It.IsAny<DateTime>()), Times.Once());
        }

        [Test]
        public async void GetEventsTest()
        {
            //this.InitLbkMobileService();
            //var service = this.Ioc.Resolve<ILbkMobileService>();

            var mockService = this.CreateMockMobileService();
            var service = mockService.Object;

            this.OnDeviceUidSuccess(Constants.DeviceUidTest);

            var result = await service.GetEventsAsync();

            Assert.NotNull(result);
            Assert.Greater(result.Count, 0);

            //mockService.Verify(s => s.GetEventsAsync(), Times.Once());
        }

        [Test]
        public async void GetQuizTest()
        {
            const int QuestionsCount = 12;
            this.InitLbkMobileService();
            var service = this.Ioc.Resolve<ILbkMobileService>();
            //mock.Setup(s => s.GetDeviceUid()).Returns(DeviceUidTest);

            this.OnDeviceUidSuccess(Constants.DeviceUidTest);

            var result = await service.GetQuizAsync(QuestionsCount);

            Assert.NotNull(result);
            Assert.AreEqual(result.Questions.Count(), QuestionsCount);
        }
    }
}