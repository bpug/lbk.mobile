// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReservationView.cs" company="ip-connect GmbH">
//   Copyright (c) ip-connect GmbH. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views
{
    using Android.App;

    using AndroidClassLibrary;

    using Cirrious.MvvmCross.Binding.Droid.Views;
    
    using Lbk.Mobile.Portable.Core.ViewModels.Reservation;

    using PortableTest;

    using Resource = Lbk.Mobile.UI.Droid.Resource;

    [Activity(Label = "Reservierung")]
    public class ReservationView : MvxBindingActivityView<ReservationViewModel>
    {
        protected override void OnViewModelSet()
        {
            this.SetContentView(Resource.Layout.Page_Reservation);

            var test = Util4.Test("Boris");

            var test2 = Util2.Test("Boris");
        }
    }
}