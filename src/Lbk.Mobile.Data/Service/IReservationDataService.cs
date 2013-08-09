//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IReservationDataService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Service
{
    using System;
    using System.Collections.Generic;

    using Lbk.Mobile.Data.LbkMobileService;

    public interface IReservationDataService
    {
        void Delete(Reservation reservation);

        Reservation Get(Guid reservationId);

        IEnumerable<Reservation> GetAll();

        Reservation GetRequested();

        void Update(Reservation voucher);
    }
}