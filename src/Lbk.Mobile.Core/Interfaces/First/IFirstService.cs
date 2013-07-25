//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IFirstService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Interfaces.First
{
    using System;
    using System.Collections.Generic;

    public interface IFirstService
    {
        void GetItems(string key, Action<List<SimpleItem>> onSuccess, Action<FirstServiceError> onError);
    }
}