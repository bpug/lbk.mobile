using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Lbk.Mobile.UI.Droid.Bindings
{
    using Android.Util;

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

        public int Value { 
            get
        {
            return this.Value;
        } }

        public event EventHandler ValueChanged;

        public void OnValueChange(NumberPicker picker, int oldVal, int newVal)
        {
            var handler = ValueChanged;
            if (handler != null)
            {
                handler(this, null);
            }
        }
    }
}