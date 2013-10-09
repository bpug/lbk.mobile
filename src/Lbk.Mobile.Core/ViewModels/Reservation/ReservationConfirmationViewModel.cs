//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ReservationConfirmationViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Reservation
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Data.LbkMobileService;
    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Data.Services;

    public class ReservationConfirmationViewModel : BaseViewModel
    {
        private readonly ILbkMobileService lbkMobileService;

        private readonly IReservationRepository reservationRepository;

        private Reservation reservation;

        private string userConfirmCode;

        public ReservationConfirmationViewModel(
            ILbkMobileService lbkMobileService,
            IReservationRepository reservationRepository)
        {
            this.lbkMobileService = lbkMobileService;
            this.reservationRepository = reservationRepository;
        }

        public ICommand AbortCommand
        {
            get
            {
                return new MvxCommand(this.AbortExecute);
            }
        }

        public ICommand ConfirmCommand
        {
            get
            {
                return new MvxCommand(async () => await this.ConfirmExecute(), () => this.UserConfirmCode.Length > 3);
            }
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

        public string UserConfirmCode
        {
            get
            {
                return this.userConfirmCode;
            }
            set
            {
                this.userConfirmCode = value;
                this.RaisePropertyChanged(() => this.UserConfirmCode);
            }
        }

        private void AbortExecute()
        {
            this.ShowConfirm(
                this.TextSource.GetText("AbortReservationQuestion"),
                string.Empty,
                async result =>
                {
                    if (result)
                    {
                        await
                            this.AsyncExecute(
                                () =>
                                    this.lbkMobileService.AbortedReservationByCustomerAsync(
                                        this.Reservation.ReservationId),
                                isAborted =>
                                {
                                    if (isAborted)
                                    {
                                        this.Reservation.Status = StatusArt.AbortedByCustomer;
                                        this.reservationRepository.Update(this.Reservation);
                                        this.ShowReservationResult();
                                    }
                                    else
                                    {
                                        this.ShowAlert(
                                            this.TextSource.GetText("AbortReservationNotConfirmed"),
                                            string.Empty);
                                    }
                                });
                    }
                });
        }

        private async Task ConfirmExecute()
        {
            if (this.UserConfirmCode.Equals(this.Reservation.ConfirmCode))
            {
                await
                    this.AsyncExecute(
                        () => this.lbkMobileService.ConfirmedReservationByCustomerAsync(this.Reservation.ReservationId),
                        result =>
                        {
                            if (result)
                            {
                                this.Reservation.Status = StatusArt.ConfirmedByCustomer;
                                this.reservationRepository.Update(this.Reservation);
                                this.ShowReservationResult();
                            }
                        });
            }
            else
            {
                this.ShowAlert(this.TextSource.GetText("ReservationNotConfirmed"), string.Empty);
            }
        }

        private void Init(Reservation booking)
        {
            this.Reservation = booking;
        }

        private void ShowReservationResult()
        {
            this.ShowViewModel<ReservationResultViewModel>(
                new
                {
                    booking = this.Reservation
                });
        }
    }
}