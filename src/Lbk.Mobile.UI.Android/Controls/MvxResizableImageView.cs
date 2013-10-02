//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MvxResizableImageView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Controls
{
    using System;

    using Android.Content;
    using Android.Graphics.Drawables;
    using Android.Runtime;
    using Android.Util;

    using Cirrious.MvvmCross.Binding.Droid.Views;

    public class MvxResizableImageView : MvxImageView
    {
        public MvxResizableImageView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        public MvxResizableImageView(Context context)
            : base(context)
        {
        }

        protected MvxResizableImageView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            Drawable drawable = this.Drawable;
            if (drawable != null)
            {
                float imageSideRatio = (float)drawable.IntrinsicWidth / (float)drawable.IntrinsicHeight;
                float viewSideRatio = (float)MeasureSpec.GetSize(widthMeasureSpec) / (float)MeasureSpec.GetSize(heightMeasureSpec);
                if (imageSideRatio >= viewSideRatio)
                {
                    // Image is wider than the display (ratio)
                    int width = MeasureSpec.GetSize(widthMeasureSpec);
                    int height = (int)(width / imageSideRatio);
                    SetMeasuredDimension(width, height);
                }
                else
                {
                    // Image is taller than the display (ratio)
                    int height = MeasureSpec.GetSize(heightMeasureSpec);
                    int width = (int)(height * imageSideRatio);
                    SetMeasuredDimension(width, height);
                }
            }
            else
            {
                base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            }
        }
    }
}