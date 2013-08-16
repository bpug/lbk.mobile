//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="HistoryRepository.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Repositories
{
    using System.Collections.Generic;

    using Lbk.Mobile.Common;
    using Lbk.Mobile.Common.Interfaces;
    using Lbk.Mobile.Data.Utility;
    using Lbk.Mobile.Model;

    public class HistoryRepository : XmlRepositoryBase, IHistoryRepository
    {
        public IObservableCollection<History> GetHistories()
        {
            var histories = XmlSerializer<List<History>>.Load(Constants.HistoryResourceFileName);
            return new SimpleObservableCollection<History>(histories);
        }
    }
}