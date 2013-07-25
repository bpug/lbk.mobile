// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseViewModel.cs" company="ip-connect GmbH">
//   Copyright (c) ip-connect GmbH. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels
{
    using System;

    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.Localization;
    using Cirrious.MvvmCross.Plugins.Email;
    using Cirrious.MvvmCross.Plugins.Messenger;
    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Core.Interfaces.Errors;

    public class BaseViewModel : MvxViewModel
    {
        private bool isLoading;
        

        public bool IsLoading
        {
            get
            {
                return this.isLoading;
            }

            set
            {
                this.isLoading = value;
                this.RaisePropertyChanged(() => this.IsLoading);
            }
        }

        public IMvxLanguageBinder TextSource
        {
            get { return new MvxLanguageBinder(Constants.GeneralNamespace, GetType().Name); }
        }

        protected void ComposeEmail(string to, string subject, string body)
        {
            var task = Mvx.Resolve<IMvxComposeEmailTask>();
            task.ComposeEmail(to, null, subject, body, false);
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


        protected MvxSubscriptionToken Subscribe<TMessage>(Action<TMessage> action)
            where TMessage : MvxMessage
        {
            return this.MvxMessenger.Subscribe<TMessage>(action, MvxReference.Weak);
        }

        protected void Unsubscribe<TMessage>(MvxSubscriptionToken id)
            where TMessage : MvxMessage
        {
            this.MvxMessenger.Unsubscribe<TMessage>(id);
        }
    }
}