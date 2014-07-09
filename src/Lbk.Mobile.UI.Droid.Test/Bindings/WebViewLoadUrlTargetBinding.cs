//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WebViewLoadUrlTargetBinding.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Test.Bindings
{
    using Android.Webkit;

    public class WebViewLoadUrlTargetBinding : BaseWebViewTargetBinding
    {
        public WebViewLoadUrlTargetBinding(object target)
            : base(target)
        {
        }

        protected override void SetValueImpl(object target, object value)
        {
            var webView = target  as WebView;
            if (webView == null)
            {
                return;
            }
            if (string.IsNullOrEmpty((string)value))
            {
                return;
            }

            //webView.Settings.LoadWithOverviewMode = true;
            //webView.Settings.UseWideViewPort = true;
            webView.LoadUrl((string)value);
        }
    }
}