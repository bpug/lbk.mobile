//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IErrorReporter.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Services.Error
{
    public interface IErrorReporter
    {
        void ReportError(string error);
    }
}