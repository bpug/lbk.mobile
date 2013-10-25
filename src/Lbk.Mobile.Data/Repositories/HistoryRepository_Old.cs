//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="HistoryRepository.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Lbk.Mobile.Common;
    using Lbk.Mobile.Common.Interfaces;
    using Lbk.Mobile.Data.Utils;
    using Lbk.Mobile.Model;

    public class HistoryRepository_Old : XmlRepositoryBase, IHistoryRepository
    {
        public IObservableCollection<History> GetHistories()
        {
            var histories = XmlSerializer<List<History>>.LoadFromResource(Constants.HistoryResourceFileName);
            return new SimpleObservableCollection<History>(histories);
        }

        public History GetHistory(int id)
        {
            return this.GetHistories().FirstOrDefault(p => p.PageIndex == id);
        }
    }
}