//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LbkMobileServiceExtensions.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;

    using Lbk.Mobile.Data.LbkMobileService;
    using Lbk.Mobile.Model;

    using Reservation = Lbk.Mobile.Data.LbkMobileService.Reservation;

    public static class LbkMobileServiceExtensions
    {
        //public static Task<GetEventsCompletedEventArgs> GetEventsAsyncTask(this Service1SoapClient client, string fingerprint)
        //{
        //    var tcs = CreateSource<GetEventsCompletedEventArgs>(null);
        //    client.GetEventsCompleted += (sender, e) => TransferCompletion(tcs, e, () => e, null);
        //    client.GetEventsAsync(fingerprint);
        //    return tcs.Task;
        //}

        //public static Task<string> CreateReservationAsyncTask(
        //    this Service1SoapClient client,
        //    string fingerprint,
        //    string when,
        //    int seats,
        //    string mobile,
        //    string name,
        //    string advice,
        //    string confitmcode)
        //{
        //    var tcs = CreateSource<string>(null);
        //    client.CreateReservationCompleted += (sender, e) => TransferCompletion(tcs, e, () => e.Result, null);
        //    client.CreateReservationAsync(when, seats, mobile, fingerprint, name, advice, confitmcode);
        //    return tcs.Task;
        //}


        public static Task<bool> ActivateVoucherAsyncTask(this Service1SoapClient client, QuizVoucher voucher, string fingerprint)
        {
            var tcs = CreateSource<bool>(null);
            client.ActivateVoucherCompleted += (sender, e) => TransferCompletion(tcs, e, () => e.Result, null);
            client.ActivateVoucherAsync(fingerprint, voucher.QuizId, voucher.Code);
            return tcs.Task;
        }

        public static Task<Guid> CreateReservationAsyncTask(this Service1SoapClient client, Reservation reservation)
        {
            var tcs = CreateSource<Guid>(null);
            client.CreateReservationByObjectCompleted += (sender, e) => TransferCompletion(tcs, e, () => e.Result, null);
            client.CreateReservationByObjectAsync(reservation);
            return tcs.Task;
        }

        public static Task<DishesOfTheDay> TodaysMenuAsyncTask(
            this Service1SoapClient client,
            DateTime date,
            string fingerprint)
        {
            var tcs = CreateSource<DishesOfTheDay>(null);
            client.TodaysMenuCompleted += (sender, e) => TransferCompletion(tcs, e, () => e.Result, null);
            client.TodaysMenuAsync(date.ToString("yyyy-MM-dd"), fingerprint);
            return tcs.Task;
        }

        public static Task<List<Event>> GetEventsAsyncTask(this Service1SoapClient client, string fingerprint)
        {
            var tcs = CreateSource<List<Event>>(null);
            client.GetEventsCompleted += (sender, e) => TransferCompletion(tcs, e, () => e.Result.ToList(), null);
            client.GetEventsAsync(fingerprint);
            return tcs.Task;
        }

        public static Task<DateTime?> GetMenuLastUpdateAsyncTask(this Service1SoapClient client, string fingerprint)
        {
            var tcs = CreateSource<DateTime?>(null);
            client.GetMenuLastUpdateCompleted += (sender, e) => TransferCompletion(tcs, e, () => e.Result, null);
            client.GetMenuLastUpdateAsync(fingerprint);
            return tcs.Task;
        }

        public static Task<List<Picture>> GetPicturesAsyncTask(this Service1SoapClient client, string fingerprint)
        {
            var tcs = CreateSource<List<Picture>>(null);
            client.GetPicturesCompleted += (sender, e) => TransferCompletion(tcs, e, () => e.Result.ToList(), null);
            client.GetPicturesAsync(fingerprint);
            return tcs.Task;
        }

        public static Task<Question> GetQuestionAsyncTask(this Service1SoapClient client, int questionCount)
        {
            var tcs = CreateSource<Question>(null);
            client.GetQuestionCompleted += (sender, e) => TransferCompletion(tcs, e, () => e.Result, null);
            client.GetQuestionAsync(questionCount);
            return tcs.Task;
        }

        public static Task<Quiz> GetQuizAsyncTask(this Service1SoapClient client, string fingerprint, int questionCount)
        {
            var tcs = CreateSource<Quiz>(null);
            client.GetQuizCompleted += (sender, e) => TransferCompletion(tcs, e, () => e.Result, null);
            client.GetQuizAsync(fingerprint, questionCount);
            return tcs.Task;
        }

        public static Task<List<Video>> GetVideosAsyncTask(this Service1SoapClient client, string fingerprint)
        {
            var tcs = CreateSource<List<Video>>(null);
            client.GetVideosCompleted += (sender, e) => TransferCompletion(tcs, e, () => e.Result.ToList(), null);
            client.GetEventsAsync(fingerprint);
            return tcs.Task;
        }

        private static TaskCompletionSource<T> CreateSource<T>(object state)
        {
            return new TaskCompletionSource<T>(state, TaskCreationOptions.None);
        }

        private static void TransferCompletion<T>(
            TaskCompletionSource<T> tcs,
            AsyncCompletedEventArgs e,
            Func<T> getResult,
            Action handler)
        {
            if (e.Cancelled)
            {
                tcs.TrySetCanceled();
            }
            else if (e.Error != null)
            {
                tcs.TrySetException(e.Error);
            }
            else
            {
                tcs.TrySetResult(getResult());
            }
            if (handler != null)
            {
                handler();
            }
        }
    }
}