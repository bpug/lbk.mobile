//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="RoomViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Room
{
    using System.Collections.Generic;
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
                return new MvxCommand<Room>(item => this.ShowViewModel<RoomDetailViewModel>(new { id = item.Id }));
                
            }
        }

        public override void Start()
        {
            this.LoadCommand.Execute(null);
        }

        private void OnLoadExecute()
        {
            this.Rooms = this.roomRepository.GetRooms();
        }

        //private void OnLoadExecute()
        //{
        //    var mRooms = this.roomRepository.GetRooms();
        //    this.Rooms = mRooms.Select(x => new ModelWithCommand<Room>(x, new MvxCommand(() => ShowDetailExecute(x)))).ToList(); ;
        //}
    }
}