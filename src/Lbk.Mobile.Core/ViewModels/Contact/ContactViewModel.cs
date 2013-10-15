//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ContactViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Contact
{
    using System.Windows.Input;

    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.Plugins.Location;
    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Common;
    using Lbk.Mobile.Common.Utils;

    public class ContactViewModel : BaseViewModel
    {
        private readonly IMvxGeoLocationWatcher watcher;

        public ContactViewModel()
        {
            this.watcher  = Mvx.Resolve<IMvxGeoLocationWatcher>();
            //this.watcher = watcher;
        }

        public bool IsStarted { get; set; }
        public double Distance { get; set; }

        public override void Start()
        {
            base.Start();
            DoStartStop();
        }


        public void StopWatcher()
        {
            if (this.watcher.Started)
            {
                this.watcher.Stop();
            }
        }

        private void DoStartStop()
        {
            if (!this.watcher.Started)
            {
                this.watcher.Start(new MvxGeoLocationOptions() { EnableHighAccuracy = true, Timeout = 60}, OnNewLocation, OnLocationError);
            }
            else
            {
                this.watcher.Stop();
            }
        }


        public ICommand StartWatcherCommand
        {
            get
            {
                return new MvxCommand(this.DoStartStop);
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

        private void OnLocationError(MvxLocationError error)
        {
            this.watcher.Stop();
            Trace.Error("Seen location error {0}", error.Code);
        }

        private void OnNewLocation(MvxGeoLocation location)
        {
            double lat = location.Coordinates.Latitude;
            double lng = location.Coordinates.Longitude;
            this.Distance = DistanceCalcs.DistanceInMetres(lat, lng, Constants.LbkLatitude, Constants.LbkLongitude);

            this.watcher.Stop();
        }
    }
}