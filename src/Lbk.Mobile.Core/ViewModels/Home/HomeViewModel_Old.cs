//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="HomeViewModel_Old.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Home
{
    using System.Collections.Generic;
    using System.Windows.Input;

    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Core.Interfaces.First;

    public class HomeViewModel_Old : BaseViewModel
    {
        private List<SimpleItem> _items;

        private string _key;

        public ICommand FetchItemsCommand
        {
            get
            {
                return new MvxCommand(this.DoFetchItems);
            }
        }

        public List<SimpleItem> Items
        {
            get
            {
                return this._items;
            }

            set
            {
                this._items = value;
                this.RaisePropertyChanged(() => this.Items);
            }
        }

        public string Key
        {
            get
            {
                return this._key;
            }

            set
            {
                this._key = value;
                this.RaisePropertyChanged(() => this.Key);
            }
        }

        private void DoFetchItems()
        {
            var service = Mvx.Resolve<IFirstService>();
            service.GetItems(this.Key, this.OnSuccess, this.OnError);
        }

        private void OnError(FirstServiceError firstServiceError)
        {
            this.ReportError("Sorry - a problem occurred - " + firstServiceError);
        }

        private void OnSuccess(List<SimpleItem> simpleItems)
        {
            this.Items = simpleItems;
        }
    }
}