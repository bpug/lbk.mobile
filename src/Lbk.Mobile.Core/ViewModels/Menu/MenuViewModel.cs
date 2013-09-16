//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MenuViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Menu
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Data.Services;
    using Lbk.Mobile.Plugin.Settings;

    public class MenuViewModel : BaseViewModel
    {
         private readonly ILbkMobileService service;

         private readonly ISettings settings;

        public MenuViewModel(ILbkMobileService service, ISettings settings)
        {
            this.service = service;
            this.settings = settings;
        }

        public override void Start()
        {
            this.GetLastUpdateCommand.Execute(null);
        }

        public ICommand GetLastUpdateCommand
        {
            get
            {
                return new MvxCommand(async () => await this.OnGetLastUpdateExecute());
            }
        }

        private DateTime? lastUpdate;

        public DateTime? LastUpdate
        {
            get
            {
                return lastUpdate;
            }
            set
            {
                this.lastUpdate = value;
                this.RaisePropertyChanged(() => this.LastUpdate);
            }
        }

        private async Task OnGetLastUpdateExecute()
        {
            await
                this.AsyncExecute(
                    () => this.service.GetMenuLastUpdateAsync(),this.DisplayPdf);
        }

        private void DisplayPdf(DateTime? updateDate)
        {
            this.LastUpdate = updateDate;

            var userLastUpdate = settings.GetValueOrDefault<DateTime?>(Constants.UserSettings.PdfLastUpdate, default(DateTime?));
            if (!userLastUpdate.HasValue)
            {
                PdfDownload();
            }

            if (updateDate.HasValue && userLastUpdate.HasValue
                && !(Math.Abs((updateDate.Value - userLastUpdate.Value).TotalSeconds) < 1))
            {
                this.MessageBoxService.Show(
                   this.TextSource.GetText("PdfDownloadQuestion"),
                    string.Empty,
                    this.SharedTextSource.GetText("Yes"),
                    this.SharedTextSource.GetText("No"),
                    b =>
                    {
                        if (b)
                        {
                            PdfDownload();
                        }
                    });
            }
        }

        private void PdfDownload()
        {
            
        }

    }
}