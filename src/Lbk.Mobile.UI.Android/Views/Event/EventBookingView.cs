//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EventListView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Android.Views.Event
{
    using global::Android.App;
    using global::Android.OS;

    using Lbk.Mobile.Core.ViewModels.Event;

    [Activity(Label = "Events", Icon = "@drawable/ic_launcher")]
    public class EventBookingView : BaseView<EventBookingViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Title = ViewModel.Title;
            this.SetContentView(Resource.Layout.Event_Booking);
        }
    }
}