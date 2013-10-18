//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ILbkMobileService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Lbk.Mobile.Model;

    using Event = Lbk.Mobile.Model.Event;
    using Picture = Lbk.Mobile.Model.Picture;
    using Quiz = Lbk.Mobile.Model.Quiz;
    using Video = Lbk.Mobile.Model.Video;

    public interface ILbkMobileService
    {
        Task<bool> AbortedReservationByCustomerAsync(string reservationId);

        Task<bool> ActivateVoucherAsync(QuizVoucher voucher);

        Task<Guid> CreateReservationAsync(Reservation reservation);

        Task<List<Event>> GetEventsAsync();

        Task<DateTime?> GetMenuLastUpdateAsync();

        Task<List<Picture>> GetPicturesAsync();

        Task<Quiz> GetQuizAsync(int questionCount);

        Task<List<MenuCategory>> GetTodaysMenuAsync(DateTime date);

        Task<List<Video>> GetVideosAsyn();

        Task<bool> IsDeclinedReservationByRestaurantAsyn(string reservationId);

        Task<bool> ConfirmedReservationByCustomerAsync(string reservationId);
    }
}