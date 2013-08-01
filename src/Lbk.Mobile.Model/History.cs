//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="History.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Model
{
    public class History
    {
        public string Date { get; set; }
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int PageIndex { get; set; }
        public string Title { get; set; }
    }
}