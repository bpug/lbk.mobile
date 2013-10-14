//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MessageBoxService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Plugin.UserInteraction.Droid
{
    using System;
    using System.Threading.Tasks;

    using Android.App;
    using Android.Widget;

    using Cirrious.CrossCore;
    using Cirrious.CrossCore.Droid.Platform;

    public class MessageBoxService : IMessageBoxService
    {
        protected Activity CurrentActivity
        {
            get
            {
                return Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            }
        }

        public void Error(string message, string title = "Sorry!", Action done = null)
        {
            this.Alert(message, title, "Ok", done);
        }

        public void Alert(string message, string title, string buttonText, Action done = null)
        {
            Application.SynchronizationContext.Post(
                ignored =>
                {
                    if (this.CurrentActivity == null)
                    {
                        return;
                    }
                    new AlertDialog.Builder(this.CurrentActivity).SetMessage(message)
                        .SetCancelable(false)
                        .SetTitle(title)
                        .SetPositiveButton(
                            buttonText,
                            delegate
                            {
                                if (done != null)
                                {
                                    done();
                                }
                            }).Show();
                },
                null);
        }

        public Task AlertAsync(string message, string title, string buttonText)
        {
            var tcs = new TaskCompletionSource<object>();
            this.Alert(message, title, buttonText, () => tcs.SetResult(null));
            return tcs.Task;
        }

        public void Confirm(string message, string title, string okButton, string cancelButton, Action<bool> answer)
        {
            Application.SynchronizationContext.Post(
                ignored =>
                {
                    if (this.CurrentActivity == null)
                    {
                        return;
                    }
                    new AlertDialog.Builder(this.CurrentActivity).SetMessage(message)
                        .SetCancelable(false)
                        .SetTitle(title)
                        .SetPositiveButton(
                            okButton,
                            delegate
                            {
                                if (answer != null)
                                {
                                    answer(true);
                                }
                            }).SetNegativeButton(
                                cancelButton,
                                delegate
                                {
                                    if (answer != null)
                                    {
                                        answer(false);
                                    }
                                }).Show();
                },
                null);
        }

        public void Confirm(string message, string title, string okButton, string cancelButton, Action okClicked)
        {
            this.Confirm(
                message,
                title,
                okButton,
                cancelButton,
                confirmed =>
                {
                    if (confirmed)
                    {
                        okClicked();
                    }
                });
        }

        public Task<bool> ConfirmAsync(string message, string title, string okButton, string cancelButton)
        {
            var tcs = new TaskCompletionSource<bool>();
            this.Confirm(message, title, okButton, cancelButton, confirmed => tcs.SetResult(confirmed));
            return tcs.Task;
        }

        public void Input(
            string message,
            string title,
            string okButton,
            string cancelButton,
            Action<string> okClicked,
            string placeholder = null)
        {
            this.Input(
                message,
                title,
                okButton,
                cancelButton,
                (ok, text) =>
                {
                    if (ok)
                    {
                        okClicked(text);
                    }
                },
                placeholder);
        }

        public void Input(
            string message,
            string title,
            string okButton,
            string cancelButton,
            Action<bool, string> answer,
            string placeholder = null)
        {
            Application.SynchronizationContext.Post(
                ignored =>
                {
                    if (this.CurrentActivity == null)
                    {
                        return;
                    }
                    var input = new EditText(this.CurrentActivity)
                    {
                        Hint = placeholder
                    };

                    new AlertDialog.Builder(this.CurrentActivity).SetMessage(message)
                        .SetTitle(title)
                        .SetView(input)
                        .SetPositiveButton(
                            okButton,
                            delegate
                            {
                                if (answer != null)
                                {
                                    answer(true, input.Text);
                                }
                            }).SetNegativeButton(
                                cancelButton,
                                delegate
                                {
                                    if (answer != null)
                                    {
                                        answer(false, input.Text);
                                    }
                                }).Show();
                },
                null);
        }

        public Task<InputResponse> InputAsync(
            string message,
            string title,
            string okButton,
            string cancelButton,
            string placeholder = null)
        {
            var tcs = new TaskCompletionSource<InputResponse>();
            this.Input(
                message,
                title,
                okButton,
                cancelButton,
                (ok, text) => tcs.SetResult(
                    new InputResponse()
                    {
                        Ok = ok,
                        Text = text
                    }),
                placeholder);
            return tcs.Task;
        }
    }
}