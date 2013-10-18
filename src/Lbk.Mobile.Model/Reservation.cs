//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Reservation.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Model
{
    using System;

    using Lbk.Mobile.Model.Enums;

    public class Reservation : BaseDbEntity
    {
        public string Advice { get; set; }
        public string ConfirmCode { get; set; }
        public string DeclineReason { get; set; }
        public string FingerPrint { get; set; }
        public string GuestName { get; set; }
        public DateTime LastChange { get; set; }
        public string Mobile { get; set; }
        public string ReservationId { get; set; }
        public DateTime? ReservationTime { get; set; }
        public int Seats { get; set; }
        public ReservationStatus Status { get; set; }
    }
}