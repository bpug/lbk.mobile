//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DoubleExtensions.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Common.Extensions
{
    using System;

    public static class DoubleExtensions
    {
        public static double Truncate(this double value)
        {
            return value >= 0 ? Math.Floor(value) : Math.Ceiling(value);
        }

        public static string DistanceToString(this double distance)
        {
            if (distance < 100)
                return string.Format("{0} m", Math.Round(distance));
            else if (distance < 1000)
                return string.Format("{0} m", Math.Round(distance / 5) * 5);
            else if (distance < 10000)
                return string.Format("{0} km", Math.Round(distance / 100) / 10);
            else
                return string.Format("{0} km", Math.Round(distance / 1000));
        }
    }
}