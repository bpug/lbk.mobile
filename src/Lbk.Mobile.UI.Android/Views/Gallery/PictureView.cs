//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="RoomDetailView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Gallery
{
    using Android.App;
    using Android.OS;
    using Android.Views;

    using Cheesebaron.MvvmCross.Bindings.Droid;

    using DK.Ostebaronen.Droid.ViewPagerIndicator;

    using Lbk.Mobile.Core.ViewModels.Gallery;

    using Resource = Lbk.Mobile.UI.Droid.Resource;

    [Activity(Icon = "@drawable/ic_launcher")]
    public class PictureView : BaseView<PictureViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            this.RequestWindowFeature(WindowFeatures.NoTitle);
            //this.Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.Gallery_Picture);

            var pager = this.FindViewById<BindableViewPager>(Resource.Id.picture_pager);
            var indicator = this.FindViewById<UnderlinePageIndicator>(Resource.Id.picture_pager_indicator);
            indicator.SetViewPager(pager);
            indicator.CurrentItem = ViewModel.Current.PageIndex;

        }
    }
}