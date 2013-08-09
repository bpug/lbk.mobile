//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IObservableCollection.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Common.Interfaces
{
    using System.Collections.Generic;
    using System.Collections.Specialized;

    public interface IObservableCollection<T> : IList<T>, INotifyCollectionChanged
    {
    }
}