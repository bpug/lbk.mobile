//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EventListView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Event
{
    using Android.App;
    using Android.OS;
    using Android.Webkit;

    using Lbk.Mobile.Core.ViewModels.Event;

    [Activity(Label = "Events", Icon = "@drawable/ic_launcher")]
    public class EventBookingView : BaseView<EventBookingViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.Title = this.ViewModel.Title;
            this.SetContentView(Resource.Layout.Event_Booking);

            WebView browser = (WebView)FindViewById(Resource.Id.webView_booking);
            browser.Settings.LoadWithOverviewMode = true;
            browser.Settings.UseWideViewPort = true;
            browser.Settings.BuiltInZoomControls = true;
        }
    }
}