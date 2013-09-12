//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="RoomListView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Android.Views.Room
{
    using global::Android.App;
    using global::Android.OS;

    using Lbk.Mobile.Core.ViewModels.Room;

    [Activity(Label = "R�ume", Icon = "@drawable/ic_launcher")]
    public class RoomListView : BaseView<RoomListViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Room_List);
        }
    }
    
}