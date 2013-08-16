//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IReservationDataService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Repositories
{
    using System;
    using System.Collections.Generic;

    using Lbk.Mobile.Data.LbkMobileService;

    public interface IReservationRepository
    {
        void Delete(Reservation reservation);

        Reservation Get(Guid reservationId);

        IEnumerable<Reservation> GetAll();

        Reservation GetRequested();

        void Update(Reservation voucher);
    }
}