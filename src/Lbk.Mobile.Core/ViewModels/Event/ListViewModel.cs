﻿//  --------------------------------------------------------------------------------------------------------------------
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
    using Cirrious.MvvmCross.ViewModels;
    using Lbk.Mobile.Data.Service.Interfaces;
    using Lbk.Mobile.Data.Service.LbkMobileService;
    using Lbk.Mobile.Infrastructure;
    using Lbk.Mobile.Infrastructure.Extensions;

    public class ListViewModel : BaseViewModel
    {
        private readonly ILbkMobileService service;

        private List<Event> events;

        public ListViewModel(ILbkMobileService service)
        {
            this.service = service;
            this.OnLoadExecute();
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
                    item => this.ShowViewModel<DetailViewModel>(
                        new DetailViewModel.Nav()
                        {
                            ReservationLink = item.ReservationLink
                        }),
                    item => !item.ReservationLink.IsEmpty());
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
            await this.AsyncExecute(() => this.service.GetEventsAsync(), list => this.Events = list);
            
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