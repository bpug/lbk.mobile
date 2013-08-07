//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LbkMobileService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Service.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Cirrious.MvvmCross.Plugins.DeviceIdentifier.Interfaces;

    using Lbk.Mobile.Data.Service.Extensions;
    using Lbk.Mobile.Data.Service.Interfaces;
    using Lbk.Mobile.Data.Service.LbkMobileService;

    public class LbkMobileService : LbkMobileServiceBase<Service1SoapClient>, ILbkMobileService
    {
        private readonly IDeviceUidService deviceUidService;

        private string deviceUid;

        public LbkMobileService(IDeviceUidService deviceUidService)
        {
            this.deviceUidService = deviceUidService;

            this.GetDeviceUid();
        }

        public async Task<DishesOfTheDay> GetTodaysMenuAsync(DateTime date)
        {
            var result = await this.Service.TodaysMenuAsyncTask(date, this.deviceUid);
            return result;
        }

        public async Task<List<Event>> GetEventsAsync()
        {
            return await this.Service.GetEventsAsyncTask(this.deviceUid);
        }

        public async Task<DateTime?> GetMenuLastUpdateAsync()
        {
            return await this.Service.GetMenuLastUpdateAsyncTask(this.deviceUid);
        }

        public async Task<List<Picture>> GetPicturesAsync()
        {
            return await this.Service.GetPicturesAsyncTask(this.deviceUid);
        }

        public async Task<Quiz> GetQuizAsync(int questionCount)
        {
            return await this.Service.GetQuizAsyncTask(this.deviceUid, questionCount);
        }

        public async Task<Guid> CreateReservationAsync(Reservation reservation)
        {
            return await this.Service.CreateReservationAsyncTask(reservation);
        }

        public async Task<List<Video>> GetVideosAsyn()
        {
            return await this.Service.GetVideosAsyncTask(this.deviceUid);
        }

        private void GetDeviceUid()
        {
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