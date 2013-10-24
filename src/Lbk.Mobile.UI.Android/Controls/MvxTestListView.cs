//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MvxTestListView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Controls
{
    using Android.Content;
    using Android.Util;

    using Cirrious.MvvmCross.Binding.Droid.Views;

    public class MvxTestListView : MvxListView
    {
        public MvxTestListView(Context context, IAttributeSet attrs): base(context, attrs)
        {
        }

        public MvxTestListView(Context context, IAttributeSet attrs, IMvxAdapter adapter)
            : base(context, attrs, adapter)
        {
        }
    }
}