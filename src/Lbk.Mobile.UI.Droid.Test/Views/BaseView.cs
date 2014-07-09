//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BaseView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Test.Views
{
    using Android.App;
    using Android.OS;

    using Cirrious.MvvmCross.Binding.BindingContext;
    using Cirrious.MvvmCross.Droid.Views;

    using Lbk.Mobile.Core.ViewModels;
    using Lbk.Mobile.UI.Droid.Test.Controls.ProgressBarDialog;
    using Lbk.Mobile.UI.Droid.Test.Extensions;

    [Activity(Icon = "@drawable/ic_launcher")]
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

            this.SetBackground(this.Background);

            this.BindHud();
        }

        protected virtual int Background
        {
            get
            {
                return  Resource.Drawable.background;
            }
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