//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Reservation.Partial.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Service.LbkMobileService
{
    using System;
    using System.Xml.Serialization;

    using Cirrious.MvvmCross.Plugins.Sqlite;

    using Lbk.Mobile.Model;

    public partial class Reservation : IDbEntity
    {
        [XmlIgnore]
        public DateTime CreateAt { get; set; }

        [XmlIgnore]
        public bool Deleted { get; set; }

        [XmlIgnore]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [XmlIgnore]
        public DateTime ModifyAt { get; set; }
    }
}