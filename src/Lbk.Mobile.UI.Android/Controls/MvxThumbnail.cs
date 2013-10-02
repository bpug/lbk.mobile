//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MvxThumbnail.cs" company="ip-connect GmbH">
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
    using Android.Widget;

    using Cirrious.CrossCore;
    using Cirrious.CrossCore.Core;
    using Cirrious.CrossCore.Platform;
    using Cirrious.MvvmCross.Binding.Droid.ResourceHelpers;
    using Cirrious.MvvmCross.Plugins.File;

    using Lbk.Mobile.UI.Droid.Extensions;
    using Lbk.Mobile.UI.Droid.Tools;

    using Path = System.IO.Path;

    public class MvxThumbnailView : ImageView
    {
        protected readonly IMvxImageHelper<Bitmap> ImageHelper;

        private readonly int defaultImageResource;

        public MvxThumbnailView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            if (!Mvx.TryResolve(out this.ImageHelper))
            {
                MvxTrace.Error(
                    "No IMvxImageHelper registered - you must provide an image helper before you can use a MvxImageView");
            }
            else
            {
                this.ImageHelper.ImageChanged += this.ImageHelperOnImageChanged;
            }

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

            var defaultSrc = context.ObtainStyledAttributes(attrs, Resource.Styleable.MvxThumbnail);
            this.defaultImageResource = defaultSrc.GetResourceId(Resource.Styleable.MvxThumbnail_defaultImage, 0);
            //this.DefaultImageSrc = this.GetDefaultImagePath(defaultImageResource);
        }

        public MvxThumbnailView(Context context)
            : base(context)
        {
        }

        protected MvxThumbnailView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        //public string DefaultImageSrc
        //{
        //    get
        //    {
        //        return this.ImageHelper.DefaultImagePath;
        //    }
        //    set
        //    {
        //        this.ImageHelper.DefaultImagePath = value;
        //    }
        //}

        //public string ErrorImagePath
        //{
        //    get
        //    {
        //        return this.ImageHelper.ErrorImagePath;
        //    }
        //    set
        //    {
        //        this.ImageHelper.ErrorImagePath = value;
        //    }
        //}

        [Obsolete("Use ImageUrl instead")]
        public string HttpImageUrl
        {
            get
            {
                return this.ImageUrl;
            }
            set
            {
                this.ImageUrl = value;
            }
        }

        public string ImageUrl
        {
            get
            {
                if (this.ImageHelper == null)
                {
                    return null;
                }
                return this.ImageHelper.ImageUrl;
            }
            set
            {
                if (this.ImageHelper == null)
                {
                    return;
                }
                this.ImageHelper.ImageUrl = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.ImageHelper != null)
                {
                    this.ImageHelper.Dispose();
                }
            }

            base.Dispose(disposing);
        }


        private string GetDefaultImagePath(int resourceId)
        {
            var fileStore = Mvx.Resolve<IMvxFileStore>();

            string resourcePath = this.Resources.GetString(resourceId);

            string filename = Path.GetFileName(resourcePath);

            if (!fileStore.Exists(filename))
            {
                var bitmap = BitmapFactory.DecodeResource(this.Resources, resourceId);
                var array = bitmap.ToByteArray();
                fileStore.WriteFile(filename, array);
            }

            return filename;
        }

        private void ImageHelperOnImageChanged(object sender, MvxValueEventArgs<Bitmap> mvxValueEventArgs)
        {
            if (mvxValueEventArgs.Value != null)
            {
                using (var bmp = mvxValueEventArgs.Value.ScaleCenterCrop(this.MeasuredWidth, this.MeasuredHeight))
                {
                    this.SetImageBitmap(bmp);
                }
            }
            else
            {
                if (this.defaultImageResource != 0)
                {
                    this.SetImageDrawable(this.Context.Resources.GetDrawable(this.defaultImageResource));
                }
            }
        }
    }
}