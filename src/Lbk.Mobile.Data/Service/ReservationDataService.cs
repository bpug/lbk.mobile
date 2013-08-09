//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ReservationDataService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Cirrious.MvvmCross.Plugins.Sqlite;

    using Lbk.Mobile.Data.LbkMobileService;

    public class ReservationDataService : DataServiceBase, IReservationDataService
    {
        public ReservationDataService(ISQLiteConnectionFactory factory)
            : base(factory)
        {
            this.Connection.CreateTable<Reservation>();
        }

        public void Delete(Reservation reservation)
        {
            reservation.Deleted = true;
            this.Connection.Update(reservation);
        }

        public Reservation Get(Guid reservationId)
        {
            return
                this.Connection.Table<Reservation>()
                    .FirstOrDefault(p => (p.Deleted == false && p.ReservationId == reservationId));
        }

        public IEnumerable<Reservation> GetAll()
        {
            return this.Connection.Table<Reservation>().Where(p => (p.Deleted == false)).ToList();
        }

        public Reservation GetRequested()
        {
            return
                this.Connection.Table<Reservation>()
                    .FirstOrDefault(p => (p.Deleted == false && p.Status == StatusArt.Requested));
        }

        public void Update(Reservation reservation)
        {
            reservation.ModifyAt = DateTime.Now;
            if (reservation.Id == 0)
            {
                reservation.CreateAt = DateTime.Now;
                this.Connection.Insert(reservation);
            }
            else
            {
                this.Connection.Update(reservation);
            }
        }
    }
}