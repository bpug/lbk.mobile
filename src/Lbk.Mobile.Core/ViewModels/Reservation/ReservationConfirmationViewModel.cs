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

    using Lbk.Mobile.Model;
    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Data.Services;
    using Lbk.Mobile.Model.Enums;

    public class ReservationConfirmationViewModel : BaseViewModel
    {
        private readonly ILbkMobileService lbkMobileService;

        private readonly IReservationRepository reservationRepository;
        
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
                return new MvxCommand(async () => await this.ConfirmExecute(), () => this.IsCodeLengthVaild);
            }
        }

        public bool IsCodeLengthVaild
        {
            get
            {
                return this.UserConfirmCode.Length > 3;
            }
        }

        public Reservation Reservation { get; set; }

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
                this.GetText("AbortReservationQuestion"),
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
                                        this.Reservation.Status = ReservationStatus.AbortedByCustomer;
                                        this.reservationRepository.Update(this.Reservation);
                                        this.ShowReservationResult();
                                    }
                                    else
                                    {
                                        this.ShowAlert(
                                            this.GetText("AbortReservationNotConfirmed"),
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
                                this.Reservation.Status = ReservationStatus.ConfirmedByCustomer;
                                this.reservationRepository.Update(this.Reservation);
                                this.ShowReservationResult();
                            }
                        });
            }
            else
            {
                this.ShowAlert(this.GetText("ReservationNotConfirmed"), string.Empty);
            }
        }


        private int reservationId;
        public void Init(int id)
        {
            this.reservationId = id;
        }

        public override void Start()
        {
           base.Start();
           this.Reservation = this.reservationRepository.Get(reservationId);
        }

        private void ShowReservationResult()
        {
            this.ShowViewModel<ReservationResultViewModel>(
                new
                {
                    id = this.Reservation.Id
                });
        }
    }
}