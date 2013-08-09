//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LbkServiceTest.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Test.Services
{
    using System;
    using System.Linq;

    using Lbk.Mobile.Data.Service;
    using Lbk.Mobile.Plugin.DeviceIdentifier;

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

            //mock.Setup(s => s.GetDeviceUid()).Returns(DeviceUidTest);

            var service = new LbkMobileService(mock.Object);
            onDeviceUidSuccess(Constants.DeviceUidTest);

            var result = await service.GetTodaysMenuAsync(DateTime.Now);

            Assert.NotNull(result);
            Assert.Greater(result.DishOfTheDay.Count(), 0);
        }

        [Test]
        public async void GetEventsTest()
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

            //mock.Setup(s => s.GetDeviceUid()).Returns(DeviceUidTest);

            var service = new LbkMobileService(mock.Object);
            onDeviceUidSuccess(Constants.DeviceUidTest);

            var result = await service.GetEventsAsync();

            Assert.NotNull(result);
            Assert.Greater(result.Count, 0);
        }

        [Test]
        public async void GetQuizTest()
        {
            Action<string> onDeviceUidSuccess = null;
            Action<Exception> onDeviceUidError = null;

            const int QuestionsCount = 12;

            var mock = new Mock<IDeviceUidService>();
            mock.Setup(s => s.GetDeviceUid(It.IsAny<Action<string>>(), It.IsAny<Action<Exception>>()))
                .Callback(
                    (Action<string> id, Action<Exception> error) =>
                    {
                        onDeviceUidSuccess = id;
                        onDeviceUidError = error;
                    });

            //mock.Setup(s => s.GetDeviceUid()).Returns(DeviceUidTest);

            var service = new LbkMobileService(mock.Object);
            onDeviceUidSuccess(Constants.DeviceUidTest);

            var result = await service.GetQuizAsync(QuestionsCount);

            Assert.NotNull(result);
            Assert.AreEqual(result.Questions.Count(), QuestionsCount);
        }
    }
}