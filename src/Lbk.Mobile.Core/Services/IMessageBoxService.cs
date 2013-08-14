//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IMessageBoxService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Services
{
    using System;

    public interface IMessageBoxService
    {
       void Show(
            string message,
            string title,
            string buttonConfirmText,
            string buttonCancelText,
            Action<bool> onBoxClose);

        void Show(string message, string title, string buttonConfirmText, string buttonCancelText);

        void Show(string message, string title, string buttonText, Action onBoxClose);
    }
}