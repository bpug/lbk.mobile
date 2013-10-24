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
    using Cirrious.MvvmCross.Plugins.PhoneCall;
    using Cirrious.MvvmCross.Plugins.WebBrowser;
    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Common;
    using Lbk.Mobile.Common.Exceptions;
    using Lbk.Mobile.Common.Utils;
    using Lbk.Mobile.Plugin.UserInteraction;
    using Lbk.Mobile.Plugin.WebVideo;

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

        public IMvxLanguageBinder SharedTextSource
        {
            get
            {
                return new MvxLanguageBinder(Constants.GeneralNamespace, Constants.ShareType);
            }
        }

        public IMvxLanguageBinder TextSource
        {
            get
            {
                return new MvxLanguageBinder(Constants.GeneralNamespace, this.GetType().Name);
            }
        }

        protected IMvxMessenger MvxMessenger
        {
            get
            {
                return this.mvxMessenger ?? (this.mvxMessenger = Mvx.Resolve<IMvxMessenger>());
            }
        }

        //public void ReportError(string error)
        //{
        //    Mvx.Resolve<IErrorService>().ReportError(error);
        //}

        public async Task AsyncExecute<T>(
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

        public async Task AsyncExecute<TResult, T1>(
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

        public async Task AsyncExecute<TResult, T1, T2>(
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

        public async Task AsyncExecute<TResult, T1, T2, T3>(
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

        protected string GetErrorText(string text)
        {
            return this.ErrorTextSource.GetText(text);
        }

        protected string GetSharedText(string text)
        {
            return this.SharedTextSource.GetText(text);
        }

        protected string GetSharedText(string text, params object[] formatArgs)
        {
            return this.SharedTextSource.GetText(text, formatArgs);
        }

        protected string GetText(string text)
        {
            return this.TextSource.GetText(text);
        }

        protected string GetText(string text, params object[] formatArgs)
        {
            return this.TextSource.GetText(text, formatArgs);
        }

        protected void MakePhoneCall(string name, string number)
        {
            var task = Mvx.Resolve<IMvxPhoneCallTask>();
            task.MakePhoneCall(name, number);
        }

        protected void PlayYoutubeVideo(string videoUrl, string title)
        {
            if (string.IsNullOrEmpty(videoUrl))
            {
                return;
            }

            string id = Utility.GetYuotubeVideoId(videoUrl);
            if (string.IsNullOrEmpty(videoUrl))
            {
                return;
            }
            var task = Mvx.Resolve<IWebVideoTask>();
            task.PlayYoutubeVideo(id, title);
        }

        protected void ShowAlert(string message, string title)
        {
            string buttonConfirmText = this.GetSharedText("ButtonOk");
            this.MessageBoxService.Alert(message, title, buttonConfirmText);
        }

        protected void ShowConfirm(string message, string title, Action<bool> onDialogClose)
        {
            string buttonConfirmText = this.GetSharedText("ButtonYes");
            string buttonCancelText = this.GetSharedText("ButtonNo");
            this.MessageBoxService.Confirm(message, title, buttonConfirmText, buttonCancelText, onDialogClose);
        }

        protected void ShowWebPage(string webPage)
        {
            if (string.IsNullOrEmpty(webPage))
            {
                return;
            }
            var task = Mvx.Resolve<IMvxWebBrowserTask>();
            task.ShowWebPage(webPage);
        }

        protected MvxSubscriptionToken Subscribe<TMessage>(Action<TMessage> action) where TMessage : MvxMessage
        {
            return this.MvxMessenger.Subscribe<TMessage>(action, MvxReference.Weak);
        }

        protected MvxSubscriptionToken SubscribeOnMainThread<TMessage>(Action<TMessage> action) where TMessage : MvxMessage
        {
            return this.MvxMessenger.SubscribeOnMainThread<TMessage>(action, MvxReference.Weak);
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
                        //this.ReportError(this.GetErrorText("OnAsyncExecute") + ex.Message);
                        this.MessageBoxService.Error(this.GetErrorText("OnAsyncExecute") + ex.Message);
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
                string errorText = this.GetErrorText("NotReachable");
                this.MessageBoxService.Error(
                    errorText,
                    this.GetSharedText("Sorry"),
                    () =>
                    {
                        if (onError != null)
                        {
                            onError(new ReachabilityException(errorText));
                        }
                    });

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