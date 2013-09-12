//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EventBookingViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Event
{
    public class EventBookingViewModel : BaseViewModel
    {
        private string reservationLink;

        private string title;

        public string ReservationLink
        {
            get
            {
                return this.reservationLink;
            }
            set
            {
                this.reservationLink = value;
                this.RaisePropertyChanged(() => this.ReservationLink);
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
                this.RaisePropertyChanged(() => this.Title);
            }
        }

        public void Init(Nav navigation)
        {
            this.ReservationLink = navigation.ReservationLink;
            this.Title = navigation.Title;
        }

        public class Nav
        {
            public string ReservationLink { get; set; }
            public string Title { get; set; }
        }
    }
}