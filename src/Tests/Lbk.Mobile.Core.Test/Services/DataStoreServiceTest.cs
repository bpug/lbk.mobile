//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ResourceDataStoreServiceTest.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Test.Services
{
    using System;
    using System.Collections.Generic;

    using Cirrious.MvvmCross.Plugins.File;

    using Lbk.Mobile.Core.Test.Implementation;
    using Lbk.Mobile.Data.Service.Service;
    using Lbk.Mobile.Data.Service.Utility;
    using Lbk.Mobile.Infrastructure.Interfaces;
    using Lbk.Mobile.Model;

    using NUnit.Framework;

    [TestFixture]
    public class DataStoreServiceTest : TestBase
    {
         [Test]
        public void XmlSerializerTest()
        {
            //var mockFinder = new Mock<IMvxFileStore>();
            //Ioc.RegisterSingleton(mockFinder.Object);

            this.Ioc.RegisterType<IMvxFileStore, MvxTestFileStore>();
            //Ioc.RegisterType<IMvxResourceLoader, MvxTestResourceLoader>();

            var path = @"Xml/History.xml";
            var histories = XmlSerializer<List<History>>.Load(path);

            Assert.NotNull(histories);
            Assert.Greater(histories.Count, 0);

            var count = histories.Count;
            histories.Add(new History { Description = "New Item", Title = Guid.NewGuid().ToString() });

            XmlSerializer<List<History>>.Save(histories, path);

            histories = XmlSerializer<List<History>>.Load(path);

            Assert.AreEqual(histories.Count, count + 1);
            
        }

         [Test]
         public void GetHistoriesTest()
         {
             this.Ioc.RegisterType<IMvxFileStore, MvxTestFileStore>();

             var service = new XmlDataService();
             var histories = service.GetHistories();

             Assert.NotNull(histories);
             Assert.Greater(histories.Count, 0);
             Assert.IsInstanceOf<IObservableCollection<History>>(histories);
         }

         [Test]
         public void GetRoomsTest()
         {
             this.Ioc.RegisterType<IMvxFileStore, MvxTestFileStore>();

             var service = new XmlDataService();
             var rooms = service.GetRooms();

             Assert.NotNull(rooms);
             Assert.Greater(rooms.Count, 0);
             Assert.IsInstanceOf<IObservableCollection<Room>>(rooms);
         }
    }
}