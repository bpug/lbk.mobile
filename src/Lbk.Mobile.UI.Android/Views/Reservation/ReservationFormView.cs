//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ReservationView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Reservation
{
    using Android.App;
    using Android.OS;

    using Lbk.Mobile.Core.ViewModels.Reservation;

    [Activity(Label = "Reservierung")]
    public class ReservationFormView : BaseFragmentActivity<ReservationFormViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Reservation_Form);
        }
    }
}