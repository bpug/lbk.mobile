//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ListViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Event
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Common;
    using Lbk.Mobile.Common.Exceptions;
    using Lbk.Mobile.Common.Extensions;
    using Lbk.Mobile.Data.Service;
    using Lbk.Mobile.Data.LbkMobileService;

    public class EventListViewModel : BaseViewModel
    {
        private readonly ILbkMobileService service;

        private List<Event> events;

        public EventListViewModel(ILbkMobileService service)
        {
            this.service = service;
        }

        public EventListViewModel()
        {
            this.service = Mvx.Resolve<ILbkMobileService>();
        }

        public void Init()
        {
            LoadCommand.Execute(null);
        }
       

        public List<Event> Events
        {
            get
            {
                return this.events;
            }
            set
            {
                this.events = value;
                this.RaisePropertyChanged(() => this.Events);
            }
        }

        //public MvxAsyncCommand LoadAsyncCommand
        //{
        //    get
        //    {
        //        return new MvxAsyncCommand(this.OnLoadEventsExecute);
        //    }
        //}

        public ICommand LoadCommand
        {
            get
            {
                return new MvxCommand(async () => await this.OnLoadExecute());
            }
        }

        //public ICommand LoadEventsCommand
        //{
        //    get
        //    {
        //        return new MvxCommand(async () => await this.OnLoadEventsExecute());
        //    }
        //}

        public ICommand ShowDetailCommand
        {
            get
            {
                return new MvxCommand<Event>(
                    item => this.ShowViewModel<EventDetailViewModel>(
                        new EventDetailViewModel.Nav()
                        {
                            ReservationLink = item.ReservationLink
                        }),
                    item => !item.ReservationLink.IsEmpty());
            }
        }

        public ICommand ShowOrderCommand
        {
            get
            {
                return new MvxCommand<Event>(item => this.ShowWebPage(item.ReservationLink));
            }
        }

        //public async Task OnLoadEventsExecute()
        //{
        //    this.IsBusy = true;

        //    await Task.Factory.StartNew(
        //        () =>
        //        {
        //            //this.Events = this.service.GetEventsAsync().Result;
        //            var task = this.service.GetEventsAsync();
        //            task.ContinueWith(
        //                t =>
        //                {
        //                    if (t.IsFaulted)
        //                    {
        //                        var ex = (Exception)t.Exception;
        //                        Trace.Error("OnLoadExecute Error: " + ex.Message);
        //                    }
        //                    else
        //                    {
        //                        this.Events = t.Result;
        //                    }
        //                    this.IsBusy = false;
        //                });
        //        });

        //    //this.IsBusy = false;
        //}

        private async Task OnLoadExecute()
        {
            await this.AsyncExecute(() => this.service.GetEventsAsync(), OnSuccess, OnLoadError);
            
            //if (IsBusy)
            //    return;

            //this.IsBusy = true;

            //var task = this.service.GetEventsAsync();
            //await task.ContinueWith(
            //    t =>
            //    {
            //        if (t.IsFaulted)
            //        {
            //            var ex = (Exception)t.Exception;
            //            Trace.Error("OnLoadExecute Error: " + ex.Message);
            //        }
            //        else
            //        {
            //            this.Events = t.Result;
            //        }
            //        this.IsBusy = false;
            //    });
        }

        private void OnSuccess(List<Event> list)
        {
           this.Events = list;
        }


        private void OnLoadError(Exception exception)
        {
            if (exception is ReachabilityException)
            {
                Trace.Warn("Not Reachability");
            }
        }

        //private async void OnLoadExecute2()
        //{
        //    this.IsBusy = true;
        //    try
        //    {
        //        var task = this.service.GetEventsAsync();
        //        this.Events = await task;
        //    }
        //    catch (Exception ex)
        //    {
        //        Trace.Error(ex.Message);
        //    }
        //    finally
        //    {
        //        this.IsBusy = false;
        //    }
        //}
    }
}