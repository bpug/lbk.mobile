//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TodaysMenuListView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Test.Controls.PullToRefresh
{
    using Android.Content;
    using Android.Util;

    public class VideoBindingListView : BindingPullToRefreshListView
    {
        public VideoBindingListView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            
        }

        protected override void Initialize()
        {
            this.Init(Resource.Layout.Video_ListView);
        }
    }
}