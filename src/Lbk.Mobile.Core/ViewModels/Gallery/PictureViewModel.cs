//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="PictureViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Gallery
{
    using System.Collections.Generic;
    using System.Linq;

    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Model;

    public class PictureViewModel : BaseViewModel
    {
        private readonly IGalleryRepository galleryRepository;

        public PictureViewModel(IGalleryRepository galleryRepository)
        {
            this.galleryRepository = galleryRepository;
        }

        public Picture Current { get; set; }

        public List<Picture> Pictures { get; set; }

        public void Init(int index)
        {
            this.IsBusy = true;            
            this.galleryRepository.GetPictures(
                list =>
                {
                    this.Pictures = list;
                    this.Current = list.FirstOrDefault(p => p.Index == index);
                    this.IsBusy = false;
                },
                null);

            //MvxAsyncDispatcher.BeginAsync(
            //    () =>
            //    {
            //        this.Pictures = this.galleryRepository.GetPictures();
            //        if (this.Pictures != null)
            //        {
            //            this.Current = this.Pictures.FirstOrDefault(p => p.Index == index);
            //        }
            //        this.IsBusy = false;
            //    });
        }
    }
}