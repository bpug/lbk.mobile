using Android.Content;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;

namespace Lbk.Mobile.UI.Droid.Test
{
    using System.Collections.Generic;
    using System.Reflection;

    using Android.Webkit;
    using Android.Widget;

    using Cheesebaron.MvvmCross.Bindings.Droid;

    using Cirrious.MvvmCross.Binding.Bindings.Target.Construction;

    using DK.Ostebaronen.Droid.ViewPagerIndicator;

    using Lbk.Mobile.UI.Droid.Test.Bindings;

    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }


        protected override IList<Assembly> AndroidViewAssemblies
        {
            get
            {
                var assemblies = base.AndroidViewAssemblies;
                assemblies.Add(typeof(BindableViewPager).Assembly);
                assemblies.Add(typeof(CirclePageIndicator).Assembly);
                return assemblies;
            }
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);

            registry.RegisterCustomBindingFactory<ImageView>(
                "DrawableResource",
                imageView => new MvxImageViewDrawableTargetBinding(imageView));
            registry.RegisterCustomBindingFactory<WebView>("Url", webView => new WebViewLoadUrlTargetBinding(webView));
            registry.RegisterCustomBindingFactory<NumberPicker>(
                "Value",
                picker => new NumberPickerTargetBinding(picker));
            //registry.RegisterPropertyInfoBindingFactory((typeof(NumberPickerTargetBinding)), typeof(NumberPicker), "Value");

            //registry.RegisterCustomBindingFactory<WebView>("YoutubeUrl",
            //                                                webView => new WebViewLoadYoutubeThnTargetBinding(webView));
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.LbkApp();
        }
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}