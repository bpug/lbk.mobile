// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IErrorReporter.cs" company="ip-connect GmbH">
//   Copyright (c) ip-connect GmbH. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Portable.Core.Interfaces.Errors
{
    public interface IErrorReporter
    {
        void ReportError(string error);
    }
}