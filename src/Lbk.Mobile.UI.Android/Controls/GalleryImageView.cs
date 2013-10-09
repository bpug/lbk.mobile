//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GalleryImageView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Controls
{
    using System;

    using Android.Content;
    using Android.Graphics;
    using Android.Graphics.Drawables;
    using Android.Runtime;
    using Android.Util;
    using Android.Views.Animations;
    using Android.Widget;

    using Cirrious.MvvmCross.Binding.Droid.ResourceHelpers;

    using Lbk.Mobile.UI.Droid.Extensions;

    using UrlImageViewHelper;

    public class GalleryImageView : ImageView
    {
        private readonly int defaultImageResource;

        private readonly bool isThumbnail;

        private string imageUrl;

        public GalleryImageView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            var typedArray = context.ObtainStyledAttributes(
                attrs,
                MvxAndroidBindingResource.Instance.ImageViewStylableGroupId);

            int numStyles = typedArray.IndexCount;
            for (int i = 0; i < numStyles; ++i)
            {
                int attributeId = typedArray.GetIndex(i);
                if (attributeId == MvxAndroidBindingResource.Instance.SourceBindId)
                {
                    this.ImageUrl = typedArray.GetString(attributeId);
                }
            }
            typedArray.Recycle();

            var defaultSrc = context.ObtainStyledAttributes(attrs, Resource.Styleable.DefaultImage);
            this.defaultImageResource = defaultSrc.GetResourceId(Resource.Styleable.DefaultImage_defaultImage, 0);
            //this.DefaultImageSrc = this.GetDefaultImagePath(defaultImageResource);

            var isThumbnailSrc = context.ObtainStyledAttributes(attrs, Resource.Styleable.IsThumbnail);
            this.isThumbnail = isThumbnailSrc.GetBoolean(Resource.Styleable.IsThumbnail_thumbnail, false);
        }

        public GalleryImageView(Context context)
            : base(context)
        {
        }

        protected GalleryImageView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public string ImageUrl
        {
            get
            {
                return this.imageUrl;
            }
            set
            {
                this.imageUrl = value;
                this.RequestImage(this.imageUrl);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }

        private void RequestImage(string url)
        {
            UrlImageViewHelper.SetUrlDrawable(
                this,
                url,
                this.Context.Resources.GetDrawable(this.defaultImageResource),
                new MyCallback(),
                this.ScaleImage);
        }

        private byte[] ScaleImage(byte[] imageData)
        {
            using (var bitmap = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length))
            {
                using (var scaled = bitmap.Scale(this.MeasuredWidth, this.MeasuredHeight))
                {
                    return scaled.ToByteArray();
                }
            }
        }

        private class MyCallback : IUrlImageViewCallback
        {
            public void OnLoaded(ImageView imageView, Drawable loadedDrawable, string url, bool loadedFromCache)
            {
                if (!loadedFromCache)
                {
                    var scale = new ScaleAnimation(0, 1, 0, 1, Dimension.RelativeToSelf, .5f, Dimension.RelativeToSelf, .5f)
                    {
                        Duration = 300,
                        Interpolator = new LinearInterpolator()
                    };
                    imageView.StartAnimation(scale); 

                    //var anim = AnimationUtils.LoadAnimation(imageView.Context, Resource.Animation.fade_out);
                    //imageView.StartAnimation(anim);
                }
            }
        }
    }
}