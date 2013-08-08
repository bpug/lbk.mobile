//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MvxReachability.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Test.Implementation
{
    using Cirrious.MvvmCross.Plugins.Network.Reachability;

    using Constants = Lbk.Mobile.Core.Constants;

    public class MvxTestReachability : IMvxReachability
    {
        public bool IsHostReachable(string host)
        {
            if (host == Constants.HostReachableName)
            {
                return true;
            }
            return false;
        }
    }
}