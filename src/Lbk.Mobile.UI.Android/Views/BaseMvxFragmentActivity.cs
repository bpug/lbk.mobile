//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BaseMvxFragmentActivity.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views
{
    using Android.App;
    using Android.OS;

    using Cirrious.MvvmCross.Droid.Fragging;

    using Lbk.Mobile.Core.ViewModels;
    using Lbk.Mobile.UI.Droid.Extensions;

    [Activity(Icon = "@drawable/ic_launcher")]
    public abstract class BaseMvxFragmentActivity<TViewModel> : MvxFragmentActivity, IBaseView<TViewModel>
        where TViewModel : BaseViewModel
    {
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
            base.OnCreate(bundle);

            this.SetBackground(this.Background);
        }

        protected virtual int Background
        {
            get
            {
                return Resource.Drawable.background;
            }
        }
    }
}