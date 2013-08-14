//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GalleryViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Gallery
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Data.LbkMobileService;
    using Lbk.Mobile.Data.Service;

    public class GalleryViewModel : BaseViewModel
    {
        private readonly ILbkMobileService service;

        private List<Picture> pictures;

        public GalleryViewModel(ILbkMobileService service)
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

        public List<Picture> Pictures
        {
            get
            {
                return this.pictures;
            }
            set
            {
                this.pictures = value;
                this.RaisePropertyChanged(() => this.Pictures);
            }
        }

        public void Init()
        {
            this.LoadCommand.Execute(null);
        }

        private async Task OnLoadExecute()
        {
            await this.AsyncExecute(() => this.service.GetPicturesAsync(), list => this.Pictures = list);
        }
    }
}