//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="RoomRepository.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Lbk.Mobile.Common;
    using Lbk.Mobile.Common.Interfaces;
    using Lbk.Mobile.Data.Utility;
    using Lbk.Mobile.Model;

    public class RoomRepository : XmlRepositoryBase, IRoomRepository
    {
        public Room GetRoom(int id)
        {
            var room = this.GetRooms().FirstOrDefault(p => p.Id == id);
            return room;
        }

        public IObservableCollection<Room> GetRooms()
        {
            var rooms = XmlSerializer<List<Room>>.Load(Constants.RoomResourceFileName);
            return new SimpleObservableCollection<Room>(rooms);
        }
    }
}