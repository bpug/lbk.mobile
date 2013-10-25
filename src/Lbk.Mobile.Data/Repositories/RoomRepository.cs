//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="RoomRepository.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Cirrious.CrossCore.Core;
    using Cirrious.CrossCore.Platform;

    using Lbk.Mobile.Common;
    using Lbk.Mobile.Common.Interfaces;
    using Lbk.Mobile.Model;

    public class RoomRepository : MvxLockableObject, IRoomRepository
    {
        //private readonly IMvxFileStore fileStore;

        private readonly IMvxJsonConverter jsonConverter;

        private readonly IMvxResourceLoader resourceLoader;

        public RoomRepository(IMvxResourceLoader resourceLoader, IMvxJsonConverter jsonConverter)
        {
            this.jsonConverter = jsonConverter;
            this.resourceLoader = resourceLoader;
        }

        public void GetRoom(int id, Action<Room> onSuccess, Action<Exception> onError)
        {
            this.GetRooms(
                list =>
                {
                    var picture = list.FirstOrDefault(p => p.Id == id);
                    onSuccess(picture);
                },
                onError);
        }

        public void GetRooms(Action<IObservableCollection<Room>> onSuccess, Action<Exception> onError)
        {
            this.RunAsyncWithLock(
                () =>
                {
                    if (this.resourceLoader.ResourceExists(Constants.RoomResourceJFileName))
                    {
                        string json = this.resourceLoader.GetTextResource(Constants.RoomResourceJFileName);
                        var rooms = this.jsonConverter.DeserializeObject<List<Room>>(json);
                        var observableRooms = new SimpleObservableCollection<Room>(rooms);
                        onSuccess(observableRooms);
                    }
                    else
                    {
                        onSuccess(null);
                    }
                });
        }

        //public void Save(List<Room> rooms)
        //{
        //    this.RunAsyncWithLock(
        //        () =>
        //        {
        //            string json = this.jsonConverter.SerializeObject(rooms);
        //            this.fileStore.WriteFile(Constants.RoomResourceJFileName, json);
        //        });
        //}

        //public Room GetRoom(int id)
        //{
        //    var room = this.GetRooms().FirstOrDefault(p => p.Id == id);
        //    return room;
        //}

        //public IObservableCollection<Room> GetRooms()
        //{
        //    var rooms = XmlSerializer<List<Room>>.Load(Constants.RoomResourceFileName);
        //    if (rooms != null)
        //    {
        //        return new SimpleObservableCollection<Room>(rooms);
        //    }
        //    return null;
        //}
    }
}