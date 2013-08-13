//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ICollectionExtensions.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Common.Extensions
{
    using System.Collections;

    public static class ICollectionExtensions
    {
        public static bool IsNullOrEmpty(this ICollection source)
        {
            return source == null || source.Count == 0;
        }
    }
}