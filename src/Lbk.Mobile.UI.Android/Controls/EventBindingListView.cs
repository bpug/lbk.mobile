//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TodaysMenuListView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Controls
{
    using Android.Content;
    using Android.Util;

    public class EventBindingListView : BindingPullToRefreshListView
    {
        public EventBindingListView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            
        }

        protected override void Initialize()
        {
            this.Init(Resource.Layout.Event_ListView);
        }
    }
}