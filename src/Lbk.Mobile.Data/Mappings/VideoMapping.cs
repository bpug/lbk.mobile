//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="VideoMapping.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Mappings
{
    using System.Collections.Generic;
    using System.Linq;

    using Lbk.Mobile.Model;

    public static class VideoMapping
    {
        public static List<Video> ToModel(this IEnumerable<LbkMobileService.Video> sourceList)
        {
            if (sourceList == null)
            {
                return null;
            }
            return sourceList.Select(p => p.ToModel()).ToList();
        }

        public static Video ToModel(this LbkMobileService.Video source)
        {
            var video = new Video()
            {
                ThumbnailLink = source.ThumbnailLink,
                Url = source.Link,
                Title = source.Description,
                Sort = source.SortOrder
            };

            return video;
        }
    }
}