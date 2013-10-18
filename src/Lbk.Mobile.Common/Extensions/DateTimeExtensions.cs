//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DateTimeExtensions.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Common.Extensions
{
    using System;

    public static class DateTimeExtensions
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static double GetMillisecondsSinceUnixEpoch(this DateTime source)
        {
            return source.ToUniversalTime().Subtract(UnixEpoch).TotalMilliseconds;
        }
    }
}