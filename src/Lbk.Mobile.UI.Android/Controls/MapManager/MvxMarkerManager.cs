//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MvxMarkerManager.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Controls.MapManager
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    using Android.Gms.Maps;
    using Android.Gms.Maps.Model;
    using Android.Views;

    using Cirrious.CrossCore.Platform;
    using Cirrious.CrossCore.WeakSubscription;
    using Cirrious.MvvmCross.Binding;
    using Cirrious.MvvmCross.Binding.Attributes;

    public abstract class MvxMarkerManager
    {
        private readonly GoogleMap googleMap;

        private readonly View googleMapView;

        private IEnumerable itemsSource;

        private Dictionary<object, Marker> markers = new Dictionary<object, Marker>();

        private IDisposable subscription;

        protected MvxMarkerManager(GoogleMap googleMap, View googleMapView)
        {
            this.googleMap = googleMap;
            this.googleMapView = googleMapView;
            this.googleMapView.ViewTreeObserver.GlobalLayout += ViewTreeObserverOnGlobalLayout;
        }

        // MvxSetToNullAfterBinding isn't strictly needed any more 
        // - but it's nice to have for when binding is torn down
        [MvxSetToNullAfterBinding]
        public virtual IEnumerable ItemsSource
        {
            get
            {
                return this.itemsSource;
            }
            set
            {
                this.SetItemsSource(value);
            }
        }

        protected virtual void AddMarkerFor(object item)
        {
            var markerOptions = this.CreateMarker(item);
            var marker = this.googleMap.AddMarker(markerOptions);
            this.markers[item] = marker;

            // Notz show Markers witout title
            if (string.IsNullOrEmpty(marker.Title) && string.IsNullOrEmpty(marker.Snippet))
            {
                marker.Remove();
            }

            this.MoveCamera();
        }

        protected virtual void MoveCamera()
        {

            if (this.googleMapView.ViewTreeObserver.IsAlive)
            {
                //this.googleMapView.ViewTreeObserver.GlobalLayout -= ViewTreeObserverOnGlobalLayout;
                //this.googleMapView.ViewTreeObserver.GlobalLayout += ViewTreeObserverOnGlobalLayout;
            }
            else
            {
                ViewTreeObserverOnGlobalLayout(null, null);
            }
        }

        private void ViewTreeObserverOnGlobalLayout(object sender, EventArgs eventArgs)
        {
            var boundsBuilder = new LatLngBounds.Builder();
            foreach (var marker in markers)
            {
                boundsBuilder.Include(marker.Value.Position);
            }
            var bounds = boundsBuilder.Build();
            this.googleMap.AnimateCamera(CameraUpdateFactory.NewLatLngBounds(bounds, 50));
        }

        protected virtual void AddMarkers(IEnumerable newItems)
        {
            foreach (var item in newItems)
            {
                this.AddMarkerFor(item);
            }
        }

        protected abstract MarkerOptions CreateMarker(object item);

        protected virtual void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.AddMarkers(e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.RemoveMarkers(e.OldItems);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    this.RemoveMarkers(e.OldItems);
                    this.AddMarkers(e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Move:
                    // not interested in this
                    break;
                case NotifyCollectionChangedAction.Reset:
                    this.ReloadAllMarkers();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected virtual void ReloadAllMarkers()
        {
            //this.googleMap.RemoveAnnotations(this.markers.Values.Select(x => (NSObject)x).ToArray());
            foreach (var marker in this.markers)
            {
                marker.Value.Remove();
            }
            this.markers.Clear();

            if (this.itemsSource == null)
            {
                return;
            }

            this.AddMarkers(this.itemsSource);
        }

        protected virtual void RemoveMarkerFor(object item)
        {
            var marker = this.markers[item];
            marker.Remove();
            this.markers.Remove(item);
        }

        protected virtual void RemoveMarkers(IEnumerable oldItems)
        {
            foreach (var item in oldItems)
            {
                this.RemoveMarkerFor(item);
            }
        }

        protected virtual void SetItemsSource(IEnumerable value)
        {
            if (Equals(this.itemsSource, value))
            {
                return;
            }

            if (this.subscription != null)
            {
                this.subscription.Dispose();
                this.subscription = null;
            }
            this.itemsSource = value;
            if (this.itemsSource != null && !(this.itemsSource is IList))
            {
                MvxBindingTrace.Trace(
                    MvxTraceLevel.Warning,
                    "Binding to IEnumerable rather than IList - this can be inefficient, especially for large lists");
            }

            this.ReloadAllMarkers();

            var newObservable = this.itemsSource as INotifyCollectionChanged;
            if (newObservable != null)
            {
                this.subscription = newObservable.WeakSubscribe(this.OnItemsSourceCollectionChanged);
            }
        }
    }
}