// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DishOfTheDay.cs" company="ip-connect GmbH">
//   Copyright (c) ip-connect GmbH. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Model
{
    using System.Collections.Generic;

    public class DishOfTheDay
    {
        public List<Dish> Dishes { get; set; }

        public string Subtitle { get; set; }

        public string Title { get; set; }
    }
}