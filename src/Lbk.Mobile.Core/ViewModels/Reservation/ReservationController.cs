//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ReservationController.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Reservation
{
    using System;

    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Data.Services;
    using Lbk.Mobile.Model.Enums;
    using Lbk.Mobile.Model;

    public class ReservationController : BaseViewModel
    {
        private readonly ILbkMobileService lbkMobileService;

        private readonly IReservationRepository reservationRepository;

        private Reservation reservation;

        public ReservationController(ILbkMobileService lbkMobileService, IReservationRepository reservationRepository)
        {
            this.lbkMobileService = lbkMobileService;
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

        public override void Start()
        {
            base.Start();
            this.Reservation = this.reservationRepository.GetRequested();
            if (this.Reservation != null)
            {
                // Is Abgelaufen ?
                if (this.Reservation.ReservationTime < DateTime.Now)
                {
                    this.Reservation.Status = ReservationStatus.AbortedByCustomer;
                    this.reservationRepository.Update(this.Reservation);

                    return;
                }
                this.CheckDeclinedByRestaurant();
            }
            else
            {
                this.ShowReservationForm();
            }
        }

        public void Init()
        {
            
        }

        private async void CheckDeclinedByRestaurant()
        {
            await
                this.AsyncExecute(
                    () => this.lbkMobileService.IsDeclinedReservationByRestaurantAsyn(this.Reservation.ReservationId),
                    result =>
                    {
                        if (result)
                        {
                            this.Reservation.Status = ReservationStatus.DeclinedByRestaurant;
                            this.reservationRepository.Update(this.Reservation);
                            this.ShowReservationResult(this.Reservation);
                        }
                        else
                        {
                            this.ShowReservationConfirmation(this.Reservation);
                        }
                    });
        }

        private void ShowReservationConfirmation(Reservation booking)
        {
            this.ShowViewModel<ReservationResultViewModel>(
                new
                {
                    booking = booking
                });
        }

        private void ShowReservationForm()
        {
            this.ShowViewModel<ReservationFormViewModel>();
        }

        private void ShowReservationResult(Reservation booking)
        {
            this.ShowViewModel<ReservationResultViewModel>(
                new
                {
                    booking = booking
                });
        }
    }
}