//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ContactViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Contact
{
    using System.Windows.Input;

    using Cirrious.MvvmCross.Plugins.Location;
    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Common;
    using Lbk.Mobile.Common.Utils;

    public class ContactViewModel : BaseViewModel
    {
        private readonly IMvxGeoLocationWatcher watcher;

        private double distance;

        public ContactViewModel(IMvxGeoLocationWatcher watcher)
        {
            this.watcher = watcher;
            watcher.Start(
                new MvxGeoLocationOptions
                {
                    EnableHighAccuracy = true
                },
                this.OnLocation,
                this.OnLocationError);
        }

        public double Distance
        {
            get
            {
                return this.distance;
            }
            set
            {
                this.distance = value;
                this.RaisePropertyChanged(() => this.Distance);
            }
        }

        public ICommand EmailCommand
        {
            get
            {
                return new MvxCommand(() => this.ComposeEmail(Constants.LbkEmail, "Löwenbräu", string.Empty));
            }
        }

        public ICommand PhoneCallCommand
        {
            get
            {
                return new MvxCommand(() => this.MakePhoneCall(string.Empty, Constants.LbkPhone));
            }
        }

        public ICommand ShowAppInfoCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<AppInfoViewModel>());
            }
        }

        public ICommand ShowMapCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<MapViewModel>());
            }
        }

        private void OnLocationError(MvxLocationError error)
        {
            Trace.Error("Seen location error {0}", error.Code);
        }

        private void OnLocation(MvxGeoLocation location)
        {
            double lat = location.Coordinates.Latitude;
            double lng = location.Coordinates.Longitude;
            this.Distance = DistanceCalcs.DistanceInMetres(lat, lng, Constants.LbkLatitude, Constants.LbkLongitude);

            this.watcher.Stop();
        }
    }
}