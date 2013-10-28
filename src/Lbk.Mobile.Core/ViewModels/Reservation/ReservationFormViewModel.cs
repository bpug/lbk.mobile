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

    using Lbk.Mobile.Common;
    using Lbk.Mobile.Common.Extensions;
    using Lbk.Mobile.Common.Utils;
    using Lbk.Mobile.Core.Extensions;
    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Data.Services;
    using Lbk.Mobile.Model;
    using Lbk.Mobile.Model.Enums;

    public class ReservationFormViewModel : BaseViewModel
    {
        private const int HourLimit = 1;

        private const int MaximalSeats = 10;

        private const int MinimalSeats = 2;

        private readonly ILbkMobileService lbkMobileService;

        private readonly IReservationRepository reservationRepository;

        private string advice;

        private DateTime date;

        private string guestName;

        private string mobile;

        private int seats;

        private TimeSpan time;

        public ReservationFormViewModel(
            ILbkMobileService lbkMobileService,
            IReservationRepository reservationRepository)
        {
            this.lbkMobileService = lbkMobileService;
            this.reservationRepository = reservationRepository;
            this.Errors = new ObservableDictionary<string, string>();
            this.MinSeats = this.Seats = MinimalSeats;
            this.MaxSeats = MaximalSeats;
            this.MinAllowedDate = (long)DateTime.Now.Date.GetMillisecondsSinceUnixEpoch();
            this.Time = DateTime.Now.AddHours(HourLimit).TimeOfDay;
            this.Date = DateTime.Now.Date;
        }

        public string Advice
        {
            get
            {
                return this.advice;
            }
            set
            {
                this.advice = value;
                this.RaisePropertyChanged(() => this.Advice);
                this.Validate();
            }
        }

        public DateTime Date
        {
            get
            {
                return this.date;
            }
            set
            {
                this.date = value;
                this.RaisePropertyChanged(() => this.Date);
                this.RaisePropertyChanged(() => this.ReservationTime);
            }
        }

        public ObservableDictionary<string, string> Errors { get; set; }

        public bool HasNoError
        {
            get
            {
                return Errors.Count == 0;
            }
        }

        public string GuestName
        {
            get
            {
                return this.guestName;
            }
            set
            {
                this.guestName = value;
                this.RaisePropertyChanged(() => this.GuestName);
                this.Validate();
            }
        }

        public int MaxSeats { get; private set; }

        public long MinAllowedDate { get; private set; }

        public int MinSeats { get; private set; }

        public string Mobile
        {
            get
            {
                return this.mobile;
            }
            set
            {
                this.mobile = value;
                this.RaisePropertyChanged(() => this.Mobile);
                this.Validate();
            }
        }

        public DateTime ReservationTime
        {
            get
            {
                return this.Date.Add(this.Time);
            }
        }

        public ICommand ReserveCommand
        {
            get
            {
                return new MvxCommand(async () => await this.ReserveExecute());
            }
        }

        public int Seats
        {
            get
            {
                return this.seats;
            }
            set
            {
                this.seats = value;
                this.RaisePropertyChanged(() => this.Seats);
                this.Validate();
            }
        }

        public TimeSpan Time
        {
            get
            {
                return this.time;
            }
            set
            {
                this.time = value;
                this.RaisePropertyChanged(() => this.Time);
                this.RaisePropertyChanged(() => this.ReservationTime);
            }
        }

        private async Task ReserveExecute()
        {
            var reservation = this.ToModel();
            reservation.ConfirmCode = Utility.GetRandomString(4);
            reservation.ReservationId = Guid.NewGuid().ToString();

            await this.AsyncExecute(
                () => this.lbkMobileService.CreateReservationAsync(reservation),
                result =>
                {
                    reservation.ReservationId = result.ToString();
                    reservation.Status = ReservationStatus.Requested;
                    this.reservationRepository.Update(reservation);
                    this.ShowConfirmation(reservation);
                });
        }

        private void ShowConfirmation(Reservation booking)
        {
            this.ShowViewModel<ReservationConfirmationViewModel>(new { id = booking.Id });
        }

        private void UpdateError(bool isInError, string propertyName, string errorMessage)
        {
            if (isInError)
            {
                this.Errors[propertyName] = errorMessage;
            }
            else
            {
                if (this.Errors.ContainsKey(propertyName))
                {
                    this.Errors.Remove(propertyName);
                }
            }

            this.RaisePropertyChanged(() => this.HasNoError);
        }

        private void Validate()
        {
            this.UpdateError(!this.guestName.IsName(), "GuestName", "Please enter a Name");
            this.UpdateError(!this.mobile.IsPhone(), "Mobile", "Please enter a valid Mobile");
            //UpdateError(!reservationTime.HasValue, "ReservationTime", "Please enter a Reservation Time");
            this.UpdateError(this.seats < 1, "Seats", "Please enter a number of Seats");
        }
    }
}