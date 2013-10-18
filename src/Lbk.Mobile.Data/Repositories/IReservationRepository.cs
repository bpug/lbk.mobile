//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IReservationRepository.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Repositories
{
    using System.Collections.Generic;

    using Lbk.Mobile.Model;

    public interface IReservationRepository
    {
        void Delete(Reservation reservation);

        Reservation Get(string reservationId);

        IEnumerable<Reservation> GetAll();

        Reservation GetRequested();

        void Update(Reservation voucher);
    }
}