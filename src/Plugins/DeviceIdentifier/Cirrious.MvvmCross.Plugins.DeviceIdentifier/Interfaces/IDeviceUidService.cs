//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IDeviceUidService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Cirrious.MvvmCross.Plugins.DeviceIdentifier.Interfaces
{
    using System;

    public interface IDeviceUidService
    {
        void GetDeviceUid(Action<string> onSuccess, Action<Exception> onError);

        string GetDeviceUid();
    }
}