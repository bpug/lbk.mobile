//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Location.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Contact
{
    using Cirrious.MvvmCross.ViewModels;

    public class Location : MvxNotifyPropertyChanged
    {
        public double Lat { get; set; }

        public double Lng { get; set; }

        public override bool Equals(object obj)
        {
            var lRhs = obj as Location;
            if (lRhs == null)
            {
                return false;
            }

            return lRhs.Lat == this.Lat && lRhs.Lng == this.Lng;
        }

        public override int GetHashCode()
        {
            return this.Lat.GetHashCode() + this.Lng.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0:0.00000} {1:0.00000}", this.Lat, this.Lng);
        }
    }
}