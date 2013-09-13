//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="RoomListView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Room
{
    using Android.App;
    using Android.OS;

    using Lbk.Mobile.Core.ViewModels.Room;

    [Activity(Label = "Räume", Icon = "@drawable/ic_launcher")]
    public class RoomListView : BaseView<RoomListViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Room_List);
        }
    }
    
}