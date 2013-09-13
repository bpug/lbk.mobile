//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="RoomDetailView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Room
{
    using Android.App;
    using Android.OS;

    using Cheesebaron.MvvmCross.Bindings.Droid;

    using DK.Ostebaronen.Droid.ViewPagerIndicator;

    using Lbk.Mobile.Core.ViewModels.Room;

    using Resource = Lbk.Mobile.UI.Droid.Resource;

    [Activity(Label = "Raum", Icon = "@drawable/ic_launcher")]
    public class RoomDetailView : BaseView<RoomDetailViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.Title = this.ViewModel.Room.Title;
            this.SetContentView(Resource.Layout.Room_Details);

            var pager = this.FindViewById<BindableViewPager>(Resource.Id.image_pager);
            var indicator = this.FindViewById<CirclePageIndicator>(Resource.Id.image_pager_indicator);
            indicator.SetViewPager(pager);
        }
    }
}