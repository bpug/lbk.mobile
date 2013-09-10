//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MenuCategoryMapping.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Lbk.Mobile.Common.Extensions;
    using Lbk.Mobile.Data.LbkMobileService;
    using Lbk.Mobile.Model;

    public static class MenuCategoryMapping
    {
        public static List<MenuCategory> ToModel(this IEnumerable<category> sourceList)
        {
            if (sourceList == null)
            {
                return null;
            }
            return sourceList.Select(p => p.ToModel()).ToList();
        }

        public static MenuCategory ToModel(this category source)
        {
            var category = new MenuCategory()
            {
                Subtitle = source.Subtitle,
                Title = source.Title
            };

            if (!source.Dishes.IsNullOrEmpty())
            {
                category.Dishes = source.Dishes.ToModel();
            }

            return category;
        }
    }
}