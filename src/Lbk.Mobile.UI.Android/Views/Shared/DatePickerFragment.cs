//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DatePickerFragment.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Shared
{
    using Android.App;
    using Android.Content;
    using Android.OS;

    using Cirrious.MvvmCross.Binding.Droid.BindingContext;
    using Cirrious.MvvmCross.Droid.Fragging.Fragments;

    public class DatePickerFragment : MvxDialogFragment
    {
        private readonly Context context;

        public DatePickerFragment(Context context)
        {
            this.context = context;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            base.EnsureBindingContextSet(savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.Dialog_DatePicker, null);
            //var dp = view.FindViewById<MvxDatePicker>(Resource.Id.mvxdate_picker);

            var builder = new AlertDialog.Builder(this.context);
            builder.SetIconAttribute(Android.Resource.Attribute.CalendarViewShown);
            builder.SetTitle("Datum");
            builder.SetView(view);
            builder.SetPositiveButton("Ok", (sender, args) => { });
            return builder.Create();
        }
    }
}