//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ServiceExtensions.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace SoapTest
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;

    using SoapTest.MyService3;

    public static class ServiceExtensions
    {
        public static Task<AsyncCompletedEventArgs> EventAsyncTask(this Service1SoapClient client, string key)
        {
            var tcs = CreateSource<AsyncCompletedEventArgs>(null);
            client.GetEventsCompleted += (sender, e) => TransferCompletion(tcs, e, () => e, null);
            client.GetEventsAsync(key);
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