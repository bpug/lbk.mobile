//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ReservationViewModelExtensions.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Extensions
{
    using Lbk.Mobile.Core.ViewModels.Reservation;
    using Lbk.Mobile.Model;

    public static class ReservationViewModelExtensions
    {
        public static Reservation ToModel(this ReservationFormViewModel source)
        {
            var result = new Reservation
            {
                Seats = source.Seats,
                ReservationTime = source.ReservationTime,
                GuestName = source.GuestName,
                Advice = source.Advice,
                Mobile = source.Mobile
            };
            return result;
        }
    }
}