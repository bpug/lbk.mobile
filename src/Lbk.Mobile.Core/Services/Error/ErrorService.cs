//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ErrorService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Services.Error
{
    using Cirrious.MvvmCross.Plugins.Messenger;

    using Lbk.Mobile.Core.Messages;
    using Lbk.Mobile.Infrastructure;

    public class ErrorService : IErrorService
    {
        private readonly IMvxMessenger messenger;

        public ErrorService(IMvxMessenger messenger)
        {
            this.messenger = messenger;
        }

        public void ReportError(string errorMessage)
        {
            Trace.Error("Error reported: {0}", errorMessage);
            this.messenger.Publish(new ErrorMessage(this, errorMessage));
        }
    }
}