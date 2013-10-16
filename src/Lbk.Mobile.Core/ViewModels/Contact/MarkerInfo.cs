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
    }
}