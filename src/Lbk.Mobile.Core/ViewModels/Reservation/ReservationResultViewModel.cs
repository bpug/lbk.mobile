//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ReservationResultViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Reservation
{
    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Model;
    using Lbk.Mobile.Model.Enums;

    public class ReservationResultViewModel : BaseViewModel
    {
        private Reservation reservation;

        private readonly IReservationRepository reservationRepository;
        public ReservationResultViewModel(IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
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

        private int reservationId;

        public void Init(int id)
        {
            this.reservationId = id;
        }

        public string ConfirmationMassage { get; set; }

        public override void Start()
        {
            base.Start();
            this.Reservation = this.reservationRepository.Get(reservationId);

            switch (this.Reservation.Status)
            {
                case ReservationStatus.AbortedByCustomer:
                    this.ConfirmationMassage = this.GetText("AbortedByCustomer");
                    break;
                case ReservationStatus.DeclinedByRestaurant:
                    this.ConfirmationMassage = this.GetText("SuccessConfirmation");
                    break;
                case ReservationStatus.ConfirmedByCustomer:
                    this.ConfirmationMassage = this.GetText("SuccessConfirmation");
                    break;
            }
        }
    }
}