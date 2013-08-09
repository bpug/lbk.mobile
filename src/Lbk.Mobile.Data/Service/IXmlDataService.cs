//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IResourceDataStoreService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Service
{
    using Lbk.Mobile.Infrastructure.Interfaces;
    using Lbk.Mobile.Model;

    public interface IXmlDataService
    {
        IObservableCollection<History> GetHistories();

        IObservableCollection<Room> GetRooms();
    }
}