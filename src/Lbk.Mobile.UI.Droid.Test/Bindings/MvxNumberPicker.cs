//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MvxNumberPicker.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Bindings
{
    using System;

    using Android.Content;
    using Android.Util;
    using Android.Widget;

    public class MvxNumberPicker : NumberPicker, NumberPicker.IOnValueChangeListener
    {
        public MvxNumberPicker(Context context)
            : base(context)
        {
        }

        public MvxNumberPicker(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        public event EventHandler ValueChanged;

        public int Value
        {
            get
            {
                return this.Value;
            }
        }

        public void OnValueChange(NumberPicker picker, int oldVal, int newVal)
        {
            var handler = this.ValueChanged;
            if (handler != null)
            {
                handler(this, null);
            }
        }
    }
}