//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EventBookingViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Event
{
    public class EventBookingViewModel : BaseViewModel
    {
       public string ReservationLink { get; set; }

        public string Title { get; set; }

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