//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="SimpleObservableCollection.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Infrastructure
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Lbk.Mobile.Infrastructure.Interfaces;

    public class SimpleObservableCollection<T> : ObservableCollection<T>, IObservableCollection<T>
    {
        public SimpleObservableCollection(IEnumerable<T> source)
            : base(source)
        {
        }
    }
}