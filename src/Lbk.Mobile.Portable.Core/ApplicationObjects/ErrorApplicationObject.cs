// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorApplicationObject.cs" company="ip-connect GmbH">
//   Copyright (c) ip-connect GmbH. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Portable.Core.ApplicationObjects
{
    using System;

    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Portable.Core.Interfaces.Errors;

    public class ErrorApplicationObject : MvxApplicationObject, IErrorReporter, IErrorSource
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