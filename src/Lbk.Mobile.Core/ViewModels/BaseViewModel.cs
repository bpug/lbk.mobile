//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BaseViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels
{
    using System;
    using System.Threading.Tasks;

    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.Localization;
    using Cirrious.MvvmCross.Plugins.Email;
    using Cirrious.MvvmCross.Plugins.Messenger;
    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Core.Interfaces.Errors;
    using Lbk.Mobile.Infrastructure;

    public class BaseViewModel : MvxViewModel
    {
        private bool isBusy;

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

        
        protected async void AsyncExecute<T>(Func<Task<T>> execute, Action<T> onSuccess, Action<Exception> onError = null)
        {
            if (IsBusy)
                return;

            this.IsBusy = true;

            //object param = null;

           var task = execute();

            await task.ContinueWith(
                t =>
                {
                    if (t.IsFaulted)
                    {
                        var ex = (Exception)t.Exception;
                        Trace.Error("OnLoadExecute Error: " + ex.Message);
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

        protected async void AsyncExecute<TResult, T1>(
            Func<T1, Task<TResult>> execute,
            T1 parameter1,
            Action<TResult> onSuccess,
            Action<Exception> onError = null)
        {
            if (IsBusy)
                return;

            this.IsBusy = true;

            var task = execute(parameter1);

            await task.ContinueWith(
                t =>
                {
                    if (t.IsFaulted)
                    {
                        var ex = (Exception)t.Exception;
                        Trace.Error("OnLoadExecute Error: " + ex.Message);
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

        protected async void AsyncExecute<TResult, T1, T2>(Func<T1, T2, Task<TResult>> execute, T1 parameter1, T2 parameter2, Action<TResult> onSuccess, Action<Exception> onError = null)
        {
            if (IsBusy)
                return;

            this.IsBusy = true;

            //object param = null;

            var task = execute(parameter1, parameter2);

            await task.ContinueWith(
                t =>
                {
                    if (t.IsFaulted)
                    {
                        var ex = (Exception)t.Exception;
                        Trace.Error("OnLoadExecute Error: " + ex.Message);
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

        protected async void AsyncExecute<TResult, T1, T2, T3>(Func<T1, T2, T3, Task<TResult>> execute, T1 parameter1, T2 parameter2, T3 parameter3, Action<TResult> onSuccess, Action<Exception> onError = null)
        {
            if (IsBusy)
                return;

            this.IsBusy = true;

            //object param = null;

            var task = execute(parameter1, parameter2, parameter3);

            await task.ContinueWith(
                t =>
                {
                    if (t.IsFaulted)
                    {
                        var ex = (Exception)t.Exception;
                        Trace.Error("OnLoadExecute Error: " + ex.Message);
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

        public IMvxLanguageBinder TextSource
        {
            get
            {
                return new MvxLanguageBinder(Constants.GeneralNamespace, this.GetType().Name);
            }
        }

        private IMvxMessenger MvxMessenger
        {
            get
            {
                return Mvx.Resolve<IMvxMessenger>();
            }
        }

        public void ReportError(string error)
        {
            Mvx.Resolve<IErrorReporter>().ReportError(error);
        }

        protected void ComposeEmail(string to, string subject, string body)
        {
            var task = Mvx.Resolve<IMvxComposeEmailTask>();
            task.ComposeEmail(to, null, subject, body, false);
        }

        protected MvxSubscriptionToken Subscribe<TMessage>(Action<TMessage> action) where TMessage : MvxMessage
        {
            return this.MvxMessenger.Subscribe<TMessage>(action, MvxReference.Weak);
        }

        protected void Unsubscribe<TMessage>(MvxSubscriptionToken id) where TMessage : MvxMessage
        {
            this.MvxMessenger.Unsubscribe<TMessage>(id);
        }
    }
}