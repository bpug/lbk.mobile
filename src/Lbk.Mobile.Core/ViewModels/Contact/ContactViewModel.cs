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
        private readonly IMvxLocationWatcher watcher;

        public ContactViewModel(IMvxLocationWatcher watcher)
        {
            this.watcher = watcher;
        }

        public ICommand CurrentLocationCommand
        {
            get
            {
                return new MvxCommand(this.CurrentLocationExecute);
            }
        }

        public double Distance { get; set; }

        public ICommand EmailCommand
        {
            get
            {
                return new MvxCommand(() => this.ComposeEmail(Constants.LbkEmail, "Löwenbräu", string.Empty));
            }
        }

        public void Init()
        {
            this.StartWatcher();
        }

        public bool IsStarted { get; set; }

        public ICommand PhoneCallCommand
        {
            get
            {
                return new MvxCommand(() => this.MakePhoneCall(string.Empty, Constants.LbkPhone));
            }
        }

        public ICommand ShowImpressumCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<ImpressumViewModel>());
            }
        }

        public ICommand ShowMapCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<MapViewModel>());
            }
        }

        public override void Start()
        {
            base.Start();
            this.CurrentLocationExecute();
        }

        public void StopWatcher()
        {
            if (this.watcher.Started)
            {
                this.watcher.Stop();
            }
        }

        private void CalculateDistance(MvxGeoLocation location)
        {
            double lat = location.Coordinates.Latitude;
            double lng = location.Coordinates.Longitude;
            this.Distance = DistanceCalcs.DistanceInMetres(lat, lng, Constants.LbkLatitude, Constants.LbkLongitude);
        }

        private void CurrentLocationExecute()
        {
            var currentLocation = this.watcher.CurrentLocation;
            if (currentLocation != null)
            {
                this.CalculateDistance(currentLocation);
                this.StopWatcher();
            }
        }

        private void OnLocationError(MvxLocationError error)
        {
            this.watcher.Stop();
            Trace.Error("Seen location error {0}", error.Code);
        }

        private void OnNewLocation(MvxGeoLocation location)
        {
            this.CalculateDistance(location);
            this.watcher.Stop();
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
            else
            {
                this.watcher.Stop();
            }
        }
    }
}