//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WebViewLoadYoutubeThnTargetBinding.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Android.Bindings
{
    using global::Android.Webkit;

    public class WebViewLoadYoutubeThnTargetBinding : BaseWebViewTargetBinding
    {
        protected const int PicSize =100;

        public WebViewLoadYoutubeThnTargetBinding(object target)
            : base(target)
        {
        }

        public override void SetValue(object value)
        {
            var webView = this.WebView;
            if (webView == null)
            {
                return;
            }
            if (string.IsNullOrEmpty((string)value))
            {
                return;
            }

            var url = (string)value;

            //var embedHtml = "<html><head> " +
            //   "</head>" +
            //       "<body >" +
            //       "<div><strong>Boris</strong></div>"+
            //   "</body>" +
            //   "</html>";


            var load = "<html><head></head><body><iframe class='youtube-player' type='text/html' style='border: 0; width: 100%; height:  100%; padding:0px; margin:0px' id='ytplayer' src='http://www.youtube.com/embed/TVfO6i2xSdc?fs=0' frameborder='0' ></iframe></body></html>";

            string test = "<iframe class=\"youtube-player\" style=\"border: 0; width: 100%; height: 95%; padding:0px; margin:0px\" id=\"ytplayer\" type=\"text/html\" src=\"http://www.youtube.com/embed/"
            + "J2fB5XWj6IE"
            + "?fs=0\" frameborder=\"0\">\n"
            + "</iframe>\n";

            var embedHtml = "<html><head> " +
                    "<meta name = 'viewport' content = 'initial-scale = 1.0, user-scalable = no, width = {1}'/></head>" +
                    "<body style=\"background:#fffff;margin:0px;\" >" +
                    "<object width='{1}px' height='{1}'>" +
                    "<param name=\"movie\" value='{0}'></param>" +
                    "<param name=\"wmode\" value=\"transparent\"></param>" +
                    "<embed  id ='yt' src='{0}' type='application/x-shockwave-flash' " +
                    "width='{1}' height='{1}' wmode=\"transparent\"></embed>  " +
                    "</object>" +
                "</body>" +
                "</html>";
            string html = string.Format(embedHtml, "http://www.youtube.com/v/TVfO6i2xSdc", PicSize);

            webView.SetWebChromeClient(new WebChromeClient());
            //webView.Settings.PluginsEnabled = true;
            //webView.Settings.SetPluginState(WebSettings.PluginState.On);
            //webView.Settings.JavaScriptEnabled = true;
            webView.LoadData(html, "text/html", "UTF-8");
        }
    }
}