//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ExpandCollapseAnimation.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Controls
{
    using Android.App;
    using Android.Util;
    using Android.Views;
    using Android.Views.Animations;
    using Android.Widget;

    public class ExpandCollapseAnimation : Animation
    {
        private readonly View animatedView;

        private readonly int endHeight;

        private readonly Type type;

        public enum Type
        {
            Expand = 0,
            Collapse = 1
        }

        /**
     * Initializes expand collapse animation, has two types, collapse (1) and expand (0).
     * @param view The view to animate
     * @param duration
     * @param type The type of animation: 0 will expand from gone and 0 size to visible and layout size defined in xml. 
     * 1 will collapse view and set to gone
     */

        public ExpandCollapseAnimation(View view, int duration, Type type, Activity activity)
        {
            this.Duration = duration;
            this.animatedView = view;

            SetHeightForWrapContent(activity, view);
            this.endHeight = this.animatedView.LayoutParameters.Height;
            this.type = type;
            if (this.type == Type.Expand)
            {
                this.animatedView.LayoutParameters.Height = 0;
                this.animatedView.Visibility = ViewStates.Visible;
            }
        }

        public static void SetHeightForWrapContent(Activity activity, View view)
        {
            var metrics = new DisplayMetrics();
            activity.WindowManager.DefaultDisplay.GetMetrics(metrics);

            int screenWidth = metrics.WidthPixels;

            int heightMeasureSpec = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);
            int widthMeasureSpec = View.MeasureSpec.MakeMeasureSpec(screenWidth, MeasureSpecMode.Exactly);

            view.Measure(widthMeasureSpec, heightMeasureSpec);
            int height = view.MeasuredHeight;
            view.LayoutParameters.Height = height;
        }

        protected override void ApplyTransformation(float interpolatedTime, Transformation t)
        {
            base.ApplyTransformation(interpolatedTime, t);
            if (interpolatedTime < 1.0f)
            {
                if (this.type == Type.Expand)
                {
                    this.animatedView.LayoutParameters.Height = (int)(this.endHeight * interpolatedTime);
                }
                else
                {
                    this.animatedView.LayoutParameters.Height = this.endHeight
                                                                - (int)(this.endHeight * interpolatedTime);
                }
                this.animatedView.RequestLayout();
            }
            else
            {
                if (this.type == Type.Expand)
                {
                    this.animatedView.LayoutParameters.Height = this.endHeight;
                    this.animatedView.RequestLayout();
                }
                else
                {
                    this.animatedView.LayoutParameters.Height = 0;
                    this.animatedView.Visibility = ViewStates.Gone;
                    this.animatedView.RequestLayout();
                    this.animatedView.LayoutParameters.Height = ViewGroup.LayoutParams.WrapContent; //this.endHeight;
                }
            }
        }
    }
}