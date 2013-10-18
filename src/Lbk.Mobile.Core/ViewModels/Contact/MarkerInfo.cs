//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MarkerInfo.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Contact
{
    using Cirrious.MvvmCross.ViewModels;

    public class MarkerInfo : MvxNotifyPropertyChanged
    {
        public string Description { get; set; }
        public Location Location { get; set; }
        public string Title { get; set; }

        public override bool Equals(object obj)
        {
            var lRhs = obj as MarkerInfo;
            if (lRhs == null)
            {
                return false;
            }

            var result = lRhs.Location.Equals(this.Location);// && lRhs.Title == this.Title;
            return result;
        }

        public override int GetHashCode()
        {
            var hashcode = this.Location.GetHashCode(); // + this.Title.GetHashCode();
            return hashcode;
        }
    }
}