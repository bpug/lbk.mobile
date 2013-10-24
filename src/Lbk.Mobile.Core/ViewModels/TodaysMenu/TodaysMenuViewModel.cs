//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TodaysMenuViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.TodaysMenu
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Data.Services;
    using Lbk.Mobile.Model;

    public class TodaysMenuViewModel : BaseViewModel
    {
        private readonly ILbkMobileService service;

        public TodaysMenuViewModel(ILbkMobileService service)
        {
            this.service = service;
            this.Date = DateTime.Now.ToString("D");
        }

        public ICommand LoadCommand
        {
            get
            {
                return new MvxCommand(async () => await this.OnLoadExecute());
            }
        }

        public string Date { get; set; }

        public List<MenuCategory> MenuCategories { get; set; }

        public override void Start()
        {
            this.LoadCommand.Execute(null);
        }

        private async Task OnLoadExecute()
        {
            await
                this.AsyncExecute(
                    () => this.service.GetTodaysMenuAsync(DateTime.Now),
                    list => this.MenuCategories = list);
        }
    }
}