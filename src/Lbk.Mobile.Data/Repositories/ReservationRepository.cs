//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ReservationDataService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Cirrious.MvvmCross.Plugins.Sqlite;

    using Lbk.Mobile.Model;
    using Lbk.Mobile.Model.Enums;

    public class ReservationRepository : RepositoryBase, IReservationRepository
    {
        public ReservationRepository(ISQLiteConnectionFactory factory)
            : base(factory)
        {
            this.Connection.CreateTable<Reservation>();
        }

        public void Delete(Reservation reservation)
        {
            reservation.Deleted = true;
            this.Connection.Update(reservation);
        }

        public Reservation Get(string reservationId)
        {
            return
                this.Connection.Table<Reservation>()
                    .FirstOrDefault(p => (p.Deleted == false && p.ReservationId == reservationId));
        }

        public Reservation Get(int id)
        {
            return
                this.Connection.Table<Reservation>()
                    .FirstOrDefault(p => (p.Deleted == false && p.Id == id));
        }

        public IEnumerable<Reservation> GetAll()
        {
            return this.Connection.Table<Reservation>().Where(p => (p.Deleted == false)).ToList();
        }

        public Reservation GetRequested()
        {
            return
                this.Connection.Table<Reservation>()
                    .FirstOrDefault(p => (p.Deleted == false && p.Status ==  ReservationStatus.Requested));
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