//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BaseView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views
{
    using Cirrious.MvvmCross.Binding.BindingContext;
    using Cirrious.MvvmCross.Droid.Views;

    using Android.OS;

    using Lbk.Mobile.Core.ViewModels;
    using Lbk.Mobile.UI.Droid.Extensions;

    public abstract class BaseView<TViewModel> : MvxActivity, IBaseView<TViewModel>
        where TViewModel : BaseViewModel
    {
        private BindableProgress bindableProgress;

        public new TViewModel ViewModel
        {
            get
            {
                return (TViewModel)base.ViewModel;
            }
            set
            {
                base.ViewModel = value;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            //this.RequestWindowFeature(WindowFeatures.NoTitle);

            base.OnCreate(bundle);

            this.SetBackground();

            this.BindHud();
        }

        private void BindHud()
        {
            this.bindableProgress = new BindableProgress(this);
            var set = this.CreateBindingSet<BaseView<TViewModel>, TViewModel>();
            set.Bind(this.bindableProgress).For(p => p.Visible).To(vm => vm.IsBusy);
            set.Apply();
        }
    }
}