//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EventViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Event
{
    public class DetailViewModel : BaseViewModel
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

    }
}