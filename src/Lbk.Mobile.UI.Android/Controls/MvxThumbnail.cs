//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MvxThumbnail.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Controls
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    using Android.Content;
    using Android.Graphics;
    using Android.Runtime;
    using Android.Util;
    using Android.Widget;

    using Cirrious.CrossCore;
    using Cirrious.CrossCore.Core;
    using Cirrious.CrossCore.Exceptions;
    using Cirrious.CrossCore.Platform;
    using Cirrious.MvvmCross.Binding.Droid.ResourceHelpers;
    using Cirrious.MvvmCross.Plugins.File;

    using Path = System.IO.Path;

    public class MvxThumbnailView : ImageView
    {
        protected readonly IMvxImageHelper<Bitmap> ImageHelper;

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
            int defaultImgResourceId = defaultSrc.GetResourceId(Resource.Styleable.MvxThumbnail_defaultImage, 0);
            this.DefaultImageSrc = this.GetDefaultImagePath(defaultImgResourceId);
        }

        public MvxThumbnailView(Context context)
            : base(context)
        {
        }

        protected MvxThumbnailView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        //public string DefaultImagePath
        //{
        //    get { return ImageHelper.DefaultImagePath; }
        //    set { ImageHelper.DefaultImagePath = value; }
        //}

        //private string defaultImageSrc;

        public string DefaultImageSrc
        {
            get
            {
                return this.ImageHelper.DefaultImagePath;
            }
            set
            {
                this.ImageHelper.DefaultImagePath = value;
            }
        }

        public string ErrorImagePath
        {
            get
            {
                return this.ImageHelper.ErrorImagePath;
            }
            set
            {
                this.ImageHelper.ErrorImagePath = value;
            }
        }

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

        private static int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
        {
            // Raw height and width of image
            float height = (float)options.OutHeight;
            float width = (float)options.OutWidth;
            double inSampleSize = 1D;

            if (height > reqHeight || width > reqWidth)
            {
                inSampleSize = width > height ? height / reqHeight : width / reqWidth;
            }

            return (int)inSampleSize;
        }

        private static byte[] ConvertBitmapToByteArray(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                return null;
            }

            byte[] bitmapData = null;
            try
            {
                using (var stream = new MemoryStream())
                {
                    bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                    bitmapData = stream.ToArray();
                }
            }
            catch (Exception e)
            {
                Trace.Fail("Failed to convert  bitmap tp array: {0}", e.ToLongString());
            }
            return bitmapData;
        }

        private static Task<Bitmap> DecodeSampledBitmap(Bitmap bitmap, int reqWidth, int reqHeight)
        {
            //return Bitmap.CreateScaledBitmap(bitmap, reqWidth, reqHeight, true);

            // First decode with inJustDecodeBounds=true to check dimensions
            var options = new BitmapFactory.Options
            {
                InJustDecodeBounds = true
            };

            var bitmapData = ConvertBitmapToByteArray(bitmap);

            BitmapFactory.DecodeByteArrayAsync(bitmapData, 0, bitmapData.Length, options);

            // Calculate inSampleSize
            options.InSampleSize = CalculateInSampleSize(options, reqWidth, reqHeight);

            // Decode bitmap with inSampleSize set
            options.InJustDecodeBounds = false;
            return BitmapFactory.DecodeByteArrayAsync(bitmapData, 0, bitmapData.Length, options);
        }

        private string GetDefaultImagePath(int resourceId)
        {
            var fileStore = Mvx.Resolve<IMvxFileStore>();
            var res = this.Context.Resources;

            var resourcePath = this.Resources.GetString(resourceId);

            var filename = Path.GetFileName(resourcePath);

            if (!fileStore.Exists(filename))
            {
                var bitmap = BitmapFactory.DecodeResource(this.Resources, resourceId);
                var array = ConvertBitmapToByteArray(bitmap);
                fileStore.WriteFile(filename, array);
            }

            return filename;
        }

        private void ImageHelperOnImageChanged(object sender, MvxValueEventArgs<Bitmap> mvxValueEventArgs)
        {
            //using (var bitmap = await DecodeSampledBitmap(mvxValueEventArgs.Value, 80, 80))
            //{
            //    this.SetImageBitmap(bitmap);
            //}

            using (var bmp = this.ResizedBitmap(mvxValueEventArgs.Value, 80, 80))
            {
                this.SetImageBitmap(bmp);
            }
        }

        private Bitmap ResizedBitmap(Bitmap bmp, int newHeight, int newWidth)
        {
            int width = bmp.Width;
            int height = bmp.Height;
            float scaleWidth = ((float)newWidth) / width;
            float scaleHeight = ((float)newHeight) / height;
            // CREATE A MATRIX FOR THE MANIPULATION
            var matrix = new Matrix();
            // RESIZE THE BIT MAP
            matrix.PostScale(scaleWidth, scaleHeight);

            // "RECREATE" THE NEW BITMAP
            var newBitmap = Bitmap.CreateBitmap(bmp, 0, 0, width, height, matrix, false);
            return newBitmap;
        }
    }
}