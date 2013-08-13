//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ILbkMobileService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Lbk.Mobile.Data.LbkMobileService;

    public interface ILbkMobileService
    {
        Task<bool> ActivateVoucherAsync(Model.QuizVoucher voucher);

        Task<Guid> CreateReservationAsync(Reservation reservation);

        List<Event> GetEvents();

        Task<List<Event>> GetEventsAsync();

        Task<DateTime?> GetMenuLastUpdateAsync();

        Task<List<Picture>> GetPicturesAsync();

        Task<Quiz> GetQuizAsync(int questionCount);

        Task<DishesOfTheDay> GetTodaysMenuAsync(DateTime date);

        Task<List<Video>> GetVideosAsyn();
    }
}