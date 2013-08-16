//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="RoomViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Room
{
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Common.Interfaces;
    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Model;

    public class RoomListViewModel : BaseViewModel
    {
        private readonly IRoomRepository roomRepository;

        private IObservableCollection<Room> rooms;

        public RoomListViewModel(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }

        public ICommand LoadCommand
        {
            get
            {
                return new MvxCommand(this.OnLoadExecute);
            }
        }

        public IObservableCollection<Room> Rooms
        {
            get
            {
                return this.rooms;
            }
            set
            {
                this.rooms = value;
                this.RaisePropertyChanged(() => this.Rooms);
            }
        }

        public ICommand ShowDetailCommand
        {
            get
            {
                //return new MvxCommand<Room>(
                //    item => this.ShowViewModel<RoomDetailViewModel>(new {id = item.Id}));
                return new MvxCommand<Room>(this.ShowDetailExecute);
            }
        }

        private void ShowDetailExecute(Room room)
        {
            this.ShowViewModel<RoomDetailViewModel>(
                new
                {
                    id = room.Id
                });
        }

        public void Init()
        {
            this.LoadCommand.Execute(null);
        }

        private void OnLoadExecute()
        {
            this.Rooms = this.roomRepository.GetRooms();
        }
    }
}