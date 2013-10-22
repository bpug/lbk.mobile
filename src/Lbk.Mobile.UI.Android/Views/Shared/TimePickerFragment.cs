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
    using Android.Views;

    using Cirrious.MvvmCross.Binding.Droid.BindingContext;
    using Cirrious.MvvmCross.Binding.Droid.Views;
    using Cirrious.MvvmCross.Droid.Fragging.Fragments;

    public abstract class TimePickerFragment : MvxDialogFragment
    {
        private readonly Context context;

        private readonly int resourceId;

        private readonly string title;

        protected View TickerView;

        protected TimePickerFragment(Context context, string title, int resourceId)
        {
            this.context = context;
            this.title = title;
            this.resourceId = resourceId;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            base.EnsureBindingContextSet(savedInstanceState);

            TickerView = this.BindingInflate(this.resourceId, null);
            this.Init();
            var builder = new AlertDialog.Builder(this.context);
            builder.SetIcon(Resource.Drawable.ic_action_time);
            builder.SetTitle(this.title);
            builder.SetView(TickerView);
            builder.SetPositiveButton("Ok", (sender, args) => { });

            return builder.Create();
        }

        protected abstract void Init();
    }
}