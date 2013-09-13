//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="HistoryDetailViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.History
{
    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Model;

    public class HistoryDetailViewModel : BaseViewModel
    {
        private readonly IHistoryRepository historyRepository;

        private History history;

        public HistoryDetailViewModel(IHistoryRepository historyRepository)
        {
            this.historyRepository = historyRepository;
        }

        public History History
        {
            get
            {
                return this.history;
            }
            set
            {
                this.history = value;
                this.RaisePropertyChanged(() => this.History);
            }
        }

        public void Init(int id)
        {
            this.History = this.historyRepository.GetHistory(id);
        }
    }
}