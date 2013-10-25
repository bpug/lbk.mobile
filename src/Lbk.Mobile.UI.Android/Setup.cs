//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Setup.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid
{
    using System.Collections.Generic;
    using System.Reflection;

    using Android.Content;
    using Android.Webkit;
    using Android.Widget;

    using Cheesebaron.MvvmCross.Bindings.Droid;

    using Cirrious.CrossCore.Platform;
    using Cirrious.MvvmCross.Binding.Bindings.Target.Construction;
    using Cirrious.MvvmCross.Droid.Platform;
    using Cirrious.MvvmCross.ViewModels;

    using DK.Ostebaronen.Droid.ViewPagerIndicator;

    using Lbk.Mobile.Core;
    using Lbk.Mobile.UI.Droid.Bindings;

    using PullToRefresharp.Android.Views;

    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override IList<Assembly> AndroidViewAssemblies
        {
            get
            {
                var assemblies = base.AndroidViewAssemblies;
                assemblies.Add(typeof(BindableViewPager).Assembly);
                assemblies.Add(typeof(CirclePageIndicator).Assembly);
                assemblies.Add(typeof(ViewWrapper).Assembly);
                return assemblies;
            }
        }

        protected override IMvxApplication CreateApp()
        {
            return new LbkApp();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);

            registry.RegisterCustomBindingFactory<ImageView>(
                "DrawableResource",
                imageView => new MvxImageViewDrawableTargetBinding(imageView));
            registry.RegisterCustomBindingFactory<WebView>("Url", webView => new WebViewLoadUrlTargetBinding(webView));
            registry.RegisterCustomBindingFactory<NumberPicker>(
                "Value", picker => new NumberPickerTargetBinding(picker));
            //registry.RegisterPropertyInfoBindingFactory((typeof(NumberPickerTargetBinding)), typeof(NumberPicker), "Value");

            //registry.RegisterCustomBindingFactory<WebView>("YoutubeUrl",
            //                                                webView => new WebViewLoadYoutubeThnTargetBinding(webView));
        }

        //protected override void InitializeLastChance()
        //{
        //    var errorDisplayer = new ErrorDisplayer(base.ApplicationContext);
        //    base.InitializeLastChance();
        //}
    }
}