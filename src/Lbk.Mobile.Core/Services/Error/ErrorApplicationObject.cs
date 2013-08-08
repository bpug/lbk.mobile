//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ErrorApplicationObject.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Services.Error
{
    using System;

    using Cirrious.CrossCore.Core;

    public class ErrorApplicationObject : MvxMainThreadDispatchingObject, IErrorReporter, IErrorSource
    {
        public event EventHandler<ErrorEventArgs> ErrorReported;

        public void ReportError(string error)
        {
            if (this.ErrorReported == null)
            {
                return;
            }

            this.InvokeOnMainThread(
                () =>
                {
                    var handler = this.ErrorReported;
                    if (handler != null)
                    {
                        handler(this, new ErrorEventArgs(error));
                    }
                });
        }
    }
}