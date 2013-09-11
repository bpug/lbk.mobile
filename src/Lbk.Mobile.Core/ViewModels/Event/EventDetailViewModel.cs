﻿//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EventViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Event
{
    public class EventDetailViewModel : BaseViewModel
    {

        public class Nav
        {
            public string ReservationLink { get; set; }
        }

        public string ReservationLink { get; set; }

        public void Init(Nav navigation)
        {
            ReservationLink = navigation.ReservationLink;
        }

        public void Init(string reservationLink)
        {
            ReservationLink = reservationLink;
        }

    }
}