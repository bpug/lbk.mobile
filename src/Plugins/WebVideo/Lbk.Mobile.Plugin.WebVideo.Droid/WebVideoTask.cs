//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WebVideoTask.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Plugin.WebVideo.Droid
{
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.Net;

    using Cirrious.CrossCore;
    using Cirrious.CrossCore.Droid;
    using Cirrious.CrossCore.Droid.Platform;

    public class WebVideoTask : MvxAndroidTask, IWebVideoTask
    {
        public void PlayYoutubeVideo(string videoId, string title)
        {
            string url = string.Format("vnd.youtube:{0}", videoId);

            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;

            //var test = IsAppInstalled2("com.google.android.youtube");

            var intent = new Intent(Intent.ActionView, Uri.Parse(url));
            if (!IsAppInstalled(intent))
            {
                //url = string.Format("http://www.youtube.com/watch?v={0}", videoId);
                //this.StartActivity(new Intent(Intent.ActionView, Uri.Parse(url)));

                this.PlayInWebBrowser(activity, videoId, title);
                return;
            }

            this.StartActivity(intent);
        }


        private void PlayInWebBrowser(Activity activity, string videoId, string title)
        {
            var intent = new Intent(activity, typeof(YoutubeView));
            intent.PutExtra("videoId", videoId);
            intent.PutExtra("title", title);
            this.StartActivity(intent);
        }

        private static bool IsAppInstalled(Intent intent)
        {
            var globals = Mvx.Resolve<IMvxAndroidGlobals>();

            var pm = globals.ApplicationContext.PackageManager;

            var list = pm.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);

            return list.Count != 0;
        }
    }
}