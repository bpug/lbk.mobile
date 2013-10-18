//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ReservationMapping.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Mappings
{
    using System;

    using Lbk.Mobile.Data.LbkMobileService;
    using Lbk.Mobile.Model.Enums;

    using Reservation = Lbk.Mobile.Model.Reservation;

    public static class ReservationMapping
    {
        public static Reservation ToModel(this LbkMobileService.Reservation source)
        {
            var question = new Reservation()
            {
                Advice = source.Advice,
                ConfirmCode = source.ConfirmCode,
                DeclineReason = source.DeclineReason,
                FingerPrint = source.Fingerprint,
                GuestName = source.GuestName,
                LastChange = source.LastChange,
                Mobile = source.Mobile,
                ReservationId = source.ReservationId.ToString(),
                ReservationTime = source.ReservationTime,
                Seats = source.Seats,
                Status = (ReservationStatus)source.Status
            };

            return question;
        }

        public static LbkMobileService.Reservation ToServiceModel(this Reservation source)
        {
            var reservation = new LbkMobileService.Reservation()
            {
                Advice = source.Advice,
                ConfirmCode = source.ConfirmCode,
                DeclineReason = source.DeclineReason,
                Fingerprint = source.FingerPrint,
                GuestName = source.GuestName,
                LastChange = source.LastChange,
                Mobile = source.Mobile,
                ReservationId = new Guid(source.ReservationId),
                
                Seats = source.Seats,
                Status = (StatusArt)source.Status
            };

            if (source.ReservationTime != null)
            {
                reservation.ReservationTime = source.ReservationTime.Value;
            }

            return reservation;
        }
    }
}