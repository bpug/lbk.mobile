//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="VerticalScrollview.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Controls
{
    using Android.Content;
    using Android.Util;
    using Android.Views;
    using Android.Widget;

    internal class VerticalScrollview : ScrollView
    {
        public VerticalScrollview(Context context)
            : base(context)
        {
        }

        public VerticalScrollview(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        public VerticalScrollview(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            var action = ev.Action;
            switch (action)
            {
                case MotionEventActions.Down:
                    base.OnTouchEvent(ev);
                    break;

                case MotionEventActions.Move:
                    return false; // redirect MotionEvents to ourself

                case MotionEventActions.Cancel:

                    base.OnTouchEvent(ev);
                    break;

                case MotionEventActions.Up:
                    return false;

                default:
                    break;
            }

            return false;
        }
    }
}