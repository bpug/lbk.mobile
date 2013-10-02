//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="PictureMapping.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Mappings
{
    using System.Collections.Generic;
    using System.Linq;

    using Lbk.Mobile.Model;

    public static class PictureMapping
    {
        public static List<Picture> ToModel(this IEnumerable<LbkMobileService.Picture> sourceList)
        {
            if (sourceList == null)
            {
                return null;
            }
            var pictures=  sourceList.Select(p => p.ToModel()).ToList();

            for (var i = 0; i < pictures.Count; i++)
            {
                pictures[i].Index = i;
            }

            return pictures;
        }

        public static Picture ToModel(this LbkMobileService.Picture source)
        {
            var picture = new Picture()
            {
                Description = source.Description.Trim(),
                FileName = source.FileName,
                Url = source.Link,
                Title = source.Title.Trim(),
                Sort = source.SortOrder
            };

            return picture;
        }
    }
}