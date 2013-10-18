//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TimePickerFragment.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Shared
{
    using Android.App;
    using Android.Content;
    using Android.OS;

    using Cirrious.MvvmCross.Binding.Droid.BindingContext;
    using Cirrious.MvvmCross.Binding.Droid.Views;
    using Cirrious.MvvmCross.Droid.Fragging.Fragments;

    using Java.Lang;

    using Lbk.Mobile.Core.ViewModels.Reservation;

    public class TimePickerFragment : MvxDialogFragment
    {
        private readonly Context context;

        private readonly bool is24HourView;

        public TimePickerFragment(Context context, bool is24HourView = true)
        {
            this.context = context;
            this.is24HourView = is24HourView;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            base.EnsureBindingContextSet(savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.Dialog_TimePicker, null);
            var timePicker = view.FindViewById<MvxTimePicker>(Resource.Id.mvxtime_picker);
            if (timePicker != null)
            {
                if (this.is24HourView)
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

            var builder = new AlertDialog.Builder(this.context);
            builder.SetIconAttribute(Android.Resource.Attribute.CalendarViewShown);
            builder.SetTitle("Zeit");
            builder.SetView(view);
            builder.SetPositiveButton("Ok", (sender, args) => { });

            return builder.Create();
        }
    }
}