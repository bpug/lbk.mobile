//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="VideoViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Video
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Data.LbkMobileService;
    using Lbk.Mobile.Data.Services;

    public class VideoViewModel : BaseViewModel
    {
        private readonly ILbkMobileService service;

        private List<Video> videos;

        public VideoViewModel(ILbkMobileService service)
        {
            this.service = service;
        }

        public ICommand LoadCommand
        {
            get
            {
                return new MvxCommand(async () => await this.OnLoadExecute());
            }
        }

        public List<Video> Videos
        {
            get
            {
                return this.videos;
            }
            set
            {
                this.videos = value;
                this.RaisePropertyChanged(() => this.Videos);
            }
        }

        public void Init()
        {
            this.LoadCommand.Execute(null);
        }

        private async Task OnLoadExecute()
        {
            await this.AsyncExecute(() => this.service.GetVideosAsyn(), list => this.Videos = list);
        }
    }
}