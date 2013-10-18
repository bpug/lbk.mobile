//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LbkAnnotationManager.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Bindings.MapManager
{
    using Android.Gms.Maps;
    using Android.Gms.Maps.Model;
    using Android.Views;

    using Lbk.Mobile.Core.ViewModels.Contact;
    using Lbk.Mobile.UI.Droid.Extensions;

    public class LbkAnnotationManager : MvxMarkerManager
    {
        public LbkAnnotationManager(GoogleMap googleMap, View googleMapView)
            : base(googleMap, googleMapView)
        {
        }

        protected override MarkerOptions CreateMarker(object item)
        {
            var markerInfo = item as MarkerInfo;

            if (markerInfo == null)
            {
                return null;
            }

            var options =
                new MarkerOptions().SetPosition(markerInfo.Location.ToLatLng())
                    .SetTitle(markerInfo.Title)
                    .SetSnippet(markerInfo.Description)
                    .InvokeIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueRed));
            return options;
        }
    }
}