//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TodaysMenuListView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Controls.PullToRefresh
{
    using Android.Content;
    using Android.Util;

    public class TodaysMenuBindingListView : BindingPullToRefreshListView
    {
        public TodaysMenuBindingListView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            
        }

        protected override void Initialize()
        {
            this.Init(Resource.Layout.TodaysMenu_ListView);
        }
    }
}