//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="PdfView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Plugin.WebVideo.Droid
{
    using System;

    using Android.App;
    using Android.Media;
    using Android.OS;
    using Android.Views;
    using Android.Webkit;
    using Android.Widget;

    using  Android.Media;

    using Uri = Android.Net.Uri;

    public class YoutubeView : Activity
    {
        private WebView webView;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            string videoId = this.Intent.GetStringExtra("videoId");
            string title = this.Intent.GetStringExtra("title");
            string googleUrl = string.Format("http://www.youtube.com/embed/{0}?autoplay=1&vq=small", videoId);

            this.Title = title;

            webView = new WebView(this.BaseContext)
            {
                LayoutParameters =
                    new LinearLayout.LayoutParams(
                        ViewGroup.LayoutParams.MatchParent,
                        ViewGroup.LayoutParams.MatchParent)
            };
            webView.Settings.JavaScriptEnabled = true;
            webView.Settings.SetPluginState(WebSettings.PluginState.On);
            webView.LoadUrl(googleUrl);
            webView.SetWebChromeClient(new WebChromeClient());

            //string playVideo = string.Format("<html><body><iframe class=\"youtube-player\" allowfullscreen  type=\"text/html\" width=\"640\" height=\"385\" src=\"http://www.youtube.com/embed/{0}\" frameborder=\"0\"></body></html>", videoId);

            //webView.LoadData(playVideo, "text/html", "utf-8");

            this.SetContentView(webView);
        }

        //protected override void OnCreate(Bundle savedInstanceState)
        //{
        //    base.OnCreate(savedInstanceState);

        //    string videoId = this.Intent.GetStringExtra("videoId");
        //    string title = this.Intent.GetStringExtra("title");

        //    this.Title = title;

        //    webView = new VideoView(this.BaseContext)
        //    {
        //        LayoutParameters =
        //            new LinearLayout.LayoutParams(
        //                ViewGroup.LayoutParams.MatchParent,
        //                ViewGroup.LayoutParams.MatchParent)
        //    };
        //    //webView.SetVideoURI(Uri.Parse("rtsp://r4---sn-4g57kuek.c.youtube.com/CiILENy73wIaGQnXSbEt6s5XTRMYDSANFEgGUgZ2aWRlb3MM/0/0/0/video.3gp"));
        //    webView.SetVideoURI(Uri.Parse("rtsp://v5.cache5.c.youtube.com/CiILENy73wIaGQmC00ZlwwIDOxMYDSANFEgGUgZ2aWRlb3MM/0/0/0/video.3gp"));

        //    var mc = new MediaController(this);
        //    webView.SetMediaController(mc); 
        //    mc.Show(0);
        //    webView.RequestFocus();
        //    webView.Start()
        //    this.SetContentView(webView);
        //}


    }
}