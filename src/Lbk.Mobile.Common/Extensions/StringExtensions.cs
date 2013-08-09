//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="StringExtensions.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Common.Extensions
{
    using Lbk.Mobile.Common.Cryptography;

    public static class StringExtensions
    {
        public static string GetMd5(this string source)
        {
            return MD5.GetHashString(source);
        }

        public static string Indent(this string source, int count)
        {
            return "".PadLeft(count) + source;
        }

        public static bool IsEmpty(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }
    }
}