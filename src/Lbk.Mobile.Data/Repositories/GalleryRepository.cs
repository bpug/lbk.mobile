//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GaleryRepository.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Lbk.Mobile.Data.Utility;
    using Lbk.Mobile.Model;

    public class GalleryRepository : XmlRepositoryBase, IGalleryRepository
    {
        public Picture GetPicture(string url)
        {
            return this.GetPictures().FirstOrDefault(p => p.Url == url);
        }

        public List<Picture> GetPictures()
        {
           var pictures = XmlSerializer<List<Picture>>.Load(Constants.GaleryFileName);
           return pictures;
        }

        public void SavePictures(List<Picture> pictures)
        {
            this.RunAsyncWithLock(
                () => XmlSerializer<List<Picture>>.Save(pictures, Constants.GaleryFileName));
        }
    }
}