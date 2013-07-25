//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FirstService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Lbk.Mobile.Core.Interfaces.First;

    public class FirstService : IFirstService
    {
        public void GetItems(string key, Action<List<SimpleItem>> onSuccess, Action<FirstServiceError> onError)
        {
            ThreadPool.QueueUserWorkItem(
                ignored =>
                {
                    if (string.IsNullOrWhiteSpace(key))
                    {
                        onError(FirstServiceError.ErrorEmptyKey);
                        return;
                    }

                    var success = new List<SimpleItem>();
                    for (int i = 0; i < 10; i++)
                    {
                        success.Add(
                            new SimpleItem()
                            {
                                Id = i,
                                Title = "Title " + i + "(" + key + ")",
                                Notes =
                                    string.Format(
                                        "This item returned from {0} - here's a GUID: {1}",
                                        key,
                                        Guid.NewGuid().ToString("N")),
                                When = DateTime.UtcNow.AddMinutes(-i)
                            });
                    }
                    onSuccess(success);
                });
        }
    }
}