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
        private readonly int resourceId;
        private readonly string title;

        public DatePickerFragment(Context context, string title, int resourceId)
        {
            this.context = context;
            this.title = title;
            this.resourceId = resourceId;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            base.EnsureBindingContextSet(savedInstanceState);

            var view = this.BindingInflate(resourceId, null);
            //var dp = view.FindViewById<MvxDatePicker>(Resource.Id.mvxdate_picker);

            var builder = new AlertDialog.Builder(this.context);
            builder.SetIcon(Resource.Drawable.ic_action_event);
            builder.SetTitle(title);
            builder.SetView(view);
            builder.SetPositiveButton("Ok", (sender, args) => { });
            return builder.Create();
        }
    }
}