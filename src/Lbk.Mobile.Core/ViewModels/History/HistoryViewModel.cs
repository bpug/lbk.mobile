//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="HistoryViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.History
{
    using System.Windows.Input;
    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Common.Interfaces;
    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Model;

    
    public class HistoryViewModel : BaseViewModel
    {
        private readonly IHistoryRepository historyRepository;

        public HistoryViewModel(IHistoryRepository historyRepository)
        {
            this.historyRepository = historyRepository;
        }

        public IObservableCollection<History> Histories { get; set; }

        public ICommand LoadCommand
        {
            get
            {
                return new MvxCommand(this.OnLoadExecute);
            }
        }

        public override void Start()
        {
            this.LoadCommand.Execute(null);
        }

        private void OnLoadExecute()
        {
            IsBusy = true;
            this.historyRepository.GetHistories(
                histories =>
                {
                    this.Histories = histories;
                    IsBusy = false;
                },
                exception => { IsBusy = false; });
        }

        //private void OnLoadExecute()
        //{
        //    IsBusy = true;
        //    MvxAsyncDispatcher.BeginAsync(
        //        () =>
        //        {
        //            this.Histories = this.historyRepository.GetHistories();
        //            IsBusy = false;
        //        });
        //}

    }
}