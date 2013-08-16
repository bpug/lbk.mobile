//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="RoomDetailViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Room
{
    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Model;

    public class RoomDetailViewModel : BaseViewModel
    {
        private readonly IRoomRepository roomRepository;

        private Room room;

        public RoomDetailViewModel(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }

        public Room Room
        {
            get
            {
                return this.room;
            }
            set
            {
                this.room = value;
                this.RaisePropertyChanged(() => this.Room);
            }
        }

        public void Init(int id)
        {
            this.Room = this.roomRepository.GetRoom(id);
        }
    }
}