//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BaseFragmentActivity.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views
{
    using Android.App;
    using Android.OS;

    using Cirrious.MvvmCross.Binding.BindingContext;
    using Cirrious.MvvmCross.Droid.Fragging;

    using Lbk.Mobile.Core.ViewModels;
    using Lbk.Mobile.UI.Droid.Controls.ProgressBarDialog;
    using Lbk.Mobile.UI.Droid.Extensions;

    [Activity(Icon = "@drawable/ic_launcher")]
    public abstract class BaseFragmentActivity<TViewModel> : MvxFragmentActivity, IBaseView<TViewModel>
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

        protected virtual int Background
        {
            get
            {
                return Resource.Drawable.background;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetBackground(this.Background);

            this.BindHud();
        }

        private void BindHud()
        {
            this.bindableProgress = new BindableProgress(this);
            var set = this.CreateBindingSet<BaseFragmentActivity<TViewModel>, TViewModel>();
            set.Bind(this.bindableProgress).For(p => p.Visible).To(vm => vm.IsBusy);
            set.Apply();
        }
    }
}