//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TodaysMenuViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.MenuOfTheDay
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Data.LbkMobileService;
    using Lbk.Mobile.Data.Service;

    public class TodaysMenuViewModel : BaseViewModel
    {
        private readonly ILbkMobileService service;

        private List<category> menuCategories;

        private DishesOfTheDay todaysMenu;

        public TodaysMenuViewModel(ILbkMobileService service)
        {
            this.service = service;
            this.LoadCommand.Execute(null);
        }

        public ICommand LoadCommand
        {
            get
            {
                return new MvxCommand(async () => await this.OnLoadExecute());
            }
        }

        public List<category> MenuCategories
        {
            get
            {
                return this.menuCategories;
            }
            set
            {
                this.menuCategories = value;
                this.RaisePropertyChanged(() => this.MenuCategories);
            }
        }

        public DishesOfTheDay TodaysMenu
        {
            get
            {
                return this.todaysMenu;
            }
            set
            {
                this.todaysMenu = value;
                if (value != null && this.todaysMenu.DishOfTheDay != null)
                {
                    this.MenuCategories = this.todaysMenu.DishOfTheDay.ToList();
                }
                this.RaisePropertyChanged(() => this.TodaysMenu);
            }
        }

        private async Task OnLoadExecute()
        {
            await this.AsyncExecute(() => this.service.GetTodaysMenuAsync(DateTime.Now), list => this.TodaysMenu = list);
        }
    }
}