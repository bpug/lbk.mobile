// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IErrorSource.cs" company="ip-connect GmbH">
//   Copyright (c) ip-connect GmbH. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Portable.Core.Interfaces.Errors
{
    using System;

    public interface IErrorSource
    {
        event EventHandler<ErrorEventArgs> ErrorReported;
    }
}