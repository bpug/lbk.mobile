//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LbkTimePickerFragment.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Reservation
{
    using Android.Content;

    using Cirrious.MvvmCross.Binding.Droid.Views;
    using Java.Lang;
    using Lbk.Mobile.Core.ViewModels.Reservation;
    using Lbk.Mobile.UI.Droid.Views.Shared;

    public class LbkTimePickerFragment : TimePickerFragment
    {
        private readonly bool is24HourView;

        private readonly int tickerId;

        public LbkTimePickerFragment(Context context, string title, int resourceId,int tickerId, bool is24HourView = true)
            : base(context, title, resourceId)
        {
            this.is24HourView = is24HourView;
            this.tickerId = tickerId;
        }

        protected override void Init()
        {
            var timePicker = TickerView.FindViewById<MvxTimePicker>(tickerId);
            if (timePicker != null && this.is24HourView)
            {
                timePicker.SetIs24HourView(Boolean.True);
                var vm = this.ViewModel as ReservationFormViewModel;
                if (vm != null)
                {
                    timePicker.CurrentHour = new Integer(vm.Time.Hours);
                    timePicker.CurrentMinute = new Integer(vm.Time.Minutes);
                }
            }
            
        }
    }
}