//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BaseViewExtensions.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Android.Extensions
{
    using System.Globalization;

    using Cirrious.MvvmCross.Localization;

    using Lbk.Mobile.Core.ViewModels;
    using Lbk.Mobile.UI.Android.Views;

    public static class BaseViewExtensionMethods
    {
        private static readonly MvxLanguageConverter Converter = new MvxLanguageConverter();

        public static string GetSharedText<TViewModel>(this IBaseView<TViewModel> view, string which)
            where TViewModel : BaseViewModel
        {
            return GetTextCommon(((BaseViewModel)view.ViewModel).SharedTextSource, which);
        }

        public static string GetErrorText<TViewModel>(this IBaseView<TViewModel> view, string which)
            where TViewModel : BaseViewModel
        {
            return GetTextCommon(((BaseViewModel)view.ViewModel).ErrorTextSource, which);
        }

        public static string GetText<TViewModel>(this IBaseView<TViewModel> view, string which)
            where TViewModel : BaseViewModel
        {
            return GetTextCommon(((BaseViewModel)view.ViewModel).TextSource, which);
        }

        private static string GetTextCommon(IMvxLanguageBinder source, string which)
        {
            return (string)Converter.Convert(source, typeof(string), which, CultureInfo.CurrentUICulture);
        }
    }
}