//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IRoomRepository.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Repositories
{
    using System;
    using Lbk.Mobile.Common.Interfaces;
    using Lbk.Mobile.Model;

    public interface IRoomRepository
    {
        void GetRoom(int id, Action<Room> onSuccess, Action<Exception> onError);
        void GetRooms(Action<IObservableCollection<Room>> onSuccess, Action<Exception> onError);

        //Room GetRoom(int id);
        //IObservableCollection<Room> GetRooms();
    }
}