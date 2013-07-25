//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LbkMobileServiceExtensions.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Service.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;

    using Lbk.Mobile.Data.Service.LbkMobileService;

    public static class LbkMobileServiceExtensions
    {
        //public static Task<GetEventsCompletedEventArgs> GetEventsAsyncTask(this Service1SoapClient client, string fingerprint)
        //{
        //    var tcs = CreateSource<GetEventsCompletedEventArgs>(null);
        //    client.GetEventsCompleted += (sender, e) => TransferCompletion(tcs, e, () => e, null);
        //    client.GetEventsAsync(fingerprint);
        //    return tcs.Task;
        //}

        public static Task<List<Event>> GetEventsAsyncTask(this Service1SoapClient client, string fingerprint)
        {
            var tcs = CreateSource<List<Event>>(null);
            client.GetEventsCompleted += (sender, e) => TransferCompletion(tcs, e, () => e.Result, null);
            client.GetEventsAsync(fingerprint);
            return tcs.Task;
        }

        public static Task<DishesOfTheDay> GetDishesOfTheDayAsyncTask(this Service1SoapClient client, DateTime date, string fingerprint)
        {
            var tcs = CreateSource<DishesOfTheDay>(null);
            client.TodaysMenuCompleted += (sender, e) => TransferCompletion(tcs, e, () => e.Result, null);
            client.TodaysMenuAsync(date.ToString("yyyy-MM-dd"), fingerprint);
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
            Action unregisterHandler)
        {
            if (e.UserState == tcs)
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
                if (unregisterHandler != null)
                {
                    unregisterHandler();
                }
            }
        }
    }
}