//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ReservationController.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Reservation
{
    using System;

    using Cirrious.CrossCore;

    using Lbk.Mobile.Core.ViewModels.Home;
    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Data.Services;
    using Lbk.Mobile.Model;
    using Lbk.Mobile.Model.Enums;

    public class ReservationManager
    {
        private readonly ILbkMobileService lbkMobileService;

        private readonly HomeViewModel parent;

        private readonly IReservationRepository reservationRepository;
       

        public ReservationManager(HomeViewModel parent)
        {
            this.lbkMobileService = Mvx.Resolve<ILbkMobileService>();
            this.reservationRepository = Mvx.Resolve<IReservationRepository>();
            this.parent = parent;

            Start();
        }

        //public Reservation Reservation { get; set; }

        public void Start()
        {
            var reservation = this.reservationRepository.GetRequested();
            if (reservation != null)
            {
                // Is Abgelaufen ?
                if (reservation.ReservationTime < DateTime.Now)
                {
                    reservation.Status = ReservationStatus.AbortedByCustomer;
                    this.reservationRepository.Update(reservation);
                    this.ShowReservationForm();
                    return;
                }
                this.CheckDeclinedByRestaurant(reservation);
            }
            else
            {
                this.ShowReservationForm();
            }
        }

        private async void CheckDeclinedByRestaurant(Reservation reservation)
        {
            await
                this.parent.AsyncExecute(
                    () => this.lbkMobileService.IsDeclinedReservationByRestaurantAsyn(reservation.ReservationId),
                    result =>
                    {
                        if (result)
                        {
                            reservation.Status = ReservationStatus.DeclinedByRestaurant;
                            this.reservationRepository.Update(reservation);
                            this.ShowReservationResult(reservation);
                        }
                        else
                        {
                            this.ShowReservationConfirmation(reservation);
                        }
                    });
        }

        private void ShowReservationConfirmation(Reservation reservation)
        {
            this.parent.DisplayViewModel<ReservationConfirmationViewModel>(
                new
                {
                    id = reservation.Id
                });
        }

        private void ShowReservationForm()
        {
            this.parent.DisplayViewModel<ReservationFormViewModel>();
        }

        private void ShowReservationResult(Reservation reservation)
        {
            this.parent.DisplayViewModel<ReservationResultViewModel>(
                new
                {
                    id = reservation.Id
                });
        }
    }
}