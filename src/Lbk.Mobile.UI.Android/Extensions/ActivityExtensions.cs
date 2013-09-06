//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ActivityExtensions.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Android.Extensions
{
    using global::Android.App;

    public static class ActivityExtensions
    {
        public static void SetBackground(this Activity activity)
        {
            var drawable = activity.Resources.GetDrawable(Resource.Drawable.background);
            drawable.SetDither(true);
            activity.Window.SetBackgroundDrawable(drawable);
            activity.Window.DecorView.Background.SetDither(true);
        }
    }
}