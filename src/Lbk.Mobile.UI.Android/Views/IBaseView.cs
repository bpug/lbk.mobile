//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IBaseView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views
{
    using Cirrious.MvvmCross.Views;

    using Lbk.Mobile.Core.ViewModels;

    public interface IBaseView<TViewModel> : IMvxView
        where TViewModel : BaseViewModel
    {
    }
}