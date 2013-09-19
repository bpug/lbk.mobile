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

    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.Plugins.DownloadCache;
    using Cirrious.MvvmCross.Plugins.File;
    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Data.Services;
    using Lbk.Mobile.Plugin.DocumentViewer;
    using Lbk.Mobile.Plugin.Settings;

    public class MenuViewModel : BaseViewModel
    {
        private readonly ILbkMobileService service;

        private readonly ISettings settings;

        private DateTime? lastUpdate;

        public MenuViewModel(ILbkMobileService service, ISettings settings)
        {
            this.service = service;
            this.settings = settings;
        }

        public ICommand GetLastUpdateCommand
        {
            get
            {
                return new MvxCommand(async () => await this.OnGetLastUpdateExecute());
            }
        }

        public DateTime? LastUpdate
        {
            get
            {
                return this.lastUpdate;
            }
            set
            {
                this.lastUpdate = value;
                this.RaisePropertyChanged(() => this.LastUpdate);
            }
        }

        public override void Start()
        {
            this.GetLastUpdateCommand.Execute(null);
        }

        private void CheckMenu(DateTime? updateDate)
        {

            
            
            this.LastUpdate = updateDate;

            var userLastUpdate = this.settings.GetValueOrDefault<DateTime?>(
                Constants.UserSettings.PdfLastUpdate,
                default(DateTime?));

            var fileStore = Mvx.Resolve<IMvxFileStore>();

            if (!userLastUpdate.HasValue || !fileStore.Exists(Constants.LocalMenuFilePath))
            {
                this.DownloadMenu(updateDate);
            }

            else if (updateDate.HasValue && !(Math.Abs((updateDate.Value - userLastUpdate.Value).TotalSeconds) < 1))
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
                            this.DownloadMenu(updateDate);
                        }
                    });
            }
            else
            {
                this.ShowMenu();
            }
        }

        private void DownloadMenu(DateTime? updateDate)
        {
            this.IsBusy = true;
            var downloader = Mvx.Resolve<IMvxHttpFileDownloader>();
            downloader.RequestDownload(
                Constants.MenuUrl,
                Constants.LocalMenuFilePath,
                () => this.OnDownloadSuccess(updateDate),
                this.OnDownloadError);
        }

        private void OnDownloadError(Exception exception)
        {
            this.IsBusy = false;
            this.MessageBoxService.Show(
                this.SharedTextSource.GetText("DownloadError"),
                this.SharedTextSource.GetText("PleaseTryNow"),
                this.SharedTextSource.GetText("OK"),
                () => { });
        }

        private void OnDownloadSuccess(DateTime? updateDate)
        {
            this.IsBusy = false;
            this.settings.AddOrUpdateValue(Constants.UserSettings.PdfLastUpdate, updateDate);
            this.settings.Save();
            this.ShowMenu();
        }

        private async Task OnGetLastUpdateExecute()
        {
            await this.AsyncExecute(() => this.service.GetMenuLastUpdateAsync(), this.CheckMenu);
        }

        private void ShowMenu()
        {
            var fileStore = Mvx.Resolve<IMvxFileStore>();
            var viewer = Mvx.Resolve<IDocumentViewerTask>();

            var path = fileStore.NativePath(Constants.LocalMenuFilePath);
            viewer.ShowPdf(path, Constants.MenuUrl, true);
        }
    }
}