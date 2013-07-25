// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorEventArgs.cs" company="ip-connect GmbH">
//   Copyright (c) ip-connect GmbH. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Interfaces.Errors
{
    using System;

    public class ErrorEventArgs : EventArgs
    {
        public ErrorEventArgs(string message)
        {
            this.Message = message;
        }

        public string Message { get; private set; }
    }
}