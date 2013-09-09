//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MvxImageViewDrawableTargetBinding.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Android.Bindings
{
    using System;
    using System.IO;

    using Cirrious.CrossCore.Platform;
    using Cirrious.MvvmCross.Binding;
    using Cirrious.MvvmCross.Binding.Droid.Target;

    using global::Android.Widget;

    public class MvxImageViewDrawableTargetBinding : MvxBaseStreamImageViewTargetBinding
    {
        public MvxImageViewDrawableTargetBinding(ImageView imageView)
            : base(imageView)
        {
        }

        public override Type TargetType
        {
            get
            {
                return typeof(string);
            }
        }

        protected override Stream GetStream(object value)
        {
            if (value == null)
            {
                MvxBindingTrace.Trace(MvxTraceLevel.Warning, "Null value passed to ImageView binding");
                return null;
            }

            var stringValue = value as string;

            if (string.IsNullOrWhiteSpace(stringValue))
            {
                MvxBindingTrace.Trace(MvxTraceLevel.Warning, "Empty value passed to ImageView binding");
                return null;
            }

            int resourceId;

            try
            {
                resourceId = (int)typeof(Resource.Drawable).GetField(stringValue).GetValue(null);
            }
            catch (Exception)
            {

                MvxBindingTrace.Trace(MvxTraceLevel.Warning, "Could not find a drawable with id '{0}'", value);
                return null;
            }
            

            var resources = this.AndroidGlobals.ApplicationContext.Resources;
            var stream = resources.OpenRawResource(resourceId);

            if (stream == null)
            {
                MvxBindingTrace.Trace(MvxTraceLevel.Warning, "Could not find a drawable with id '{0}'", value);
                return null;
            }

            return stream;
        }
    }
}