//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ReservationView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Reservation
{
    using Android.App;
    using Android.OS;
    using Android.Widget;

    using Lbk.Mobile.Core.ViewModels.Reservation;
    using Lbk.Mobile.UI.Droid.Views.Shared;

    [Activity(Label = "Reservierung")]
    public class ReservationFormView : BaseFragmentActivity<ReservationFormViewModel>
    {
        const int TimeDialogId = 0;
        const int DateDialogId = 1;
        const int NumberDialogId = 2;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Reservation_Form);

            Button pickDate = FindViewById<Button>(Resource.Id.pick_date);
            pickDate.Click += delegate
            {
                ShowDialog(DateDialogId);
            };

            Button pickTime = FindViewById<Button>(Resource.Id.pick_time);
            pickTime.Click += delegate
            {
                ShowDialog(TimeDialogId);
            };

            Button pickNumber = FindViewById<Button>(Resource.Id.pick_number);
            pickNumber.Click += delegate
            {
                ShowDialog(NumberDialogId);
            };
        }

        protected override Dialog OnCreateDialog(int id)
        {
            switch (id)
            {
                case DateDialogId:
                    var dialogDate = new DatePickerFragment(this)
                    {
                        ViewModel = this.ViewModel
                    };
                    dialogDate.Show(SupportFragmentManager, "DatePickerDialog"); 
                    break;
                case TimeDialogId:
                    var dialogTime = new TimePickerFragment(this)
                    {
                        ViewModel = this.ViewModel
                    };
                    dialogTime.Show(SupportFragmentManager, "TimePickerDialog"); 
                    break;
                case NumberDialogId:
                    var dialogNumber = new NumberPickerFragment(this)
                    {
                        ViewModel = this.ViewModel
                    };
                    dialogNumber.Show(SupportFragmentManager, "NumberPickerDialog"); 
                    break;
                   
            }
            return null;
        }
    }
}