//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MapViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Contact
{
    using Cirrious.MvvmCross.Plugins.Location;

    using Lbk.Mobile.Common;
    using Lbk.Mobile.Common.Utils;

    public class MapViewModel : BaseViewModel
    {
        private readonly IMvxLocationWatcher watcher;

        public MapViewModel(IMvxLocationWatcher watcher)
        {
            this.watcher = watcher;
            this.LbkInfo = new MarkerInfo
            {
                Title = Constants.LbkTitle,
                Description = Constants.LbkAddress,
                Location = new Location
                {
                    Lat = Constants.LbkLatitude,
                    Lng = Constants.LbkLongitude
                }
            };
        }

        public void Init()
        {
           this.StartWatcher();
        }

        public override void Start()
        {
            base.Start();
            SetCurrentLocation();
        }

        private void SetCurrentLocation()
        {
            var currentLocation = this.watcher.CurrentLocation;
            if (currentLocation != null)
            {
                this.CurrentLocation = new Location
                {
                    Lat = currentLocation.Coordinates.Latitude,
                    Lng = currentLocation.Coordinates.Longitude
                };
                this.StopWatcher();
            }
        }

        public void StopWatcher()
        {
            if (this.watcher.Started)
            {
                this.watcher.Stop();
            }
        }

        private void StartWatcher()
        {
            if (!this.watcher.Started)
            {
                this.watcher.Start(
                    new MvxLocationOptions
                    {
                        Accuracy = MvxLocationAccuracy.Fine
                    },
                    this.OnNewLocation,
                    this.OnLocationError);
            }
        }

        private void OnLocationError(MvxLocationError error)
        {
            this.watcher.Stop();
            Trace.Error("Seen location error {0}", error.Code);
        }

        private void OnNewLocation(MvxGeoLocation location)
        {
            
            this.CurrentLocation = new Location
            {
                Lat = location.Coordinates.Latitude,
                Lng = location.Coordinates.Longitude
            };

            //this.watcher.Stop();
        }

        public Location CurrentLocation { get; set; }
        public MarkerInfo LbkInfo { get; set; }

        //public IMvxCommand UpdateCenterCommand
        //{
        //    get
        //    {
        //        return new MvxCommand(() =>
        //        {
        //            Location = new Location()
        //            {
        //                Lat = Lat,
        //                Lng = Lng
        //            };
        //        });
        //    }
        //}
    }
}