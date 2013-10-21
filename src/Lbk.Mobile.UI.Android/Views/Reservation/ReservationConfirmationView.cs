//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ReservationConfirmationView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Reservation
{
    using Android.App;
    using Android.OS;
    using Android.Views;

    using Lbk.Mobile.Core.ViewModels.Reservation;

    [Activity(Label = "Bestätigung Code", Icon = "@drawable/ic_launcher", NoHistory = true)]
    public class ReservationConfirmationView : BaseView<ReservationConfirmationViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Reservation_Confirmation);
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back)
            {
                this.ViewModel.BackCommand.Execute(null);
                return false;
            }
            return base.OnKeyDown(keyCode, e);
        }
    }
}