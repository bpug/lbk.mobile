//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="NumberPickerTargetBinding.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Bindings
{
    using System;

    using Android.Widget;

    using Cirrious.MvvmCross.Binding;
    using Cirrious.MvvmCross.Binding.Droid.Target;

    public class NumberPickerTargetBinding : MvxAndroidTargetBinding
    {
        public NumberPickerTargetBinding(NumberPicker target)
            : base(target)
        {
        }

        public override MvxBindingMode DefaultMode
        {
            get
            {
                return MvxBindingMode.TwoWay;
            }
        }

        public override Type TargetType
        {
            get
            {
                return typeof(int);
            }
        }

        protected NumberPicker NumberPicker
        {
            get
            {
                return (NumberPicker)this.Target;
            }
        }

        public override void SubscribeToEvents()
        {
            this.NumberPicker.ValueChanged += this.TargetOnValueChanged;
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                var target = this.Target as NumberPicker;
                if (target != null)
                {
                    target.ValueChanged -= this.TargetOnValueChanged;
                }
            }
            base.Dispose(isDisposing);
        }

        protected override void SetValueImpl(object target, object value)
        {
            var pumberPicker = (NumberPicker)target;

            //TODO: 
            pumberPicker.MinValue = 0;
            pumberPicker.MaxValue = 20;

            var intValue = ((int)value);
            pumberPicker.Value = intValue;
        }

        private void TargetOnValueChanged(object sender, EventArgs eventArgs)
        {
            var target = this.Target as NumberPicker;

            if (target == null)
            {
                return;
            }

            int value = target.Value;
            this.FireValueChanged(value);
        }
    }
}