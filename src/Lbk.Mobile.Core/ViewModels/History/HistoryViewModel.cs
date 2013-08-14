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
    using Lbk.Mobile.Data.Service;
    using Lbk.Mobile.Model;

    
    public class HistoryViewModel : BaseViewModel
    {
        private readonly IXmlDataService xmlDataService;

        private IObservableCollection<History> histories;

        public HistoryViewModel(IXmlDataService xmlDataService)
        {
            this.xmlDataService = xmlDataService;
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

        public void Init()
        {
            this.LoadCommand.Execute(null);
        }

        private void OnLoadExecute()
        {
            this.Histories = this.xmlDataService.GetHistories();
        }
    }
}