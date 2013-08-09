//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DataServiceBase.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Service
{
    using Cirrious.MvvmCross.Plugins.Sqlite;

    public abstract class DataServiceBase
    {
        protected readonly ISQLiteConnection Connection;

        protected DataServiceBase(ISQLiteConnectionFactory factory)
        {
            this.Connection = factory.Create(Constants.DbName);
        }
    }
}