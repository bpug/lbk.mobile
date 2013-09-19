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

    using Lbk.Mobile.Data.Extensions;
    using Lbk.Mobile.Data.LbkMobileService;
    using Lbk.Mobile.Model;
    using Lbk.Mobile.Plugin.DeviceIdentifier;

    using Event = Lbk.Mobile.Model.Event;
    using Picture = Lbk.Mobile.Model.Picture;
    using Quiz = Lbk.Mobile.Data.LbkMobileService.Quiz;
    using Video = Lbk.Mobile.Model.Video;

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

        public Task<bool> AbortedReservationByCustomerAsync(Guid reservationId)
        {
            return this.Service.AbortedReservationByCustomerAsyncTask(reservationId);
        }

        public Task<bool> ActivateVoucherAsync(QuizVoucher voucher)
        {
            return this.Service.ActivateVoucherAsyncTask(voucher, this.deviceUid);
        }

        public Task<bool> ConfirmedReservationByCustomerAsync(Guid reservationId)
        {
            return this.Service.ConfirmReservationByCustomerAsyncTask(reservationId);
        }

        public Task<Guid> CreateReservationAsync(Reservation reservation)
        {
            return this.Service.CreateReservationAsyncTask(reservation);
        }

        public Task<List<Event>> GetEventsAsync()
        {
            return this.Service.GetEventsAsyncTask(this.deviceUid);
        }

        public Task<DateTime?> GetMenuLastUpdateAsync()
        {
            var result = this.Service.GetMenuLastUpdateAsyncTask(this.deviceUid);
            return result;
        }

        public Task<List<Picture>> GetPicturesAsync()
        {
            return this.Service.GetPicturesAsyncTask(this.deviceUid);
        }

        public Task<Quiz> GetQuizAsync(int questionCount)
        {
            return this.Service.GetQuizAsyncTask(this.deviceUid, questionCount);
        }

        public Task<List<MenuCategory>> GetTodaysMenuAsync(DateTime date)
        {
            var result = this.Service.TodaysMenuAsyncTask(date, this.deviceUid);
            return result;
        }

        public Task<List<Video>> GetVideosAsyn()
        {
            return this.Service.GetVideosAsyncTask(this.deviceUid);
        }

        public Task<bool> IsDeclinedReservationByRestaurantAsyn(Guid reservationId)
        {
            return this.Service.IsDeclinedByRestaurantAsyncTask(reservationId);
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