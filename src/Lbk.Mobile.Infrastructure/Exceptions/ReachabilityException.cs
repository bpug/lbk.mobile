//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ReachabilityException.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Infrastructure.Exceptions
{
    using System;

    public class ReachabilityException : Exception
    {
        public ReachabilityException()
        {
        }

        public ReachabilityException(string message)
            : base(message)
        {
        }

        public ReachabilityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}