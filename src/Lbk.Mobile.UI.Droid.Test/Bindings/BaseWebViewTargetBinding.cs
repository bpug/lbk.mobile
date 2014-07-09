//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WebViewUrlTargetBinding.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Test.Bindings
{
    using System;

    using Android.Webkit;

    using Cirrious.MvvmCross.Binding;
    using Cirrious.MvvmCross.Binding.Droid.Target;

    public abstract class BaseWebViewTargetBinding : MvxAndroidTargetBinding
    {
        protected  BaseWebViewTargetBinding(object target)
            : base(target)
        {
        }

        public override MvxBindingMode DefaultMode
        {
            get
            {
                return MvxBindingMode.OneWay;
            }
        }

        public override Type TargetType
        {
            get
            {
                return typeof(string);
            }
        }

        protected WebView WebView
        {
            get
            {
                return (WebView)this.Target;
            }
        }
    }
}