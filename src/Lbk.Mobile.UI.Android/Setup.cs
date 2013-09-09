//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Setup.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Android
{
    using Cirrious.CrossCore.Platform;
    using Cirrious.MvvmCross.Binding.Bindings.Target.Construction;
    using Cirrious.MvvmCross.Dialog.Droid;
    using Cirrious.MvvmCross.Droid.Platform;
    using Cirrious.MvvmCross.ViewModels;

    using global::Android.Content;
    using global::Android.Widget;

    using Lbk.Mobile.Core;
    using Lbk.Mobile.UI.Android.Bindings;

    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
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

            registry.RegisterCustomBindingFactory<ImageView>("DrawableResource",
                                                            imageView => new MvxImageViewDrawableTargetBinding(imageView));
        }

        protected override void InitializeLastChance()
        {
            var errorDisplayer = new ErrorDisplayer(base.ApplicationContext);
            base.InitializeLastChance();
        }
    }
}