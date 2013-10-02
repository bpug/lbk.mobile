//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MvxScaleImageView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Controls
{
    using Android.Content;
    using Android.Graphics;
    using Android.Util;
    using Android.Views;

    using Cirrious.MvvmCross.Binding.Droid.Views;

    public class MvxScaleImageView : MvxImageView, View.IOnTouchListener
    {
        private readonly Context mContext;

        private readonly float[] mMatrixValues = new float[9];

        private GestureDetector mGestureDetector;

        private int mHeight;

        private int mIntrinsicHeight;

        private int mIntrinsicWidth;

        private bool mIsScaling;

        private Matrix mMatrix;

        private float mMaxScale = 2.0f;

        private float mMinScale;

        private float mPreviousDistance;

        private int mPreviousMoveX;

        private int mPreviousMoveY;

        private float mScale;

        private int mWidth;

        public MvxScaleImageView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            this.mContext = context;
            this.Initialize();
        }

        public MvxScaleImageView(Context context)
            : base(context)
        {
            this.mContext = context;
            this.Initialize();
        }

        //public MvxScaleImageView(Context context, IAttributeSet attrs, int defStyle) :
        //    base(context, attrs, defStyle)
        //{
        //    m_Context = context;
        //    Initialize();
        //}

        public float Scale
        {
            get
            {
                return this.GetValue(this.mMatrix, Matrix.MscaleX);
            }
        }

        public float TranslateX
        {
            get
            {
                return this.GetValue(this.mMatrix, Matrix.MtransX);
            }
        }

        public float TranslateY
        {
            get
            {
                return this.GetValue(this.mMatrix, Matrix.MtransY);
            }
        }

        public void Cutting()
        {
            int width = (int)(this.mIntrinsicWidth * this.Scale);
            int height = (int)(this.mIntrinsicHeight * this.Scale);
            if (this.TranslateX < -(width - this.mWidth))
            {
                this.mMatrix.PostTranslate(-(this.TranslateX + width - this.mWidth), 0);
            }

            if (this.TranslateX > 0)
            {
                this.mMatrix.PostTranslate(-this.TranslateX, 0);
            }

            if (this.TranslateY < -(height - this.mHeight))
            {
                this.mMatrix.PostTranslate(0, -(this.TranslateY + height - this.mHeight));
            }

            if (this.TranslateY > 0)
            {
                this.mMatrix.PostTranslate(0, -this.TranslateY);
            }

            if (width < this.mWidth)
            {
                this.mMatrix.PostTranslate((this.mWidth - width) / 2, 0);
            }

            if (height < this.mHeight)
            {
                this.mMatrix.PostTranslate(0, (this.mHeight - height) / 2);
            }

            this.ImageMatrix = this.mMatrix;
        }

        public void MaxZoomTo(int x, int y)
        {
            if (this.mMinScale != this.Scale && (this.Scale - this.mMinScale) > 0.1f)
            {
                float scale = this.mMinScale / this.Scale;
                this.ZoomTo(scale, x, y);
            }
            else
            {
                float scale = this.mMaxScale / this.Scale;
                this.ZoomTo(scale, x, y);
            }
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            return this.OnTouchEvent(e);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (this.mGestureDetector.OnTouchEvent(e))
            {
                return true;
            }

            int touchCount = e.PointerCount;
            switch (e.Action)
            {
                case MotionEventActions.Down:
                case MotionEventActions.Pointer1Down:
                case MotionEventActions.Pointer2Down:
                {
                    if (touchCount >= 2)
                    {
                        float distance = this.Distance(e.GetX(0), e.GetX(1), e.GetY(0), e.GetY(1));
                        this.mPreviousDistance = distance;
                        this.mIsScaling = true;
                    }
                    else
                    {
                        this.mPreviousMoveX = (int)e.GetX();
                        this.mPreviousMoveY = (int)e.GetY();
                    }
                }
                    break;

                case MotionEventActions.Move:
                {
                    if (touchCount >= 2 && this.mIsScaling)
                    {
                        float distance = this.Distance(e.GetX(0), e.GetX(1), e.GetY(0), e.GetY(1));
                        float scale = (distance - this.mPreviousDistance) / this.DispDistance();
                        this.mPreviousDistance = distance;
                        scale += 1;
                        scale = scale * scale;
                        this.ZoomTo(scale, this.mWidth / 2, this.mHeight / 2);
                        this.Cutting();
                    }
                    else if (!this.mIsScaling)
                    {
                        int distanceX = this.mPreviousMoveX - (int)e.GetX();
                        int distanceY = this.mPreviousMoveY - (int)e.GetY();
                        this.mPreviousMoveX = (int)e.GetX();
                        this.mPreviousMoveY = (int)e.GetY();

                        this.mMatrix.PostTranslate(-distanceX, -distanceY);
                        this.Cutting();
                    }
                }
                    break;
                case MotionEventActions.Up:
                case MotionEventActions.Pointer1Up:
                case MotionEventActions.Pointer2Up:
                {
                    if (touchCount <= 1)
                    {
                        this.mIsScaling = false;
                    }
                }
                    break;
            }
            return true;
        }

        public override void SetImageBitmap(Bitmap bm)
        {
            base.SetImageBitmap(bm);
            this.Initialize();
        }

        public override void SetImageResource(int resId)
        {
            base.SetImageResource(resId);
            this.Initialize();
        }

        public void ZoomTo(float scale, int x, int y)
        {
            if (this.Scale * scale < this.mMinScale)
            {
                return;
            }

            if (scale >= 1 && this.Scale * scale > this.mMaxScale)
            {
                return;
            }

            this.mMatrix.PostScale(scale, scale);
            //move to center
            this.mMatrix.PostTranslate(
                -(this.mWidth * scale - this.mWidth) / 2,
                -(this.mHeight * scale - this.mHeight) / 2);

            //move x and y distance
            this.mMatrix.PostTranslate(-(x - (this.mWidth / 2)) * scale, 0);
            this.mMatrix.PostTranslate(0, -(y - (this.mHeight / 2)) * scale);
            this.ImageMatrix = this.mMatrix;
        }

        protected override bool SetFrame(int l, int t, int r, int b)
        {
            this.mWidth = r - l;
            this.mHeight = b - t;

            this.mMatrix.Reset();
            int rNorm = r - l;
            this.mScale = (float)rNorm / (float)this.mIntrinsicWidth;

            int paddingHeight = 0;
            int paddingWidth = 0;
            if (this.mScale * this.mIntrinsicHeight > this.mHeight)
            {
                this.mScale = (float)this.mHeight / (float)this.mIntrinsicHeight;
                this.mMatrix.PostScale(this.mScale, this.mScale);
                paddingWidth = (r - this.mWidth) / 2;
            }
            else
            {
                this.mMatrix.PostScale(this.mScale, this.mScale);
                paddingHeight = (b - this.mHeight) / 2;
            }

            this.mMatrix.PostTranslate(paddingWidth, paddingHeight);
            this.ImageMatrix = this.mMatrix;
            this.mMinScale = this.mScale;
            this.ZoomTo(this.mScale, this.mWidth / 2, this.mHeight / 2);
            this.Cutting();
            return base.SetFrame(l, t, r, b);
        }

        private float DispDistance()
        {
            return FloatMath.Sqrt(this.mWidth * this.mWidth + this.mHeight * this.mHeight);
        }

        private float Distance(float x0, float x1, float y0, float y1)
        {
            float x = x0 - x1;
            float y = y0 - y1;
            return FloatMath.Sqrt(x * x + y * y);
        }

        private float GetValue(Matrix matrix, int whichValue)
        {
            matrix.GetValues(this.mMatrixValues);
            return this.mMatrixValues[whichValue];
        }

        private void Initialize()
        {
            this.SetScaleType(ScaleType.Matrix);
            this.mMatrix = new Matrix();

            if (this.Drawable != null)
            {
                this.mIntrinsicWidth = this.Drawable.IntrinsicWidth;
                this.mIntrinsicHeight = this.Drawable.IntrinsicHeight;
                this.SetOnTouchListener(this);
            }

            this.mGestureDetector = new GestureDetector(this.mContext, new ScaleImageViewGestureDetector(this));
        }
    }

    public class ScaleImageViewGestureDetector : GestureDetector.SimpleOnGestureListener
    {
        private readonly MvxScaleImageView mScaleImageView;

        public ScaleImageViewGestureDetector(MvxScaleImageView imageView)
        {
            this.mScaleImageView = imageView;
        }

        public override bool OnDoubleTap(MotionEvent e)
        {
            this.mScaleImageView.MaxZoomTo((int)e.GetX(), (int)e.GetY());
            this.mScaleImageView.Cutting();
            return true;
        }

        public override bool OnDown(MotionEvent e)
        {
            return true;
        }
    }
}