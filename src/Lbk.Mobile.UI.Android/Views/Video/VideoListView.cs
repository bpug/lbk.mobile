//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="VideoListView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Video
{
    using Android.App;
    using Android.OS;

    using Lbk.Mobile.Core.ViewModels.Video;

    [Activity(Label = "Videos", Icon = "@drawable/ic_launcher")]
    public class VideoListView : BaseView<VideoListViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Video_List);
        }
    }
}