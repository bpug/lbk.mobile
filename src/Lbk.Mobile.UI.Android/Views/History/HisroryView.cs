//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="HisroryView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.History
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;

    using Cheesebaron.MvvmCross.Bindings.Droid;

    using DK.Ostebaronen.Droid.ViewPagerIndicator;

    using Lbk.Mobile.Core.ViewModels.History;

    using Resource = Lbk.Mobile.UI.Droid.Resource;

    [Activity(Label = "Historie", Icon = "@drawable/ic_launcher", ScreenOrientation = ScreenOrientation.Portrait)]
    public class HisroryView : BaseView<HistoryViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.History);

            var pager = this.FindViewById<BindableViewPager>(Resource.Id.history_pager);
            var indicator = this.FindViewById<CirclePageIndicator>(Resource.Id.history_pager_indicator);
            indicator.SetViewPager(pager);
        }
    }
}