//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DocumentViewerTask.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Plugin.DocumentViewer.Droid
{
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.Net;

    using Cirrious.CrossCore;
    using Cirrious.CrossCore.Droid;
    using Cirrious.CrossCore.Droid.Platform;

    using Java.IO;

    public class DocumentViewerTask : MvxAndroidTask, IDocumentViewerTask
    {
        public void ShowPdf(string localPath, string url, bool onTop = false)
        {

            var test  = Mvx.Resolve<IMvxAndroidGlobals>().ApplicationContext;
            var path = test.FilesDir.Path;
            var path2 = test.GetExternalFilesDir(null);
            
            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            var uri = Uri.FromFile(new File(localPath));

            var intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(uri, "application/pdf");
            intent.SetFlags(ActivityFlags.ClearTop);

            if (!IsAppInstalled(intent))
            {
                this.OpenPdfThroughGoogleDrive(activity, url, onTop);
                return;
            }

            if (onTop)
            {
                this.SetOnTop(activity, intent);
            }

            this.StartActivity(intent);
        }

        private static bool IsAppInstalled(Intent intent)
        {
            var globals = Mvx.Resolve<IMvxAndroidGlobals>();

            var pm = globals.ApplicationContext.PackageManager;

            var list = pm.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);

            return list.Count != 0;
        }

        private void OpenPdfThroughGoogleDrive(Activity activity, string pdfUrl, bool onTop)
        {
            var intent = new Intent(activity, typeof(PdfView));
            intent.PutExtra("url", pdfUrl);
            if (onTop)
            {
                this.SetOnTop(activity, intent);
                //activity.OverridePendingTransition(0, 0);
                //intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.NoHistory | ActivityFlags.NoAnimation);
                //activity.Finish();
                //activity.OverridePendingTransition(0, 0);
            }

            this.StartActivity(intent);
        }

        private void SetOnTop(Activity activity, Intent intent)
        {
            activity.OverridePendingTransition(0, 0);
            intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.NoHistory | ActivityFlags.NoAnimation);
            activity.Finish();
            activity.OverridePendingTransition(0, 0);
        }
    }
}