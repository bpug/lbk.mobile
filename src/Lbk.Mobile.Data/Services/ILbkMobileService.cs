﻿//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ILbkMobileService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Lbk.Mobile.Data.LbkMobileService;
    using Lbk.Mobile.Model;

    using Quiz = Lbk.Mobile.Data.LbkMobileService.Quiz;

    public interface ILbkMobileService
    {
        Task<bool> AbortedReservationByCustomerAsync(Guid reservationId);

        Task<bool> ActivateVoucherAsync(QuizVoucher voucher);

        Task<Guid> CreateReservationAsync(Reservation reservation);

        Task<List<Event>> GetEventsAsync();

        Task<DateTime?> GetMenuLastUpdateAsync();

        Task<List<Picture>> GetPicturesAsync();

        Task<Quiz> GetQuizAsync(int questionCount);

        Task<DishesOfTheDay> GetTodaysMenuAsync(DateTime date);

        Task<List<Video>> GetVideosAsyn();

        Task<bool> IsDeclinedReservationByRestaurantAsyn(Guid reservationId);

        Task<bool> ConfirmedReservationByCustomerAsync(Guid reservationId);
    }
}