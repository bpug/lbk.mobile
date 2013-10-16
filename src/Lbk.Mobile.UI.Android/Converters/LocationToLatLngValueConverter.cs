//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LocationToLatLngValueConverter.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Converters
{
    using System;
    using System.Globalization;

    using Android.Gms.Maps.Model;

    using Cirrious.CrossCore.Converters;

    using Lbk.Mobile.Core.ViewModels.Contact;

    public class LocationToLatLngValueConverter : MvxValueConverter<Location, LatLng>
    {
        protected override LatLng Convert(Location value, Type targetType, object parameter, CultureInfo culture)
        {
            return new LatLng(value.Lat, value.Lng);
        }

        protected override Location ConvertBack(LatLng value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Location()
            {
                Lat = value.Latitude,
                Lng = value.Longitude
            };
        }
    }
}