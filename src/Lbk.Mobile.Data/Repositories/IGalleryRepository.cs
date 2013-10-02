//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IGalleryRepository.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Repositories
{
    using System;
    using System.Collections.Generic;

    using Lbk.Mobile.Model;

    public interface IGalleryRepository
    {
        void GetPicture(int index, Action<Picture> onSuccess, Action<Exception> onError);

        void GetPictures(Action<List<Picture>> onSuccess, Action<Exception> onError);

        void SavePictures(List<Picture> pictures);
    }
}