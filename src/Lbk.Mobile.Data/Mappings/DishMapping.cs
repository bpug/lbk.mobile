//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DishMapping.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Mappings
{
    using System.Collections.Generic;
    using System.Linq;

    using Lbk.Mobile.Data.LbkMobileService;
    using Lbk.Mobile.Model;

    public static class DishMapping
    {
        public static List<Dish> ToModel(this IEnumerable<dish> sourceList)
        {
            return sourceList.Select(p => p.ToModel()).ToList();
        }

        public static Dish ToModel(this dish source)
        {
            var dish = new Dish()
            {
                Description = source.Description,
                Headline = source.Headline,
                Price = source.Price
            };

            return dish;
        }
    }
}