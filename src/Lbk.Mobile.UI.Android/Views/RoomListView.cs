//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="RoomListView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Android.Views
{
    using Cirrious.MvvmCross.Binding.BindingContext;
    using Cirrious.MvvmCross.Binding.Droid.Views;
    using Cirrious.MvvmCross.Droid.Views;
    using Cirrious.MvvmCross.ViewModels;

    using global::Android.App;
    using global::Android.OS;

    using Lbk.Mobile.Core.ViewModels.Room;

    [Activity(Label = "Räume")]
    public class RoomListView : BaseView<RoomListViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Room_List);
        }
    }
}