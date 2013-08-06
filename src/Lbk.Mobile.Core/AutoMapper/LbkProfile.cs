//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LbkProfile.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.AutoMapper
{
    using global::AutoMapper;

    using Lbk.Mobile.Data.Service.LbkMobileService;

    public class LbkProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "LbkMappings";
            }
        }

        //protected override void Configure()
        //{
        //    this.CreateMap<Event, Model.Event>();
        //}
    }
}