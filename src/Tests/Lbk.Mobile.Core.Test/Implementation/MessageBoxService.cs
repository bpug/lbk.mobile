//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MessageBoxService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Test.Implementation
{
    using System;
    using System.Threading.Tasks;

    using Lbk.Mobile.Core.Services;
    using Lbk.Mobile.Plugin.UserInteraction;

    public class MessageBoxService : IMessageBoxService
    {
        //public void Show(
        //    string message,
        //    string title,
        //    string buttonConfirmText,
        //    string buttonCancelText,
        //    Action<bool> onDialogClose)
        //{
        //    onDialogClose(true);
        //}

        //public void Show(string message, string title, string buttonConfirmText, string buttonCancelText)
        //{
        //}

        //public void Show(string message, string title, string buttonText, Action onBoxClose)
        //{
        //    throw new NotImplementedException();
        //}

        public void Error(string message, string title = "Sorry!", Action done = null)
        {
            throw new NotImplementedException();
        }

        public void Alert(string message, string title, string buttonText, Action done = null)
        {
            throw new NotImplementedException();
        }

        public Task AlertAsync(string message, string title, string buttonText)
        {
            throw new NotImplementedException();
        }

        public void Confirm(string message, string title, string okButton, string cancelButton, Action<bool> answer)
        {
            answer(true);
        }

        public void Confirm(string message, string title, string okButton, string cancelButton, Action okClicked)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ConfirmAsync(string message, string title, string okButton, string buttonCancelText)
        {
            throw new NotImplementedException();
        }

        public void Input(
            string message,
            string title,
            string okButton,
            string cancelButton,
            Action<string> okClicked,
            string placeholder = null)
        {
            throw new NotImplementedException();
        }

        public void Input(string message, string title, string okButton, string cancelButton, Action<bool, string> answer, string placeholder = null)
        {
            throw new NotImplementedException();
        }

        public Task<InputResponse> InputAsync(string message, string title, string okButton, string cancelButton, string placeholder = null)
        {
            throw new NotImplementedException();
        }
    }
}