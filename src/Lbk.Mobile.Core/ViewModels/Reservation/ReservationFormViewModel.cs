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
    using Lbk.Mobile.Core.Extensions;
    using Lbk.Mobile.Model;
    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Data.Services;

    public class ReservationFormViewModel : BaseViewModel
    {
        private const int HourLimit = 1;

        
        private readonly ILbkMobileService lbkMobileService;

        private readonly IReservationRepository reservationRepository;

        public ObservableDictionary<string, string> Errors { get; set; }

        public ReservationFormViewModel(
            ILbkMobileService lbkMobileService,
            IReservationRepository reservationRepository)
        {
            this.lbkMobileService = lbkMobileService;
            this.reservationRepository = reservationRepository;
            Errors = new ObservableDictionary<string, string>();
        }

        
        public int MinSeats
        {
            get
            {
                return 2;
            }
        }
        
        public int MaxSeats
        {
            get
            {
                return 10;
            }
        }

        public long MinAllowedDate
        {
            get
            {
                return (long)DateTime.Now.Date.GetMillisecondsSinceUnixEpoch();
            }
        }

        //public Reservation Reservation { get; set; }

        private string guestName;
        public string GuestName
        {
            get { return guestName; }
            set { guestName = value; RaisePropertyChanged(() => GuestName); Validate(); }
        }

        private string mobile;
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; RaisePropertyChanged(() => Mobile); Validate(); }
        }

        private int seats = 2;
        public int Seats
        {
            get { return seats; }
            set { seats = value; RaisePropertyChanged(() => Seats); Validate(); }
        }

        private string advice;
        public string Advice
        {
            get { return advice; }
            set { advice = value; RaisePropertyChanged(() => Advice); Validate(); }
        }

       


        //private DateTime reservationTime = DateTime.Now.AddHours(HourLimit);
        //public DateTime ReservationTime
        //{
        //    get { return reservationTime; }
        //    set { reservationTime = value; RaisePropertyChanged(() => ReservationTime); Validate(); }
        //}

        public DateTime ReservationTime
        {
            get
            {
                var d = this.date.Add(this.time);
                return d;
            }
            
        }

        private TimeSpan time = DateTime.Now.TimeOfDay;
        public TimeSpan Time
        {
            get { return time; }
            set { time = value; RaisePropertyChanged(() => Time); RaisePropertyChanged(() => ReservationTime); }
        }

        private DateTime date = DateTime.Now.Date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; RaisePropertyChanged(() => Date); RaisePropertyChanged(() => ReservationTime); }
        }


        public ICommand ReserveCommand
        {
            get
            {
                return new MvxCommand(async() => await this.ReserveExecute());
            }
        }

        private async Task ReserveExecute()
        {
            var reservation = this.ToModel();
            await this.AsyncExecute(
                () => this.lbkMobileService.CreateReservationAsync(reservation),
                result =>
                {
                    reservation.ReservationId = result.ToString();
                    this.reservationRepository.Update(reservation);
                    this.ShowReservationResult(reservation);
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


        private void Validate()
        {
            UpdateError(guestName.IsEmpty(), "GuestName", "Please enter a Name");
            UpdateError(mobile.IsEmpty(), "Mobile", "Please enter a valid Mobile");
            //UpdateError(!reservationTime.HasValue, "ReservationTime", "Please enter a Reservation Time");
            UpdateError(seats < 1, "Seats", "Please enter a number of Seats");
        }

        private void UpdateError(bool isInError, string propertyName, string errorMessage)
        {
            if (isInError)
            {
                Errors[propertyName] = errorMessage;
            }
            else
            {
                if (Errors.ContainsKey(propertyName))
                    Errors.Remove(propertyName);
            }
        }
    }
}