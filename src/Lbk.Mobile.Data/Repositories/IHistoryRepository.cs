//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IHistoryRepository.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Repositories
{
    using Lbk.Mobile.Common.Interfaces;
    using Lbk.Mobile.Model;

    public interface IHistoryRepository
    {
        IObservableCollection<History> GetHistories();
    }
}