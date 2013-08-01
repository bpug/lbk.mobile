//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Room.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Model
{
    using System.Collections.Generic;

    public class Room
    {
        public int? Area { get; set; }
        public string Description { get; set; }
        public List<string> ImageUrl { get; set; }
        public string Seats { get; set; }
        public string Subtitle { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Title { get; set; }
    }
}