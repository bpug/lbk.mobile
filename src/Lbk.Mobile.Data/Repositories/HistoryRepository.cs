//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="HistoryRepositoryNew.cs" company="ip-connect GmbH">
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

    public class HistoryRepository : MvxLockableObject, IHistoryRepository
    {
        //private readonly IMvxFileStore fileStore;

        private readonly IMvxJsonConverter jsonConverter;

        private readonly IMvxResourceLoader resourceLoader;

        public HistoryRepository(IMvxResourceLoader resourceLoader, IMvxJsonConverter jsonConverter)
        {
            this.resourceLoader = resourceLoader;
            this.jsonConverter = jsonConverter;
        }

        public void GetHistories(Action<IObservableCollection<History>> onSuccess, Action<Exception> onError)
        {
            this.RunAsyncWithLock(
                () =>
                {
                    if (this.resourceLoader.ResourceExists(Constants.RoomResourceJFileName))
                    {
                        string json = this.resourceLoader.GetTextResource(Constants.HistoryResourcejFileName);
                        var histories = this.jsonConverter.DeserializeObject<List<History>>(json);
                        var observableRooms = new SimpleObservableCollection<History>(histories);
                        onSuccess(observableRooms);
                    }
                    else
                    {
                        onSuccess(null);
                    }
                });
        }

        public void GetHistory(int id, Action<History> onSuccess, Action<Exception> onError)
        {
            this.GetHistories(
                list =>
                {
                    var picture = list.FirstOrDefault(p => p.PageIndex == id);
                    onSuccess(picture);
                },
                onError);
        }

        //public void Save(List<History> histories)
        //{

        //    this.RunAsyncWithLock(
        //        () =>
        //        {
        //            string json = this.jsonConverter.SerializeObject(histories);
        //            this.fileStore.WriteFile(Constants.HistoryResourcejFileName, json);
        //        });
        //}

        //public IObservableCollection<History> GetHistories()
        //{
        //    var histories = XmlSerializer<List<History>>.Load(Constants.HistoryResourceFileName);
        //    return new SimpleObservableCollection<History>(histories);
        //}

        //public History GetHistory(int id)
        //{
        //    return this.GetHistories().FirstOrDefault(p => p.PageIndex == id);
        //}
    }
}