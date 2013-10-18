//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MapViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Contact
{
    using System.Collections.ObjectModel;

    using Cirrious.MvvmCross.Plugins.Location;

    using Lbk.Mobile.Common;
    using Lbk.Mobile.Common.Utils;

    public class MapViewModel : BaseViewModel
    {
        private readonly IMvxLocationWatcher watcher;
        public ObservableCollection<MarkerInfo> MarkerInfos { get; set; }

        public MapViewModel(IMvxLocationWatcher watcher)
        {
            this.watcher = watcher;
            this.AddLbkLocation();
        }

        private void AddLbkLocation()
        {
            MarkerInfos = new ObservableCollection<MarkerInfo>
            {
                new MarkerInfo
                {
                    Title = Constants.LbkTitle,
                    Description = Constants.LbkAddress,
                    Location = new Location
                    {
                        Lat = Constants.LbkLatitude,
                        Lng = Constants.LbkLongitude
                    }
                }
            };
        }


        private void AddCurrentLocation(Location currentLocation)
        {
            if (currentLocation == null)
            {
                return;
            }
            var currentLocationInfo = new MarkerInfo
            {
                Location = currentLocation
            };
            if (!MarkerInfos.Contains(currentLocationInfo))
            {
                MarkerInfos.Add(currentLocationInfo);
            }
        }

        public void Init()
        {
           this.StartWatcher();
        }

        public override void Start()
        {
            base.Start();
            this.AddCurrentLocation(this.GetCurrentLocation());
        }

        private Location GetCurrentLocation()
        {
            var currentLocation = this.watcher.CurrentLocation;
            if (currentLocation != null)
            {
                var loc = new Location
                {
                    Lat = currentLocation.Coordinates.Latitude,
                    Lng = currentLocation.Coordinates.Longitude
                };
                AddCurrentLocation(loc);
                this.StopWatcher();
                return loc;
            }
            return null;
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
            
            var currentLocation = new Location
            {
                Lat = location.Coordinates.Latitude,
                Lng = location.Coordinates.Longitude
            };

            AddCurrentLocation(currentLocation);
            this.watcher.Stop();
        }

       

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