//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Reservation.Partial.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Service.LbkMobileService
{
    using System;

    using Lbk.Mobile.Model;

    public partial class Reservation : IDbEntity
    {
        public DateTime CreateAt { get; set; }
        public bool Deleted { get; set; }
        public int Id { get; set; }
        public DateTime ModifyAt { get; set; }
    }
}