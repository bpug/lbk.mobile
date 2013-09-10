//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="RoomDetailView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Android.Views
{
    using Cirrious.MvvmCross.Droid.Views;

    using global::Android.App;
    using global::Android.OS;

    using Lbk.Mobile.Core.ViewModels.Room;

    [Activity(Label = "Raum")]
    public class RoomDetailView : BaseView<RoomDetailViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.Title = this.ViewModel.Room.Title;
            this.SetContentView(Resource.Layout.Room_Details);
        }
    }
}