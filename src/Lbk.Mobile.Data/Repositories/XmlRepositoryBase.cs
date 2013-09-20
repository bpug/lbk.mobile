//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="XmlRepositoryBase.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Repositories
{
    using Cirrious.CrossCore;
    using Cirrious.CrossCore.Core;
    using Cirrious.CrossCore.Platform;

    public abstract class XmlRepositoryBase : MvxLockableObject
    {
        private IMvxResourceLoader resourceLoader;

        public IMvxResourceLoader ResourceLoader
        {
            get
            {
                return this.resourceLoader ?? (this.resourceLoader = Mvx.Resolve<IMvxResourceLoader>());
            }
        }
    }
}