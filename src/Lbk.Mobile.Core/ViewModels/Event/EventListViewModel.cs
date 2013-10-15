//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EventListViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Event
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Common;
    using Lbk.Mobile.Common.Exceptions;
    using Lbk.Mobile.Common.Extensions;
    using Lbk.Mobile.Core.ViewModels.Helpers;
    using Lbk.Mobile.Data.Services;
    using Lbk.Mobile.Model;

    public class EventListViewModel : BaseViewModel
    {
        private readonly ILbkMobileService service;

        public EventListViewModel(ILbkMobileService service)
        {
            this.service = service;
        }

        public List<ModelWithCommand<Event>> Events { get; set; }

        public ICommand LoadCommand
        {
            get
            {
                return new MvxCommand(async () => await this.LoadExecute());
            }
        }

        
        public ICommand ShowBookingCommand
        {
            get
            {
                return
                    new MvxCommand<Event>(this.ShowBookingExecute,item => !item.ReservationLink.IsEmpty());
            }
        }

        public void ShowBookingExecute(Event @event)
        {
            //this.ShowWebPage(@eEvent.ReservationLink);

            this.ShowViewModel<EventBookingViewModel>(
                new EventBookingViewModel.Nav()
                {
                    ReservationLink = @event.ReservationLink,
                    Title = @event.Title
                });
        }

        public override void Start()
        {
            this.LoadCommand.Execute(null);
        }

        private async Task LoadExecute()
        {
            await
                this.AsyncExecute(() => this.service.GetEventsAsync(),
                    result =>
                    {
                        this.Events = result.Select(x => new ModelWithCommand<Event>(x, new MvxCommand(() => ShowBookingExecute(x)))).ToList();
                    }, this.OnLoadError);
        }

        private void OnLoadError(Exception exception)
        {
            if (exception is ReachabilityException)
            {
                Trace.Warn("Not Reachability");
            }
        }

        //private async Task LoadExecute()
        //{
        //    if (IsBusy)
        //        return;

        //    this.IsBusy = true;

        //    var task = this.service.GetEventsAsync();
        //    await task.ContinueWith(
        //        t =>
        //        {
        //            if (t.IsFaulted)
        //            {
        //                var ex = (Exception)t.Exception;
        //                Trace.Error("OnLoadExecute Error: " + ex.Message);
        //            }
        //            else
        //            {
        //                this.Events = t.Result;
        //            }
        //            this.IsBusy = false;
        //        });
        //}

        //public MvxAsyncCommand LoadAsyncCommand
        //{
        //    get
        //    {
        //        return new MvxAsyncCommand(this.OnLoadEventsExecute);
        //    }
        //}

        //public ICommand LoadEventsCommand
        //{
        //    get
        //    {
        //        return new MvxCommand(async () => await this.OnLoadEventsExecute());
        //    }
        //}

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