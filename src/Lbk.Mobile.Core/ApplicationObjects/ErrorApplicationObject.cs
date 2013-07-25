// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorApplicationObject.cs" company="ip-connect GmbH">
//   Copyright (c) ip-connect GmbH. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ApplicationObjects
{
    using System;

    using Cirrious.CrossCore.Core;

    using Lbk.Mobile.Core.Interfaces.Errors;

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
                    EventHandler<ErrorEventArgs> handler = this.ErrorReported;
                    if (handler != null)
                    {
                        handler(this, new ErrorEventArgs(error));
                    }
                });
        }
    }
}