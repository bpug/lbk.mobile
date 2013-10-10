//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GalleryViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Gallery
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Data.Services;
    using Lbk.Mobile.Model;

    public class GalleryViewModel : BaseViewModel
    {
        private readonly IGalleryRepository galleryRepository;

        private readonly ILbkMobileService service;

        private List<Picture> pictures;

        public GalleryViewModel(ILbkMobileService service, IGalleryRepository galleryRepository)
        {
            this.service = service;
            this.galleryRepository = galleryRepository;
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

        public ICommand ShowPictureCommand
        {
            get
            {
                return new MvxCommand<Picture>(
                    item => this.ShowViewModel<PictureViewModel>(
                        new
                        {
                            index = item.Index
                        }));
            }
        }

        public override void Start()
        {
            this.LoadCommand.Execute(null);
            base.Start();
        }

        private async Task OnLoadExecute()
        {
            await this.AsyncExecute(() => this.service.GetPicturesAsync(), this.OnLoadSuccess);
        }

        private void OnLoadSuccess(List<Picture> pictureList)
        {
            this.Pictures = pictureList; //.Where( p => (p.Index == 6 || p.Index == 7)).ToList();
            this.galleryRepository.SavePictures(this.Pictures);
        }
    }
}