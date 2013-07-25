//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ILbkMobileService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Service.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Lbk.Mobile.Data.Service.LbkMobileService;

    public interface ILbkMobileService
    {
        Task<List<Event>> GetEventsAsync(string fingerprint);

        Task<DishesOfTheDay> GetDishesOfTheDayAsync(DateTime date, string fingerprint);
    }
}