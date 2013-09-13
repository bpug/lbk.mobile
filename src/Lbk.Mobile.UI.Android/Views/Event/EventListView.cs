//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EventListView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Event
{
    using Android.App;
    using Android.OS;

    using Lbk.Mobile.Core.ViewModels.Event;

    [Activity(Label = "Events", Icon = "@drawable/ic_launcher")]
    public class EventListView : BaseView<EventListViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Event_List);
        }
    }
}