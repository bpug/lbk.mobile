//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IRoomRepository.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Repositories
{
    using System.Collections.Generic;

    using Lbk.Mobile.Common.Interfaces;
    using Lbk.Mobile.Model;

    public interface IRoomRepository
    {
        Room GetRoom(int id);

        IObservableCollection<Room> GetRooms();
    }
}