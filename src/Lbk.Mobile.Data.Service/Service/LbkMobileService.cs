//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LbkMobileService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Service.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Lbk.Mobile.Data.Service.Extensions;
    using Lbk.Mobile.Data.Service.LbkMobileService;

    public class LbkMobileService : BaseService<Service1SoapClient>, ILbkMobileService
    {
        public async Task<List<Event>> GetEventsAsync(string fingerprint)
        {
            var result = await this.Service.GetEventsAsyncTask(fingerprint);
            return result;
        }
    }
}