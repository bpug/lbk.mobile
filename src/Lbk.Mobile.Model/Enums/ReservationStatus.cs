//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ReservationStatus.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Model.Enums
{
    public enum ReservationStatus
    {
        None,

        Requested,

        ConfirmedByRestaurant,

        ConfirmedByCustomer,

        DeclinedByRestaurant,

        DeclinedAfterConfirmedByRestaurant,

        AbortedByCustomer,
    }
}