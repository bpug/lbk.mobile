//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BaseEntity.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Model
{
    using System;

    using Cirrious.MvvmCross.Plugins.Sqlite;

    public interface IDbEntity
    {
        DateTime CreateAt { get; set; }

        bool Deleted { get; set; }

        [PrimaryKey, AutoIncrement]
        int Id { get; set; }

        DateTime ModifyAt { get; set; }
    }
}