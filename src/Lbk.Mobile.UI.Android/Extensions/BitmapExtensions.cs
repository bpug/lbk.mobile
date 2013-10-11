//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BitmapExtensions.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Extensions
{
    using System;
    using System.Diagnostics;
    using System.IO;

    using Android.Graphics;

    using Cirrious.CrossCore.Exceptions;

    public static class BitmapExtensions
    {
        public static Bitmap RoundedCorner(this Bitmap bitmap, int pixels)
        {
            var output = Bitmap.CreateBitmap(bitmap.Width, bitmap.Height, Bitmap.Config.Argb8888);
            var canvas = new Canvas(output);

            uint color = 0xff424242;
            var paint = new Paint();
            var rect = new Rect(0, 0, bitmap.Width, bitmap.Height);
            var rectF = new RectF(rect);
            float roundPx = pixels;

            paint.AntiAlias = true;
            canvas.DrawARGB(0, 0, 0, 0);
            paint.Color = new Color((int)color);
            canvas.DrawRoundRect(rectF, roundPx, roundPx, paint);

            paint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.SrcIn));
            canvas.DrawBitmap(bitmap, rect, rect, paint);

            return output;
        }

        public static Bitmap Scale(this Bitmap source, int newHeight, int newWidth)
        {
            if (source == null)
            {
                return null;
            }

            int sourceWidth = source.Width;
            int sourceHeight = source.Height;
            float xScale = ((float)newWidth) / sourceWidth;
            float yScale = ((float)newHeight) / sourceHeight;
            float scale = Math.Max(xScale, yScale);

            // Now get the size of the source bitmap when scaled
            float scaledWidth = scale * sourceWidth;
            float scaledHeight = scale * sourceHeight;

            // create a matrix for the manipulation
            var matrix = new Matrix();
            // resize the bit map
            matrix.PostScale(scale, scale);

            // recreate the new Bitmap
            Bitmap resizedBitmap;
            try
            {
                resizedBitmap = Bitmap.CreateScaledBitmap(source, (int)scaledWidth, (int)scaledHeight, false);
            }
            catch (Exception exception)
            {
                return null;
            }

            //Debug.WriteLineIf(true, "NewWidth: " + newWidth + " NewWidthToDp: " + Utility.ConvertPixelsToDp(newWidth) + " ResizedWith: " + resizedBitmap.Width + " ResizedWithToDp: " + Utility.ConvertPixelsToDp(resizedBitmap.Width));
            //Debug.WriteLineIf(true, "NewHeight: " + newHeight + " NewHeightToDp: " + Utility.ConvertPixelsToDp(newHeight) + " ResizedHeight: " + resizedBitmap.Height + " ResizedHeightToDp: " + Utility.ConvertPixelsToDp(resizedBitmap.Height));

            //Bitmap resizedBitmap2 = Bitmap.CreateBitmap(source, 0, 0, sourceWidth, sourceHeight, matrix, false);

            return resizedBitmap;
        }

        public static Bitmap ScaleCenterCrop(this Bitmap source, int newHeight, int newWidth)
        {
            if (source == null)
            {
                return null;
            }

            int sourceWidth = source.Width;
            int sourceHeight = source.Height;

            // Compute the scaling factors to fit the new height and width, respectively.
            // To cover the final image, the final scaling will be the bigger 
            // of these two.
            float xScale = (float)newWidth / sourceWidth;
            float yScale = (float)newHeight / sourceHeight;
            float scale = Math.Max(xScale, yScale);

            // Now get the size of the source bitmap when scaled
            float scaledWidth = scale * sourceWidth;
            float scaledHeight = scale * sourceHeight;

            // Let's find out the upper left coordinates if the scaled bitmap
            // should be centered in the new size give by the parameters
            float left = (newWidth - scaledWidth) / 2;
            float top = (newHeight - scaledHeight) / 2;

            // The target rectangle for the new, scaled version of the source bitmap will now
            // be
            var targetRect = new RectF(left, top, left + scaledWidth, top + scaledHeight);

            // Finally, we create a new bitmap of the specified size and draw our new,
            // scaled bitmap onto it.
            var dest = Bitmap.CreateBitmap(newWidth, newHeight, source.GetConfig());
            var canvas = new Canvas(dest);
            canvas.DrawBitmap(source, null, targetRect, null);

            return dest;
        }

        public static byte[] ToByteArray(this Bitmap bitmap)
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

        //public static Bitmap ScaleCenterCrop(this Bitmap bitmap, int reqWidth, int reqHeight)
        //{
        //   // First decode with inJustDecodeBounds=true to check dimensions
        //    var options = new BitmapFactory.Options
        //    {
        //        InJustDecodeBounds = true
        //    };

        //    var bitmapData = Utility.ConvertBitmapToByteArray(bitmap);

        //    BitmapFactory.DecodeByteArrayAsync(bitmapData, 0, bitmapData.Length, options);

        //    //// Calculate inSampleSize
        //    //options.InSampleSize = CalculateInSampleSize(options, reqWidth, reqHeight);

        //    int REQUIRED_WIDTH = reqWidth;
        //    int REQUIRED_HIGHT = reqHeight;
        //    int scale = 1;
        //    while (options.OutWidth / scale / 2 >= REQUIRED_WIDTH
        //            && options.OutHeight / scale / 2 >= REQUIRED_HIGHT)
        //        scale *= 2;

        //    // Decode bitmap with inSampleSize set
        //    var options2 = new BitmapFactory.Options
        //    {
        //        InSampleSize = scale,
        //        InPurgeable = true
        //    };
        //    return BitmapFactory.DecodeByteArray(bitmapData, 0, bitmapData.Length, options2);
        //}
    }
}