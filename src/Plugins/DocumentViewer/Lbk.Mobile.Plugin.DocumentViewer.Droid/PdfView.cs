//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="PdfView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Plugin.DocumentViewer.Droid
{
    using Android.App;
    using Android.OS;
    using Android.Views;
    using Android.Webkit;
    using Android.Widget;

    public class PdfView : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            string pdfUrl = this.Intent.GetStringExtra("url");
            var webView = new WebView(this.BaseContext)
            {
                LayoutParameters =
                    new LinearLayout.LayoutParams(
                        ViewGroup.LayoutParams.MatchParent,
                        ViewGroup.LayoutParams.MatchParent)
            };
            webView.Settings.JavaScriptEnabled = true;
            webView.Settings.SetPluginState(WebSettings.PluginState.On);
            webView.SetWebViewClient(new Callback());

            //string googleUrl = string.Format("http://docs.google.com/gview?embedded=true&url={0}", pdfUrl);
            string googleUrl = string.Format("http://drive.google.com/viewer?url={0}", pdfUrl);

            webView.LoadUrl(googleUrl);

            SetContentView(webView);
        }

        private class Callback : WebViewClient
        {
            public override bool ShouldOverrideUrlLoading(WebView view, string url)
            {
                return false;
            }
        }
    }
}