//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MvxTouchImageView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Controls
{
    using System;

    using Android.Content;
    using Android.Graphics;
    using Android.Runtime;
    using Android.Util;
    using Android.Views;

    using Cirrious.MvvmCross.Binding.Droid.Views;

    public class MvxTouchImageView : MvxImageView
    {
        protected float OrigHeight;

        protected float OrigWidth;

        private const int Click = 3;

        private Context context;

        private PointF last = new PointF();

        private float[] m;

        private ScaleGestureDetector mScaleDetector;

        private Matrix matrix;

        private float maxScale = 3f;

        private float minScale = 1f;

        private static State mode = State.None;

        private int oldMeasuredHeight;

        private int oldMeasuredWidth;

        private float saveScale = 1f;

        private PointF start = new PointF();

        private int viewHeight;

        private int viewWidth;

        public MvxTouchImageView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        public MvxTouchImageView(Context context)
            : base(context)
        {
        }

        protected MvxTouchImageView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        private enum State
        {
            None = 0,

            Drag = 1,

            Zoom = 2
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            this.mScaleDetector.OnTouchEvent(e);
            var curr = new PointF(e.GetX(), e.GetY());

            switch (e.Action)
            {
                case MotionEventActions.Down:
                    this.last.Set(curr);
                    this.start.Set(this.last);
                    this.mode = State.Drag;
                    break;

                case MotionEventActions.Move:
                    if (this.mode == State.Drag)
                    {
                        float deltaX = curr.X - this.last.X;
                        float deltaY = curr.Y - this.last.Y;
                        float fixTransX = this.GetFixDragTrans(deltaX, this.viewWidth, this.OrigWidth * this.saveScale);
                        float fixTransY = this.GetFixDragTrans(
                            deltaY,
                            this.viewHeight,
                            this.OrigHeight * this.saveScale);
                        this.matrix.PostTranslate(fixTransX, fixTransY);
                        this.FixTrans();
                        this.last.Set(curr.X, curr.X);
                    }
                    break;

                case MotionEventActions.Up:
                    this.mode = State.None;
                    int xDiff = (int)Math.Abs(curr.X - this.start.X);
                    int yDiff = (int)Math.Abs(curr.Y - this.start.Y);
                    if (xDiff < Click && yDiff < Click)
                    {
                        this.PerformClick();
                    }
                    break;

                case MotionEventActions.Pointer1Up:
                    this.mode = State.None;
                    break;
            }

            this.ImageMatrix = this.matrix;
            this.Invalidate();
            return true; // indicate event was handled
        }

        public void SetMaxZoom(float x)
        {
            this.maxScale = x;
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);

            this.viewWidth = MeasureSpec.GetSize(widthMeasureSpec);
            this.viewHeight = MeasureSpec.GetSize(heightMeasureSpec);

            //
            // Rescales image on rotation
            //
            if (this.oldMeasuredHeight == this.viewWidth && this.oldMeasuredHeight == this.viewHeight
                || this.viewWidth == 0 || this.viewHeight == 0)
            {
                return;
            }
            this.oldMeasuredHeight = this.viewHeight;
            this.oldMeasuredWidth = this.viewWidth;

            if (this.saveScale == 1)
            {
                //Fit to screen.
                float scale;

                var drawable = this.Drawable;
                if (drawable == null || drawable.IntrinsicWidth == 0 || drawable.IntrinsicHeight == 0)
                {
                    return;
                }
                int bmWidth = drawable.IntrinsicWidth;
                int bmHeight = drawable.IntrinsicHeight;

                //Log.d("bmSize", "bmWidth: " + bmWidth + " bmHeight : " + bmHeight);

                float scaleX = (float)this.viewWidth / (float)bmWidth;
                float scaleY = (float)this.viewHeight / (float)bmHeight;
                scale = Math.Min(scaleX, scaleY);
                this.matrix.SetScale(scale, scale);

                // Center the image
                float redundantYSpace = (float)this.viewHeight - (scale * (float)bmHeight);
                float redundantXSpace = (float)this.viewWidth - (scale * (float)bmWidth);
                redundantYSpace /= (float)2;
                redundantXSpace /= (float)2;

                this.matrix.PostTranslate(redundantXSpace, redundantYSpace);

                this.OrigWidth = this.viewWidth - 2 * redundantXSpace;
                this.OrigHeight = this.viewHeight - 2 * redundantYSpace;
                this.ImageMatrix = this.matrix;
            }
            this.FixTrans();
        }

        private void FixTrans()
        {
            this.matrix.GetValues(this.m);
            float transX = this.m[Matrix.MtransX];
            float transY = this.m[Matrix.MtransY];

            float fixTransX = this.GetFixTrans(transX, this.viewWidth, this.OrigWidth * this.saveScale);
            float fixTransY = this.GetFixTrans(transY, this.viewHeight, this.OrigHeight * this.saveScale);

            if (fixTransX != 0 || fixTransY != 0)
            {
                this.matrix.PostTranslate(fixTransX, fixTransY);
            }
        }

        private float GetFixDragTrans(float delta, float viewSize, float contentSize)
        {
            if (contentSize <= viewSize)
            {
                return 0;
            }
            return delta;
        }

        private float GetFixTrans(float trans, float viewSize, float contentSize)
        {
            float minTrans, maxTrans;

            if (contentSize <= viewSize)
            {
                minTrans = 0;
                maxTrans = viewSize - contentSize;
            }
            else
            {
                minTrans = viewSize - contentSize;
                maxTrans = 0;
            }

            if (trans < minTrans)
            {
                return -trans + minTrans;
            }
            if (trans > maxTrans)
            {
                return -trans + maxTrans;
            }
            return 0;
        }

        private void SharedConstructing(Context context)
        {
            base.Clickable = true;
            this.context = context;

            this.mScaleDetector = new ScaleGestureDetector(context, new ScaleListener());
            this.matrix = new Matrix();
            this.m = new float[9];
            this.ImageMatrix = this.matrix;
            this.SetScaleType(ScaleType.Matrix);
        }

        private class ScaleListener : ScaleGestureDetector.SimpleOnScaleGestureListener {
        
        public override bool OnScaleBegin(ScaleGestureDetector detector) {
            mode = State.Zoom;
            return true;
        }


        public override bool OnScale(ScaleGestureDetector detector)
        {
            float mScaleFactor = detector.ScaleFactor;
            float origScale = saveScale;
            saveScale *= mScaleFactor;
            if (saveScale > maxScale) {
                saveScale = maxScale;
                mScaleFactor = maxScale / origScale;
            } else if (saveScale < minScale) {
                saveScale = minScale;
                mScaleFactor = minScale / origScale;
            }

            if (origWidth * saveScale <= viewWidth || origHeight * saveScale <= viewHeight)
                matrix.postScale(mScaleFactor, mScaleFactor, viewWidth / 2, viewHeight / 2);
            else
                matrix.postScale(mScaleFactor, mScaleFactor, detector.getFocusX(), detector.getFocusY());

            fixTrans();
            return true;
        }
    }
    }
}