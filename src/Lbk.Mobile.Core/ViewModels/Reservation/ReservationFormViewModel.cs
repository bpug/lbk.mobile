//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ReservationFormViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Reservation
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Data.LbkMobileService;
    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Data.Services;

    public class ReservationFormViewModel : BaseViewModel
    {
        private const int HourLimit = 1;

        private const int MaxSeats = 10;

        private readonly ILbkMobileService lbkMobileService;

        private readonly IReservationRepository reservationRepository;

        public ReservationFormViewModel(
            ILbkMobileService lbkMobileService,
            IReservationRepository reservationRepository)
        {
            this.lbkMobileService = lbkMobileService;
            this.reservationRepository = reservationRepository;
        }

        public int MaximumSeats
        {
            get
            {
                return MaxSeats;
            }
        }

        public DateTime MinimumReservationDate
        {
            get
            {
                return DateTime.Now.AddHours(HourLimit);
            }
        }
        private Reservation reservation;

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

        

        public ICommand ReserveCommand
        {
            get
            {
                return new MvxCommand(async () => await this.ReserveExecute());
            }
        }

        private async Task ReserveExecute()
        {
            await this.AsyncExecute(
                () => this.lbkMobileService.CreateReservationAsync(this.Reservation),
                result =>
                {
                    this.Reservation.ReservationId = result;
                    this.reservationRepository.Update(this.Reservation);
                    this.ShowReservationResult(this.Reservation);
                });
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