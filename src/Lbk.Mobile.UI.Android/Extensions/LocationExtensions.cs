//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LocationExtensions.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Extensions
{
    using Android.Gms.Maps.Model;

    using Lbk.Mobile.Core.ViewModels.Contact;

    public static class LocationExtensions
    {
        public static LatLng ToLatLng(this Location source)
        {
            return new LatLng(source.Lat, source.Lng);
        }

        public static Location ToLocation(this LatLng source)
        {
            return new Location()
            {
                Lat = source.Latitude,
                Lng = source.Longitude
            };
        }
    }
}