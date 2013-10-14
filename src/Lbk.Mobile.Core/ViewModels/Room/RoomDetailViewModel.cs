//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="RoomDetailViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Room
{
    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Model;

    public class RoomDetailViewModel : BaseViewModel
    {
        private readonly IRoomRepository roomRepository;

        public RoomDetailViewModel(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }

        public Room Room { get; set; }

        public void Init(int id)
        {
            this.Room = this.roomRepository.GetRoom(id);
        }
    }
}