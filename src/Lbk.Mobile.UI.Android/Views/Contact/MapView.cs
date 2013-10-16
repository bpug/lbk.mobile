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

    using Lbk.Mobile.Core.ViewModels.Contact;
    using Lbk.Mobile.UI.Droid.Extensions;

    using Object = Java.Lang.Object;

    [Activity(Label = "", Icon = "@drawable/ic_launcher")]
    public class MapView : BaseFragmentActivity<MapViewModel>
    {
        private CenterHelper centerHelper;

        private Marker lbkMarker;

        private GoogleMap googleMap;

        private View mapView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Map_Page);

            SetUpMapIfNeeded();
        }

        private void SetUpMapIfNeeded()
        {
            // Do a null check to confirm that we have not already instantiated the map.
            if (googleMap == null)
            {
                // Try to obtain the map from the SupportMapFragment.
                googleMap = ((SupportMapFragment)SupportFragmentManager.FindFragmentById(Resource.Id.lbk_map)).Map;
                // Check if we were successful in obtaining the map.
                if (googleMap != null)
                {
                    SetUpMap();
                }
            }
        }

        private void SetUpMap()
        {
            AddMarkersToMap();
            //googleMap.UiSettings.MyLocationButtonEnabled = true;
            googleMap.MyLocationEnabled = true;

            googleMap.SetInfoWindowAdapter(new LbkInfoWindowsAdapter(this));

            //this.ZoomToFitMarkers(googleMap);

            //centerHelper = new CenterHelper(googleMap);
            //var set = this.CreateBindingSet<MapView, MapViewModel>();
            //set.Bind(centerHelper)
            //   .For(m => m.Center)
            //   .To(vm => vm.LbkLocation)
            //   .WithConversion(new LocationToLatLngValueConverter(), null);
            //set.Apply();

            // Pan to see all markers in view.
			// Cannot zoom to bounds until the map has a size.
            mapView = SupportFragmentManager.FindFragmentById(Resource.Id.lbk_map).View;
            if (mapView.ViewTreeObserver.IsAlive)
            {
                mapView.ViewTreeObserver.GlobalLayout += ViewTreeObserverOnGlobalLayout;
            }
        }

        protected override void OnDestroy()
        {
            this.mapView.ViewTreeObserver.GlobalLayout -= ViewTreeObserverOnGlobalLayout;
            base.OnDestroy();
        } 

        private void ViewTreeObserverOnGlobalLayout(object sender, EventArgs eventArgs)
        {
            var lbkCoordinates = this.ViewModel.LbkInfo.Location.ToLatLng();
           LatLngBounds bounds =
                            new LatLngBounds.Builder()
                                .Include(lbkCoordinates)
                                .Build();
           googleMap.MoveCamera(CameraUpdateFactory.NewLatLngBounds(bounds, 50));
            
        }

        private void AddMarkersToMap()
        {
            this.lbkMarker = googleMap.AddMarker(new MarkerOptions()
                                        .SetPosition(this.ViewModel.LbkInfo.Location.ToLatLng())
                                        .SetTitle(this.ViewModel.LbkInfo.Title)
                                        .SetSnippet(this.ViewModel.LbkInfo.Description)
                                        .InvokeIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueAzure)));
        }

        private void ZoomToFitMarkers(GoogleMap googleMap)
        {
            var lbkCoordinates = this.ViewModel.LbkInfo.Location.ToLatLng();

            var builder = new LatLngBounds.Builder();
            builder.Include(lbkCoordinates);
            var bounds = builder.Build();
            int padding = 0; // offset from edges of the map in pixels
            //CameraUpdate cu = CameraUpdateFactory.NewLatLngBounds(bounds, padding);

            googleMap.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(lbkCoordinates, 10));
            //googleMap.MoveCamera(cu);
        }

        class GlobalLayoutListener : Java.Lang.Object, ViewTreeObserver.IOnGlobalLayoutListener
        {
            readonly Action onGlobalLayout;

            public GlobalLayoutListener(Action onGlobalLayout)
            {
                this.onGlobalLayout = onGlobalLayout;
            }

            public void OnGlobalLayout()
            {
                onGlobalLayout();
            }
        }

        private class LbkInfoWindowsAdapter : Object, GoogleMap.IInfoWindowAdapter
        {
            private readonly View contentsView;

            private readonly MapView parent;

            public LbkInfoWindowsAdapter(MapView parent)
            {
                this.parent = parent;
                this.contentsView = parent.LayoutInflater.Inflate(Resource.Layout.Map_MarkerInfo, null);
            }

            public View GetInfoContents(Marker marker)
            {
                this.Render(marker, this.contentsView);
                return this.contentsView;
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
                    markerIcon = Resource.Drawable.ic_lbk_map;
                }

                var title = (TextView)view.FindViewById(Resource.Id.marker_title);
                var snippet = (TextView)view.FindViewById(Resource.Id.marker_snippet);
                var icon = (ImageView)view.FindViewById(Resource.Id.marker_icon);

                title.Text = marker.Title;
                snippet.Text = marker.Snippet;
                icon.SetImageResource(markerIcon);
            }
        }
    }

    public class CenterHelper
    {
        private GoogleMap map;

        public CenterHelper(GoogleMap map)
        {
            this.map = map;
        }

        public LatLng Center
        {
            get
            {
                return this.map.Projection.VisibleRegion.LatLngBounds.Center;
            }
            set
            {
                var center = CameraUpdateFactory.NewLatLng(value);
                this.map.MoveCamera(center);
            }
        }
    }
}