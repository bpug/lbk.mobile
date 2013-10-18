//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MapView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Contact
{
    using System;

    using Android.App;
    using Android.Gms.Maps;
    using Android.Gms.Maps.Model;
    using Android.OS;
    using Android.Views;
    using Android.Widget;

    using Cirrious.MvvmCross.Binding.BindingContext;

    using Lbk.Mobile.Core.ViewModels.Contact;
    using Lbk.Mobile.UI.Droid.Controls.MapManager;
    using Lbk.Mobile.UI.Droid.Extensions;

    using Object = Java.Lang.Object;

    [Activity(Label = "", Icon = "@drawable/ic_launcher")]
    public class MapView : BaseFragmentActivity<MapViewModel>
    {
        private GoogleMap googleMap;

        private Marker lbkMarker;

        private View mapView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Map_Page);

            this.SetUpMapIfNeeded();
        }

        private void AddMarkersToMap()
        {
            this.lbkMarker =
                this.googleMap.AddMarker(
                    new MarkerOptions().SetPosition(this.ViewModel.LbkInfo.Location.ToLatLng())
                        .SetTitle(this.ViewModel.LbkInfo.Title)
                        .SetSnippet(this.ViewModel.LbkInfo.Description)
                        .InvokeIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueRed)));
            //this.currentMarker = googleMap.AddMarker(new MarkerOptions()
            //                            .SetPosition(this.ViewModel.CurrentLocation.ToLatLng())
            //                            .SetTitle("Aktueller Ort")
            //                            .InvokeIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueAzure)));
        }

        private LbkAnnotationManager manager;
        

        private void SetUpMap()
        {
            this.AddMarkersToMap();
            this.googleMap.MyLocationEnabled = true;
            this.googleMap.SetInfoWindowAdapter(new LbkInfoWindowsAdapter(this));

            this.mapView = this.SupportFragmentManager.FindFragmentById(Resource.Id.lbk_map).View;
            if (this.mapView.ViewTreeObserver.IsAlive)
            {
                this.mapView.ViewTreeObserver.GlobalLayout += this.ViewTreeObserverOnGlobalLayout;
            }
        }

        private void SetUpMapIfNeeded()
        {
            if (this.googleMap == null)
            {
               this.googleMap = ((SupportMapFragment)this.SupportFragmentManager.FindFragmentById(Resource.Id.lbk_map)).Map;
                if (this.googleMap != null)
                {
                    this.SetUpMap();
                }
            }
        }

        private void ViewTreeObserverOnGlobalLayout(object sender, EventArgs eventArgs)
        {
            var lbkCoordinates = this.ViewModel.LbkInfo.Location.ToLatLng();
            var currentCoordinates = this.ViewModel.CurrentLocation.ToLatLng();
            var bounds = new LatLngBounds.Builder().Include(lbkCoordinates).Include(currentCoordinates).Build();
            this.googleMap.AnimateCamera(CameraUpdateFactory.NewLatLngBounds(bounds, 50));
        }

        private class LbkInfoWindowsAdapter : Object, GoogleMap.IInfoWindowAdapter
        {
            private readonly View contentView;

            private readonly MapView parent;

            public LbkInfoWindowsAdapter(MapView parent)
            {
                this.parent = parent;
                this.contentView = parent.LayoutInflater.Inflate(Resource.Layout.Map_MarkerInfo, null);
            }

            public View GetInfoContents(Marker marker)
            {
                this.Render(marker, this.contentView);
                return this.contentView;
            }

            public View GetInfoWindow(Marker marker)
            {
                return null;
            }

            private void Render(Marker marker, View view)
            {
                int markerIcon = 0;
                if (marker.Equals(this.parent.lbkMarker))
                {
                    markerIcon = Resource.Drawable.ic_launcher;
                }

                markerIcon = Resource.Drawable.ic_launcher;
                var title = (TextView)view.FindViewById(Resource.Id.marker_title);
                var snippet = (TextView)view.FindViewById(Resource.Id.marker_snippet);
                var icon = (ImageView)view.FindViewById(Resource.Id.marker_icon);

                title.Text = marker.Title;
                snippet.Text = marker.Snippet;
                icon.SetImageResource(markerIcon);
            }
        }
    }
}