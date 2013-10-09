//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IReachability.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Plugin.Reachability
{
    public interface IReachability
    {
        bool IsHostReachable(string host);
    }
}