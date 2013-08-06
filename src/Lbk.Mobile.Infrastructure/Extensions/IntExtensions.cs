//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IntExtensions.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Infrastructure.Extensions
{
    using System;

    public static class IntExtensions
    {
        public static string ToLetter(this int source)
        {
            const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string retVal = string.Empty;
            double dvalue = Convert.ToDouble(source);
            do
            {
                double remainder = dvalue - (26 * (dvalue / 26).Truncate());
                retVal = retVal + Letters.Substring((int)remainder, 1);
                dvalue = (dvalue / 26).Truncate();
            }
            while (dvalue > 0);
            return retVal;
        }
    }
}