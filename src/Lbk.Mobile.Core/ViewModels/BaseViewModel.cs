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

        protected async void OnAsyncExecute<T>(Func<Task<T>> execute, Action<T> success)
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
                    }
                    else
                    {
                        success(t.Result);
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