//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="AutoMapperConfiguration.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.AutoMapper
{
    using global::AutoMapper;

    internal class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x => { x.AddProfile<LbkProfile>(); });
        }
    }
}