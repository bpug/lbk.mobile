//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LbkNumberPickerFragment.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Reservation
{
    using Android.Content;
    using Android.Widget;

    using Lbk.Mobile.Core.ViewModels.Reservation;
    using Lbk.Mobile.UI.Droid.Views.Shared;

    public class LbkNumberPickerFragment : NumberPickerFragment
    {
        public LbkNumberPickerFragment(Context context, string title, int resourceId, int pickerId)
            : base(context, title, resourceId, pickerId)
        {
        }

        protected override void SetInitValue(NumberPicker picker)
        {
            var  vm  = this.ViewModel as ReservationFormViewModel;
            if (vm != null)
            {
                picker.Value = vm.Seats;
            }
            
        }
    }
}