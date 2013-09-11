//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TodaysMenuListView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Android.Controls
{
    using global::Android.Content;
    using global::Android.Util;

    public class EventListView : BindingPullToRefreshListView
    {
        public EventListView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            
        }

        protected override void Initialize()
        {
            this.Init(Resource.Layout.Event_ListView);
        }
    }
}