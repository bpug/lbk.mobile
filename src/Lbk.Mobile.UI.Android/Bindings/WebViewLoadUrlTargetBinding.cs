//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WebViewLoadUrlTargetBinding.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Android.Bindings
{
    public class WebViewLoadUrlTargetBinding : BaseWebViewTargetBinding
    {
        public WebViewLoadUrlTargetBinding(object target)
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

            webView.Settings.LoadWithOverviewMode = true;
            webView.Settings.UseWideViewPort = true;
            webView.LoadUrl((string)value);
        }
    }
}