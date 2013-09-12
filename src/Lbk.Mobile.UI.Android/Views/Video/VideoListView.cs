//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="VideoListView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Android.Views.Video
{
    using global::Android.App;
    using global::Android.OS;

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