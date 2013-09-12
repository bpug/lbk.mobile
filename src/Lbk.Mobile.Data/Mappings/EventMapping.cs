//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="AnswerMapping.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Mappings
{
    using System.Collections.Generic;
    using System.Linq;

    using Lbk.Mobile.Model;

    public static class EventMapping
    {
        public static List<Event> ToModel(this IEnumerable<LbkMobileService.Event> sourceList)
        {
            if (sourceList == null)
            {
                return null;
            }
            return sourceList.Select(p => p.ToModel()).ToList();
        }

        public static Event ToModel(this LbkMobileService.Event source)
        {
            var @event = new Event()
            {
                ThumbnailLink = source.ThumbnailLink,
                Title = source.Title,
                ReservationLink = source.ReservationLink,
                Date = source.Date,
                Description = source.Description,
                DateOrder = source.DateOrder
            };

            return @event;
        }
    }
}