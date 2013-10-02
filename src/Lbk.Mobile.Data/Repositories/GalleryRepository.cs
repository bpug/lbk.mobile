//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GalleryRepository.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Cirrious.CrossCore.Core;
    using Cirrious.CrossCore.Platform;
    using Cirrious.MvvmCross.Plugins.File;

    using Lbk.Mobile.Model;

    public class GalleryRepository : MvxLockableObject, IGalleryRepository
    {
        private readonly IMvxFileStore fileStore;

        private readonly IMvxJsonConverter jsonConverter;

        public GalleryRepository(IMvxFileStore fileStore, IMvxJsonConverter jsonConverter)
        {
            this.fileStore = fileStore;
            this.jsonConverter = jsonConverter;
        }

        public void GetPicture(int index, Action<Picture> onSuccess, Action<Exception> onError)
        {
            this.GetPictures(
                list =>
                {
                    var picture = list.FirstOrDefault(p => p.Index == index);
                    onSuccess(picture);
                },
                onError);
        }
       

        public void GetPictures(Action<List<Picture>> onSuccess, Action<Exception> onError)
        {
            this.RunAsyncWithLock(
                () =>
                {
                    string json;
                    if (!this.fileStore.TryReadTextFile(Constants.GaleryFileName, out json))
                    {
                        throw new FileNotFoundException();
                    }

                    var pictures = this.jsonConverter.DeserializeObject<List<Picture>>(json);

                    onSuccess(pictures);
                });
        }

        public void SavePictures(List<Picture> pictures)
        {
            //this.RunAsyncWithLock(
            //    () => XmlSerializer<List<Picture>>.Save(pictures, Constants.GaleryFileName));

            this.RunAsyncWithLock(
                () =>
                {
                    string json = this.jsonConverter.SerializeObject(pictures);
                    this.fileStore.WriteFile(Constants.GaleryFileName, json);
                });
        }

        //public Picture GetPicture(int index)
        //{
        //    return this.GetPictures().FirstOrDefault(p => p.Index == index);
        //}

        //public List<Picture> GetPictures()
        //{
        //    //var pictures = XmlSerializer<List<Picture>>.Load(Constants.GaleryFileName);
        //    //return pictures;

        //    string json;
        //    if (!this.fileStore.TryReadTextFile(Constants.GaleryFileName, out json))
        //    {
        //        return null;
        //    }

        //    var pictures = this.jsonConverter.DeserializeObject<List<Picture>>(json);

        //    return pictures;
        //}
    }
}