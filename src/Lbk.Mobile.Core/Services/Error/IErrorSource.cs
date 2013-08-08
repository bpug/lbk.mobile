//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IErrorSource.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Services.Error
{
    using System;

    public interface IErrorSource
    {
        event EventHandler<ErrorEventArgs> ErrorReported;
    }
}