//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="RoomDetailViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Room
{
    using Lbk.Mobile.Data.Service;
    using Lbk.Mobile.Model;

    public class RoomDetailViewModel : BaseViewModel
    {
        private readonly IXmlDataService xmlDataService;

        private Room room;

        public RoomDetailViewModel(IXmlDataService xmlDataService)
        {
            this.xmlDataService = xmlDataService;
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
            this.Room = this.xmlDataService.GetRoom(id);
        }
    }
}