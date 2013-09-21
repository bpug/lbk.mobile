//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BitmapExtensions.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Extensions
{
    using System;

    using Android.Graphics;

    public static class BitmapExtensions
    {
        public static Bitmap ScaleCenterCrop(this Bitmap source, int newHeight, int newWidth)
        {
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

        //// **** From  LBK Toch ****
        //public static ImageView Thumbnail(this ImageView source, Bitmap bitmap, SizeF newSize)
        //{
        //    int sourceWidth = source.MeasuredWidth;
        //    int sourceHeight = source.MeasuredHeight;
        //    int offsetY = 0;
        //    int offsetX = 0;
        //    Size croppedSize;

        //    RectangleF rect = new RectangleF(0, 0, newSize.Width, newSize.Height);

        //    // check the size of the image, we want to make it
        //    // a square with sides the size of the smallest dimension
        //    if (sourceWidth > sourceHeight)
        //    {
        //        offsetX = (sourceHeight - sourceWidth) / 2;
        //        croppedSize = new Size(sourceHeight, sourceHeight);
        //    }
        //    else
        //    {
        //        offsetY = (sourceWidth - sourceHeight) / 2;
        //        croppedSize = new Size(sourceWidth, sourceWidth);
        //    }

        //    // Crop the image before resize
        //    //RectangleF clippedRect = new RectangleF(offsetX * -1, offsetY * -1, croppedSize.Width, croppedSize.Height);
        //    //ImageView cropped = ImageView.FromImage(source.CGImage.WithImageInRect(clippedRect));

        //    Bitmap croppedBmp = Bitmap.CreateBitmap(bitmap, offsetX * -1, offsetY * -1, croppedSize.Width, croppedSize.Height);
        //    // Done cropping

        //    UIGraphics.BeginImageContext(newSize);
        //    var ctx = UIGraphics.GetCurrentContext();

        //    ctx.DrawImage(rect, cropped.CGImage);

        //    var ret = UIGraphics.GetImageFromCurrentImageContext();

        //    //Flip
        //    ctx.DrawImage(rect, ret.CGImage);
        //    ctx.RotateCTM(M_PI);
        //    ret = UIGraphics.GetImageFromCurrentImageContext();

        //    UIGraphics.EndImageContext();

        //    //ret = ret.FlipImage (newSize);
        //    return ret;

        //}
    }
}