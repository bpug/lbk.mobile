//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="NumberPickerFragment.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Shared
{
    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Widget;

    using Cirrious.MvvmCross.Binding.Droid.BindingContext;
    using Cirrious.MvvmCross.Droid.Fragging.Fragments;

    public class NumberPickerFragment : MvxDialogFragment
    {
        private readonly Context context;

        private readonly int pickerId;

        private readonly int resourceId;

        private readonly string title;

        //public NumberPickerFragment(Context context, string title, int resourceId, int pickerId)
        //{
        //    this.context = context;
        //    this.title = title;
        //    this.resourceId = resourceId;
        //    this.pickerId = pickerId;
        //}

        public NumberPickerFragment(Context context, string title, int resourceId)
        {
            this.context = context;
            this.title = title;
            this.resourceId = resourceId;
            //this.pickerId = pickerId;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            base.EnsureBindingContextSet(savedInstanceState);

            var view = this.BindingInflate(this.resourceId, null);
            //var picker = view.FindViewById<NumberPicker>(this.pickerId);

            //if (picker != null)
            //{
            //    this.SetInitValue(picker);
            //}

            var builder = new AlertDialog.Builder(this.context);
            //builder.SetIconAttribute(Resource.Attribute.CalendarViewShown);
            builder.SetIcon(Resource.Drawable.ic_action_group);
            builder.SetTitle(this.title);
            builder.SetView(view);
            builder.SetPositiveButton("Ok", (sender, args) => { });
            return builder.Create();
        }

        //protected abstract void SetInitValue(NumberPicker picker);
    }
}