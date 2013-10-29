//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DynamicImageView .cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Controls
{
    using System;

    using Android.Content;
    using Android.Util;

    using Cirrious.MvvmCross.Binding.Droid.Views;

    public class DynamicImageView : MvxImageView
    {
        public DynamicImageView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        public DynamicImageView(Context context)
            : base(context)
        {
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            var d = this.Drawable;

            if (d != null)
            {
                // ceil not round - avoid thin vertical gaps along the left/right edges
                int width = MeasureSpec.GetSize(widthMeasureSpec);
                int height = (int)Math.Ceiling(width * (float)d.IntrinsicHeight / d.IntrinsicWidth);
                this.SetMeasuredDimension(width, height);
            }
            else
            {
                base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            }
        }
    }
}