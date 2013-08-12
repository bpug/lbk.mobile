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

    using NUnit.Framework;

    [TestFixture]
    public class LbkServiceTest : TestBase
    {
       [Test]
        public async void GetDishesOfTheDayTest()
        {
            this.InitLbkMobileService();
            var service = this.Ioc.Resolve<ILbkMobileService>();

            var dishOfTheDay = await service.GetTodaysMenuAsync(DateTime.Now);

            Assert.NotNull(dishOfTheDay);
            Assert.Greater(dishOfTheDay.DishOfTheDay.Count(), 0);
        }

        [Test]
        public async void GetEventsTest()
        {
            this.InitLbkMobileService();
            var service = this.Ioc.Resolve<ILbkMobileService>();

            var result = await service.GetEventsAsync();

            Assert.NotNull(result);
            Assert.Greater(result.Count, 0);
        }

        [Test]
        public async void GetQuizTest()
        {
            const int QuestionsCount = 12;
            this.InitLbkMobileService();
            var service = this.Ioc.Resolve<ILbkMobileService>();

            var result = await service.GetQuizAsync(QuestionsCount);

            Assert.NotNull(result);
            Assert.AreEqual(result.Questions.Count(), QuestionsCount);
        }
    }
}