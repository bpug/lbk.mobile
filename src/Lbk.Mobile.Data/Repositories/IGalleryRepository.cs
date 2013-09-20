//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IGaleryRepository.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Repositories
{
    using System.Collections.Generic;

    using Lbk.Mobile.Model;

    public interface IGalleryRepository
    {
        Picture GetPicture(string  url);

        List<Picture> GetPictures();

        void SavePictures(List<Picture> pictures);
    }
}