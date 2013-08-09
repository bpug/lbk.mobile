//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ResourceDataStoreServiceService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Service
{
    using System.Collections.Generic;

    using Cirrious.CrossCore;
    using Cirrious.CrossCore.Platform;

    using Lbk.Mobile.Common;
    using Lbk.Mobile.Common.Interfaces;
    using Lbk.Mobile.Data.Utility;
    using Lbk.Mobile.Model;

    public class XmlDataService : IXmlDataService
    {
        private IMvxResourceLoader resourceLoader;

        public IMvxResourceLoader ResourceLoader
        {
            get
            {
                return this.resourceLoader ?? (this.resourceLoader = Mvx.Resolve<IMvxResourceLoader>());
            }
        }

        public IObservableCollection<History> GetHistories()
        {
            var histories = XmlSerializer<List<History>>.Load(Constants.HistoryResourceFileName);
            return new SimpleObservableCollection<History>(histories);
        }

        public IObservableCollection<Room> GetRooms()
        {
            var rooms = XmlSerializer<List<Room>>.Load(Constants.RoomResourceFileName);
            return new SimpleObservableCollection<Room>(rooms);
        }
    }
}