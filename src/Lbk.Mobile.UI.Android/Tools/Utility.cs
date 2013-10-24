//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Utility.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Tools
{
    using Android.Graphics;
    using Android.Util;

    using Cirrious.CrossCore;
    using Cirrious.CrossCore.Droid;

    public class Utility
    {
        public static int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
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

        public static int ConvertPixelsToDp(float pixelValue)
        {
            var displayMetrics = Mvx.Resolve<IMvxAndroidGlobals>().ApplicationContext.Resources.DisplayMetrics;
            int dp = (int)((pixelValue) / displayMetrics.Density);
            return dp;
        }

        public int DpToPx(int dp)
        {
            var displayMetrics = Mvx.Resolve<IMvxAndroidGlobals>().ApplicationContext.Resources.DisplayMetrics;
            int px = (int)(dp * (displayMetrics.Xdpi / (float)DisplayMetricsDensity.Default));
            return px;
        }

        public int PxToDp(int px)
        {
            var displayMetrics = Mvx.Resolve<IMvxAndroidGlobals>().ApplicationContext.Resources.DisplayMetrics;
            int dp = (int)(px / (displayMetrics.Xdpi / (float)DisplayMetricsDensity.Default));
            return dp;
        }

        //public static Task<Bitmap> DecodeSampledBitmap(Bitmap bitmap, int reqWidth, int reqHeight)
        //{
        //    //return Bitmap.CreateScaledBitmap(bitmap, reqWidth, reqHeight, true);

        //    // First decode with inJustDecodeBounds=true to check dimensions
        //    var options = new BitmapFactory.Options
        //    {
        //        InJustDecodeBounds = true
        //    };

        //    var bitmapData = bitmap.ToByteArray();

        //    BitmapFactory.DecodeByteArrayAsync(bitmapData, 0, bitmapData.Length, options);

        //    // Calculate inSampleSize
        //    options.InSampleSize = CalculateInSampleSize(options, reqWidth, reqHeight);

        //    // Decode bitmap with inSampleSize set
        //    options.InJustDecodeBounds = false;
        //    return BitmapFactory.DecodeByteArrayAsync(bitmapData, 0, bitmapData.Length, options);
        //}

        //public static Bitmap ResizedBitmap(Bitmap bmp, int newHeight, int newWidth)
        //{
        //    if (bmp == null)
        //    {
        //        return null;
        //    }

        //    int width = bmp.Width;
        //    int height = bmp.Height;
        //    float scaleWidth = ((float)newWidth) / width;
        //    float scaleHeight = ((float)newHeight) / height;
        //    // CREATE A MATRIX FOR THE MANIPULATION
        //    var matrix = new Matrix();
        //    // RESIZE THE BIT MAP
        //    matrix.PostScale(scaleWidth, scaleHeight);

        //    // "RECREATE" THE NEW BITMAP
        //    var newBitmap = Bitmap.CreateBitmap(bmp, 0, 0, width, height, matrix, false);
        //    return newBitmap;
        //}
    }
}