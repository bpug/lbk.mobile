// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseEntity.cs" company="ip-connect GmbH">
//   Copyright (c) ip-connect GmbH. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Portable.Core.Model
{
    using System;

    using Cirrious.MvvmCross.Plugins.Sqlite;

    public class BaseEntity
    {
        public virtual DateTime CreateAt { get; set; }

        public virtual bool Deleted { get; set; }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public virtual DateTime ModifyAt { get; set; }
    }
}