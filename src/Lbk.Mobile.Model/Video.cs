//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Video.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Model
{
    using System;

    public class Video
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public int Sort { get; set; }
        public string ThumbnailLink { get; set; }

        public Uri ThumbnailUri
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.ThumbnailLink) ? new Uri(this.ThumbnailLink) : null;
            }
        }

        public bool IsYoutube
        {
            get
            {
                var uri = new Uri(Url);
                return uri.Host.Contains("youtube");
            }
        }
    }
}