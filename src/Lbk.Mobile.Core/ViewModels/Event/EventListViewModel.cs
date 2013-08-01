//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EventListViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Event
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Data.Service.Interfaces;
    using Lbk.Mobile.Data.Service.Service;

    public class EventListViewModel : BaseViewModel
    {
        private readonly ILbkMobileService service;

        public EventListViewModel(ILbkMobileService service)
        {
            this.service = service;
        }

        public ICommand GetEventsCommand
        {
            get
            {
                return new MvxCommand(async () => await this.DoGetEventsCommand());
            }
        }

        public async Task DoGetEventsCommand()
        {
            this.IsBusy = true;

            await Task.Factory.StartNew(() => { this.service.GetEventsAsync(); });

            this.IsBusy = false;
        }
    }
}