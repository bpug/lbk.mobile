//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IMessageBoxService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Plugin.UserInteraction
{
    using System;
    using System.Threading.Tasks;

    public interface IMessageBoxService
    {
        void Error(string message, string title = "Sorry!", Action done = null);

        void Alert(string message, string title, string buttonText, Action done = null);

        Task AlertAsync(string message, string title, string buttonText);

        void Confirm(string message, string title, string okButton, string cancelButton, Action<bool> answer);

        void Confirm(string message, string title, string okButton, string cancelButton, Action okClicked);
        

        Task<bool> ConfirmAsync(string message, string title, string okButton, string buttonCancelText);

        void Input(
            string message,
            string title,
            string okButton,
            string cancelButton,
            Action<string> okClicked,
            string placeholder = null);

        void Input(
            string message,
            string title,
            string okButton,
            string cancelButton,
            Action<bool, string> answer,
            string placeholder = null);

        Task<InputResponse> InputAsync(
            string message,
            string title,
            string okButton,
            string cancelButton,
            string placeholder = null);
    }
}