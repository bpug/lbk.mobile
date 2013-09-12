//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Setup.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Android
{
    using System.Collections.Generic;
    using System.Reflection;

    using Cirrious.CrossCore.Platform;
    using Cirrious.MvvmCross.Binding.Bindings.Target.Construction;
    using Cirrious.MvvmCross.Droid.Platform;
    using Cirrious.MvvmCross.ViewModels;

    using global::Android.Content;
    using global::Android.Webkit;
    using global::Android.Widget;
    
    using Lbk.Mobile.UI.Android.Bindings;

    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.LbkApp();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);

            registry.RegisterCustomBindingFactory<ImageView>("DrawableResource",
                                                            imageView => new MvxImageViewDrawableTargetBinding(imageView));
            registry.RegisterCustomBindingFactory<WebView>("Url",
                                                            webView => new WebViewLoadUrlTargetBinding(webView));

            //registry.RegisterCustomBindingFactory<WebView>("YoutubeUrl",
            //                                                webView => new WebViewLoadYoutubeThnTargetBinding(webView));

            
        }

        protected override void InitializeLastChance()
        {
            var errorDisplayer = new ErrorDisplayer(base.ApplicationContext);
            base.InitializeLastChance();
        }

        protected override IList<Assembly> AndroidViewAssemblies
        {
            get
            {
                var assemblies = base.AndroidViewAssemblies;
                assemblies.Add(typeof(Cheesebaron.MvvmCross.Bindings.Droid.BindableViewPager).Assembly);
                return assemblies;
            }
        }
    }
}