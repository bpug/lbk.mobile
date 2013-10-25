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

        private int historyId;

        public HistoryDetailViewModel(IHistoryRepository historyRepository)
        {
            this.historyRepository = historyRepository;
        }

        public History History { get; set; }

        public void Init(int id)
        {
            this.historyId = id;
        }

        public override void Start()
        {
            this.IsBusy = true;
            this.historyRepository.GetHistory(
                this.historyId,
                history =>
                {
                    this.History = history;
                    this.IsBusy = false;
                },
                exception => { this.IsBusy = false; });
        }
    }
}