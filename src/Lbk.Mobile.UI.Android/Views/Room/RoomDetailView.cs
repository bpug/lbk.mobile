//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="RoomDetailView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Room
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;

    using Cheesebaron.MvvmCross.Bindings.Droid;
    using Cirrious.MvvmCross.Binding.BindingContext;
    using DK.Ostebaronen.Droid.ViewPagerIndicator;
    using Lbk.Mobile.Core.ViewModels.Room;

    using Resource = Lbk.Mobile.UI.Droid.Resource;

    [Activity(Label = "Raum", Icon = "@drawable/ic_launcher", ScreenOrientation = ScreenOrientation.Portrait)]
    public class RoomDetailView : BaseView<RoomDetailViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Room_Details);

            var set = this.CreateBindingSet<RoomDetailView, RoomDetailViewModel>();
            set.Bind(this).For(v => v.Title).To(vm => vm.Room.Title);
            set.Apply();

            var pager = this.FindViewById<BindableViewPager>(Resource.Id.image_pager);
            var indicator = this.FindViewById<CirclePageIndicator>(Resource.Id.image_pager_indicator);
            if (pager != null && indicator != null)
            {
                indicator.SetViewPager(pager);
            }
        }
    }
}