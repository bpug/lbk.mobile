//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DataServiceBase.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Repositories
{
    using Cirrious.MvvmCross.Plugins.Sqlite;

    public abstract class RepositoryBase
    {
        protected readonly ISQLiteConnection Connection;

        protected RepositoryBase(ISQLiteConnectionFactory factory)
        {
            this.Connection = factory.Create(Constants.DbName);
        }
    }
}