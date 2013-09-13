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
    using Lbk.Mobile.Data.Services;
    using Lbk.Mobile.Model;

    public class VideoListViewModel : BaseViewModel
    {
        private readonly ILbkMobileService service;

        private List<Video> videos;

        public VideoListViewModel(ILbkMobileService service)
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


        public ICommand ShowVideoCommand
        {
            get
            {
                return new MvxCommand<Video>(video => this.ShowVideoPage(video.Url));
            }
        }
        

        public override void Start()
        {
            this.LoadCommand.Execute(null);
        }

        private async Task OnLoadExecute()
        {
            await this.AsyncExecute(() => this.service.GetVideosAsyn(), list => this.Videos = list);
        }
    }
}