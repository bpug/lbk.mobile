//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ReservationResultViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Reservation
{
    using Lbk.Mobile.Data.LbkMobileService;

    public class ReservationResultViewModel : BaseViewModel
    {
        private Reservation reservation;

        public ReservationResultViewModel()
        {
        }

        public Reservation Reservation
        {
            get
            {
                return this.reservation;
            }
            set
            {
                this.reservation = value;
                this.RaisePropertyChanged(() => this.Reservation);
            }
        }

        private void Init(Reservation booking)
        {
            this.Reservation = booking;
        }
    }
}