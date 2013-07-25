using System;
using System.Collections.Generic;
using Android.Content;
using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.Binding.Droid;

namespace Lbk.Mobile.UI.Droid
{
    using Lbk.Mobile.Portable.Core;
    using Lbk.Mobile.Portable.Core.Converters;

    public class Setup
        : MvxBaseAndroidBindingSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override MvxApplication CreateApp()
        {
            return new App();
        }

        protected override IEnumerable<Type> ValueConverterHolders
        {
            get { return new[] { typeof(Converters) }; }
        }

        protected override void InitializeLastChance()
        {
            var errorDisplayer = new ErrorDisplayer(base.ApplicationContext);
            base.InitializeLastChance();
        }
    }
}