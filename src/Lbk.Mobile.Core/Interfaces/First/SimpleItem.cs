//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="SimpleItem.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Interfaces.First
{
    using System;

    public class SimpleItem
    {
        public int Id { get; set; }
        public string Notes { get; set; }
        public string Title { get; set; }
        public DateTime When { get; set; }
    }
}