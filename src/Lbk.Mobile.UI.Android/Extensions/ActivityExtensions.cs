//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ActivityExtensions.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Extensions
{
    using Android.App;
    using Android.Graphics;
    using Java.Lang;

    public static class ActivityExtensions
    {
        public static int GetScreenWidth(this Activity activity)
        {
            int columnWidth = 0;

            var wm = activity.WindowManager;

            var display = wm.DefaultDisplay;

            var point = new Point();
            try
            {
                display.GetSize(point);
            }
            catch (NoSuchMethodError ignore)
            {
                // Older device
                point.X = display.Width;
                point.Y = display.Height;
            }
            columnWidth = point.X;
            return columnWidth;
        }

        public static void SetBackground(this Activity activity)
        {
            var drawable = activity.Resources.GetDrawable(Resource.Drawable.background);
            drawable.SetDither(true);
            activity.Window.SetBackgroundDrawable(drawable);
            activity.Window.DecorView.Background.SetDither(true);
        }
    }
}