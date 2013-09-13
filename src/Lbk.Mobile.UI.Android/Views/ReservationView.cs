//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ReservationView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views
{
    using Cirrious.MvvmCross.Droid.Views;

    using Android.App;
    using Android.OS;

    [Activity(Label = "Reservierung")]
    public class ReservationView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Reservation);
        }
    }
}