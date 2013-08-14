//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MessageBoxService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Test.Implementation
{
    using System;

    using Lbk.Mobile.Core.Services;

    public class MessageBoxService : IMessageBoxService
    {
        public void Show(
            string message,
            string title,
            string buttonConfirmText,
            string buttonCancelText,
            Action<bool> onDialogClose)
        {
            onDialogClose(true);
        }

        public void Show(string message, string title, string buttonConfirmText, string buttonCancelText)
        {
        }
    }
}