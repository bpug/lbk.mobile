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

        private IObservableCollection<History> histories;

        public HistoryViewModel(IHistoryRepository historyRepository)
        {
            this.historyRepository = historyRepository;
        }

        public IObservableCollection<History> Histories
        {
            get
            {
                return this.histories;
            }
            set
            {
                this.histories = value;
                this.RaisePropertyChanged(() => this.Histories);
            }
        }

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
            this.Histories = this.historyRepository.GetHistories();
        }
    }
}