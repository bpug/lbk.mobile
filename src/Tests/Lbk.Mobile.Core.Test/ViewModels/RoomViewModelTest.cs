//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="RoomViewModelTest.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Test.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using Cirrious.MvvmCross.Platform;

    using Lbk.Mobile.Common;
    using Lbk.Mobile.Common.Interfaces;
    using Lbk.Mobile.Core.ViewModels.Room;
    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Model;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class RoomViewModelTest : TestBase
    {
        [Test]
        public void LoadAndNavigate()
        {
            var mockNavigation = this.CreateMockDispatcher();
            var mockService = new Mock<IRoomRepository>();

            var result = this.GetRoomsData();
            mockService.Setup(s => s.GetRooms()).Returns(result);

            var roomListViewModel = new RoomListViewModel(mockService.Object);

            roomListViewModel.PropertyChanged += (sender, args) =>
            {
                var vm = (RoomListViewModel)sender;
                switch (args.PropertyName)
                {
                    case "Rooms":
                        Assert.AreEqual(2, vm.Rooms.Count);
                        break;
                }
            };
            roomListViewModel.Init();
            mockService.Verify(s => s.GetRooms(), Times.Once());
            Assert.AreEqual(2, roomListViewModel.Rooms.Count);


            roomListViewModel.ShowDetailCommand.Execute(roomListViewModel.Rooms.LastOrDefault());

            Assert.AreEqual(1, mockNavigation.Requests.Count);
            var request = mockNavigation.Requests[0];
            Assert.AreEqual(typeof(RoomDetailViewModel), request.ViewModelType);
            Assert.AreEqual("2", request.ParameterValues["id"]);
        }

        protected override void AdditionalSetup()
        {
            Ioc.RegisterSingleton<IMvxStringToTypeParser>(new MvxStringToTypeParser());
        }

        private IObservableCollection<Room> GetRoomsData()
        {
            var rooms = new List<Room>
            {
                new Room
                {
                    Id = 1,
                    Description = "Room 1",
                    Title = "Title Room1"
                },
                new Room
                {
                    Id = 2,
                    Description = "Room 2",
                    Title = "Title Room2"
                }
            };
            return new SimpleObservableCollection<Room>(rooms);
        }
    }
}