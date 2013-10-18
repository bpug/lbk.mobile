//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MapView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Contact
{
    using Android.App;
    using Android.Gms.Maps;
    using Android.Gms.Maps.Model;
    using Android.OS;
    using Android.Views;
    using Android.Widget;

    using Cirrious.MvvmCross.Binding.BindingContext;

    using Java.Lang;

    using Lbk.Mobile.Core.ViewModels.Contact;
    using Lbk.Mobile.UI.Droid.Bindings.MapManager;

    [Activity(Label = "", Icon = "@drawable/ic_launcher")]
    public class MapView : BaseFragmentActivity<MapViewModel>
    {
        private GoogleMap googleMap;

        private LbkAnnotationManager manager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Map_Page);
            this.SetUpMapIfNeeded();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var inflater = this.MenuInflater;
            inflater.Inflate(Resource.Menu.map_actions, menu);
            return true;
        }

       
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.map_normal:
                    item.SetChecked(!item.IsChecked);
                    this.googleMap.MapType = GoogleMap.MapTypeNormal;
                    return true;
                case Resource.Id.map_hybrid:
                    item.SetChecked(!item.IsChecked);
                    this.googleMap.MapType = GoogleMap.MapTypeHybrid;
                    return true;
                case Resource.Id.map_satellite:
                    item.SetChecked(!item.IsChecked);
                    this.googleMap.MapType = GoogleMap.MapTypeSatellite;
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void SetUpMap()
        {
            this.googleMap.MyLocationEnabled = true;
            this.googleMap.SetInfoWindowAdapter(new LbkInfoWindowsAdapter(this));
            var googleMapView = this.SupportFragmentManager.FindFragmentById(Resource.Id.lbk_map).View;
            this.manager = new LbkAnnotationManager(this.googleMap, googleMapView);

            var set = this.CreateBindingSet<MapView, MapViewModel>();
            set.Bind(this.manager).For(m => m.ItemsSource).To(vm => vm.MarkerInfos);
            set.Apply();
        }

        private void SetUpMapIfNeeded()
        {
            if (this.googleMap == null)
            {
                this.googleMap =
                    ((SupportMapFragment)this.SupportFragmentManager.FindFragmentById(Resource.Id.lbk_map)).Map;
                if (this.googleMap != null)
                {
                    this.SetUpMap();
                }
            }
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