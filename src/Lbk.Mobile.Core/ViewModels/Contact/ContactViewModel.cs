//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ContactViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Contact
{
    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.Plugins.Location;

    using Lbk.Mobile.Common;
    using Lbk.Mobile.Common.Utils;

    public class ContactViewModel : BaseViewModel
    {
        private readonly IMvxGeoLocationWatcher watcher;

        public ContactViewModel(IMvxGeoLocationWatcher watcher)
        {
            this.watcher = watcher;
            watcher.Start(new MvxGeoLocationOptions{EnableHighAccuracy = true }, OnLocation, OnError);
        }

        private void OnLocation(MvxGeoLocation location)
        {
            var lat = location.Coordinates.Latitude;
            var lng = location.Coordinates.Longitude;

            this.Distance = DistanceCalcs.DistanceInMetres(lat, lng, Constants.LatitudeLbk, Constants.LongitudeLbk);

            this.watcher.Stop();
        }

        private double distance;

        public double Distance
        {
            get
            {
                return this.distance;
            }
            set
            {
                this.distance = value;
                RaisePropertyChanged(() => this.Distance);
            }

        }

        private void OnError(MvxLocationError error)
        {
            Trace.Error("Seen location error {0}", error.Code);
        }
    }
}