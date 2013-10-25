//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IHistoryRepository.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Repositories
{
    using System;
    using Lbk.Mobile.Common.Interfaces;
    using Lbk.Mobile.Model;

    public interface IHistoryRepository
    {
        void GetHistories(Action<IObservableCollection<History>> onSuccess, Action<Exception> onError);
        void GetHistory(int id, Action<History> onSuccess, Action<Exception> onError);

        //IObservableCollection<History> GetHistories();
        //History GetHistory(int id);
    }
}