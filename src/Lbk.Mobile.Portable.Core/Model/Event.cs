// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Event.cs" company="ip-connect GmbH">
//   Copyright (c) ip-connect GmbH. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Portable.Core.Model
{
    using System;

    public class Event
    {
        public DateTime ActivatedAt { get; set; }

        public string Date { get; set; }

        public DateTime DateOrder { get; set; }

        public string Description { get; set; }

        public DateTime ExpiresAt { get; set; }

        public bool IsActivated { get; set; }

        public string ReservationLink { get; set; }

        public string ThumbnailLink { get; set; }

        public string Title { get; set; }
    }
}