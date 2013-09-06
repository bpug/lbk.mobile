//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LbkMobileService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Cirrious.CrossCore;

    using Lbk.Mobile.Data.Extensions;
    using Lbk.Mobile.Data.LbkMobileService;
    using Lbk.Mobile.Model;
    using Lbk.Mobile.Plugin.DeviceIdentifier;

    using Quiz = Lbk.Mobile.Data.LbkMobileService.Quiz;

    public class LbkMobileService : LbkMobileServiceBase<Service1SoapClient>, ILbkMobileService
    {
        private readonly IDeviceUidService deviceUidService;

        private string deviceUid;

        public LbkMobileService(IDeviceUidService deviceUidService)
        {
            this.deviceUidService = deviceUidService;

            this.GetDeviceUid();
        }

        //public LbkMobileService()
        //{
        //    this.deviceUidService = Mvx.Resolve<IDeviceUidService>();
        //    this.GetDeviceUid();
        //}

        public async Task<bool> AbortedReservationByCustomerAsync(Guid reservationId)
        {
            return await this.Service.AbortedReservationByCustomerAsyncTask(reservationId);
        }

        public async Task<bool> ActivateVoucherAsync(QuizVoucher voucher)
        {
            return await this.Service.ActivateVoucherAsyncTask(voucher, this.deviceUid);
        }

        public async Task<bool> ConfirmedReservationByCustomerAsync(Guid reservationId)
        {
            return await this.Service.ConfirmReservationByCustomerAsyncTask(reservationId);
        }

        public async Task<Guid> CreateReservationAsync(Reservation reservation)
        {
            return await this.Service.CreateReservationAsyncTask(reservation);
        }

       
        public async Task<List<Event>> GetEventsAsync()
        {
            return await this.Service.GetEventsAsyncTask(this.deviceUid);
        }

        public async Task<DateTime?> GetMenuLastUpdateAsync()
        {
            var result = await this.Service.GetMenuLastUpdateAsyncTask(this.deviceUid);
            return result;
        }

        public async Task<List<Picture>> GetPicturesAsync()
        {
            return await this.Service.GetPicturesAsyncTask(this.deviceUid);
        }

        public async Task<Quiz> GetQuizAsync(int questionCount)
        {
            return await this.Service.GetQuizAsyncTask(this.deviceUid, questionCount);
        }

        public  Task<DishesOfTheDay> GetTodaysMenuAsync(DateTime date)
        {
            var result = this.Service.TodaysMenuAsyncTask(date, this.deviceUid);
            return result;
        }

        public async Task<List<Video>> GetVideosAsyn()
        {
            return await this.Service.GetVideosAsyncTask(this.deviceUid);
        }

        public async Task<bool> IsDeclinedReservationByRestaurantAsyn(Guid reservationId)
        {
            return await this.Service.IsDeclinedByRestaurantAsyncTask(reservationId);
        }

        private void GetDeviceUid()
        {
            //this.deviceUid = "Droid Test";
            //this.deviceUid = deviceUidService.GetDeviceUid();
            this.deviceUidService.GetDeviceUid(onSuccess: this.OnDeviceUidSuccess, onError: this.OnDeviceUidError);
        }

        private void OnDeviceUidError(Exception ex)
        {
            this.deviceUid = Guid.NewGuid().ToString();
        }

        private void OnDeviceUidSuccess(string uid)
        {
            this.deviceUid = uid;
        }
    }
}