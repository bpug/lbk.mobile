//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IErrorService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Services.Error
{
    public interface IErrorService
    {
        void ReportError(string error);
    }
}