//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BaseViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.Localization;
    using Cirrious.MvvmCross.Plugins.Email;
    using Cirrious.MvvmCross.Plugins.Messenger;
    using Cirrious.MvvmCross.Plugins.Network.Reachability;
    using Cirrious.MvvmCross.Plugins.WebBrowser;
    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Common;
    using Lbk.Mobile.Common.Exceptions;
    using Lbk.Mobile.Core.Services;
    using Lbk.Mobile.Core.Services.Error;

    public abstract class BaseViewModel : MvxViewModel
    {
        private bool isBusy;

        private IMessageBoxService messageBoxService;

        private IMvxMessenger mvxMessenger;

        public ICommand BackCommand
        {
            get
            {
                return new MvxCommand(() => this.Close(this));
            }
        }

        public IMvxLanguageBinder ErrorTextSource
        {
            get
            {
                return new MvxLanguageBinder(Constants.GeneralNamespace, Constants.ErrorType);
            }
        }

        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }

            set
            {
                this.isBusy = value;
                this.RaisePropertyChanged(() => this.IsBusy);
            }
        }

        public IMessageBoxService MessageBoxService
        {
            get
            {
                if (this.messageBoxService == null)
                {
                    this.messageBoxService = Mvx.Resolve<IMessageBoxService>();
                }
                return this.messageBoxService;
            }
        }

        protected IMvxMessenger MvxMessenger
        {
            get
            {
                return this.mvxMessenger ?? (this.mvxMessenger = Mvx.Resolve<IMvxMessenger>());
            }
        }

        protected IMvxLanguageBinder SharedTextSource
        {
            get
            {
                return new MvxLanguageBinder(Constants.GeneralNamespace, Constants.ShareType);
            }
        }

        protected IMvxLanguageBinder TextSource
        {
            get
            {
                return new MvxLanguageBinder(Constants.GeneralNamespace, this.GetType().Name);
            }
        }

        public void ReportError(string error)
        {
            Mvx.Resolve<IErrorService>().ReportError(error);
        }

        protected async Task AsyncExecute<T>(
            Func<Task<T>> execute,
            Action<T> onSuccess,
            Action<Exception> onError = null)
        {
            if (!this.CanAsyncExecute(onError))
            {
                return;
            }

            var task = execute();

            await AsyncExecute(task, onSuccess, onError);

            //await task.ContinueWith(
            //    t =>
            //    {
            //        if (t.IsFaulted)
            //        {
            //            var ex = (Exception)t.Exception;
            //            Trace.Error("OnLoadExecute Error: " + ex.Message);
            //            if (onError != null)
            //            {
            //                onError(ex);
            //            }
            //        }
            //        else
            //        {
            //            onSuccess(t.Result);
            //        }
            //        this.IsBusy = false;
            //    });
        }

        protected async Task AsyncExecute<TResult, T1>(
            Func<T1, Task<TResult>> execute,
            T1 parameter1,
            Action<TResult> onSuccess,
            Action<Exception> onError = null)
        {
            if (!this.CanAsyncExecute(onError))
            {
                return;
            }

            var task = execute(parameter1);

            await AsyncExecute(task, onSuccess, onError);
        }

        protected async Task AsyncExecute<TResult, T1, T2>(
            Func<T1, T2, Task<TResult>> execute,
            T1 parameter1,
            T2 parameter2,
            Action<TResult> onSuccess,
            Action<Exception> onError = null)
        {
            if (!this.CanAsyncExecute(onError))
            {
                return;
            }

            var task = execute(parameter1, parameter2);

            await AsyncExecute(task, onSuccess, onError);
        }

        protected async Task AsyncExecute<TResult, T1, T2, T3>(
            Func<T1, T2, T3, Task<TResult>> execute,
            T1 parameter1,
            T2 parameter2,
            T3 parameter3,
            Action<TResult> onSuccess,
            Action<Exception> onError = null)
        {
            if (!this.CanAsyncExecute(onError))
            {
                return;
            }

            this.IsBusy = true;

            var task = execute(parameter1, parameter2, parameter3);

            await AsyncExecute(task, onSuccess, onError);
        }

        protected void ComposeEmail(string to, string subject, string body)
        {
            var task = Mvx.Resolve<IMvxComposeEmailTask>();
            task.ComposeEmail(to, null, subject, body, false);
        }

        protected void ShowMessage(string message, string title, Action<bool> onDialogClose)
        {
            string buttonConfirmText = this.SharedTextSource.GetText("ButtonConfirmText");
            string buttonCancelText = this.SharedTextSource.GetText("ButtonCancelText");
            this.MessageBoxService.Show(message, title, buttonConfirmText, buttonCancelText, onDialogClose);
        }

        protected void ShowMessage(string message, string title)
        {
            string buttonConfirmText = this.SharedTextSource.GetText("ButtonConfirmText");
            string buttonCancelText = this.SharedTextSource.GetText("ButtonCancelText");
            this.MessageBoxService.Show(message, title, buttonConfirmText, buttonCancelText);
        }

        protected void ShowWebPage(string webPage)
        {
            var task = Mvx.Resolve<IMvxWebBrowserTask>();
            task.ShowWebPage(webPage);
        }

        protected MvxSubscriptionToken Subscribe<TMessage>(Action<TMessage> action) where TMessage : MvxMessage
        {
            return this.MvxMessenger.Subscribe<TMessage>(action, MvxReference.Weak);
        }

        protected void Unsubscribe<TMessage>(MvxSubscriptionToken token) where TMessage : MvxMessage
        {
            this.MvxMessenger.Unsubscribe<TMessage>(token);
        }

        private static bool IsReachable()
        {
            var reachability = Mvx.Resolve<IMvxReachability>();
            return reachability.IsHostReachable(Constants.HostReachableName);
        }

        private async Task AsyncExecute<TResult>(
            Task<TResult> task,
            Action<TResult> onSuccess,
            Action<Exception> onError = null)
        {
            if (task == null)
            {
                return;
            }

            await task.ContinueWith(
                t =>
                {
                    if (t.IsFaulted)
                    {
                        var ex = (Exception)t.Exception;
                        Trace.Error("OnAsyncExecute Error: " + ex.Message);
                        this.ReportError(this.ErrorTextSource.GetText("OnAsyncExecute") + ex.Message);
                        if (onError != null)
                        {
                            onError(ex);
                        }
                    }
                    else
                    {
                        onSuccess(t.Result);
                    }
                    this.IsBusy = false;
                });
        }

        private bool CanAsyncExecute(Action<Exception> onError)
        {
            if (!IsReachable())
            {
                this.ReportError(this.ErrorTextSource.GetText("NotReachable"));
                if (onError != null)
                {
                    onError(new ReachabilityException());
                }
                return false;
            }

            if (this.IsBusy)
            {
                return false;
            }

            this.IsBusy = true;
            return true;
        }
    }
}