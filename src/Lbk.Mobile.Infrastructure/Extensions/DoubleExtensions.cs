//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DoubleExtensions.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Infrastructure.Extensions
{
    using System;

    public static class DoubleExtensions
    {
        public static double Truncate(this double value)
        {
            return value >= 0 ? Math.Floor(value) : Math.Ceiling(value);
        }
    }
}