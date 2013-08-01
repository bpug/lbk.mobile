//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IResourceDataStoreService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Service.Interfaces
{
    using Lbk.Mobile.Infrastructure.Interfaces;
    using Lbk.Mobile.Model;

    public interface IDataStoreService
    {
        IObservableCollection<History> GetHistories();

        IObservableCollection<Room> GetRooms();
    }
}