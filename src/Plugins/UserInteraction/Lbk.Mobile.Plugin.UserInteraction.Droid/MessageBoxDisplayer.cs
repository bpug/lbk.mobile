//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MessageBoxService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Controls
{
    using System;

    using Android.App;

    using Cirrious.CrossCore;
    using Cirrious.CrossCore.Droid.Platform;
    using Cirrious.MvvmCross.Binding.BindingContext;

    using Lbk.Mobile.Core.Services;

    public class MessageBoxDisplayer : IMessageBoxService
    {
        public void Show(
            string message,
            string title,
            string buttonConfirmText,
            string buttonCancelText,
            Action<bool> onBoxClose)
        {
            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity as IMvxBindingContextOwner;
            var alertDialog = new AlertDialog.Builder((Activity)activity).Create();
            alertDialog.SetCancelable(false);
            alertDialog.SetTitle(title);
            alertDialog.SetMessage(message);
            alertDialog.SetButton(buttonConfirmText, (sender, args) => onBoxClose(true));
            alertDialog.SetButton(buttonCancelText, (sender, args) => onBoxClose(false));
            alertDialog.Show();
        }

        public void Show(string message, string title, string buttonConfirmText, string buttonCancelText)
        {
            throw new NotImplementedException();
        }

        public void Show(string message, string title, string buttonText, Action onBoxClose)
        {
            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity as IMvxBindingContextOwner;
            var alertDialog = new AlertDialog.Builder((Activity)activity).Create();
            alertDialog.SetTitle(title);
            alertDialog.SetMessage(message);
            alertDialog.SetButton(buttonText, (sender, args) => onBoxClose());
            alertDialog.Show();
        }
    }
}