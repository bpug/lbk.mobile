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

        private int roomId;

        public RoomDetailViewModel(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }

        public Room Room { get; set; }

        public void Init(int id)
        {
            this.roomId = id;
        }

        public override void Start()
        {
            this.IsBusy = true;
            this.roomRepository.GetRoom(
                this.roomId,
                mroom =>
                {
                    this.Room = mroom;
                    this.IsBusy = false;
                },
                exception => { this.IsBusy = false; });
        }
    }
}