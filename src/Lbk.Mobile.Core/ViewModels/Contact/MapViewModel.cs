//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MapViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Contact
{
    public class MapViewModel : BaseViewModel
    {
        public MapViewModel()
        {
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