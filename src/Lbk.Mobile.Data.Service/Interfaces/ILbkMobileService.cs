﻿//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ILbkMobileService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Service.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Lbk.Mobile.Data.Service.LbkMobileService;

    public interface ILbkMobileService
    {
        Task<DishesOfTheDay> GetDishesOfTheDayAsync(DateTime date);

        Task<List<Event>> GetEventsAsync();

        Task<DateTime?> GetMenuLastUpdateAsync();

        Task<List<Picture>> GetPicturesAsync();

        Task<Quiz> GetQuizAsync(int questionCount);

        Task<Guid> CreateReservationAsync(Reservation reservation);

        Task<List<Video>> GetVideosAsyn();
    }
}