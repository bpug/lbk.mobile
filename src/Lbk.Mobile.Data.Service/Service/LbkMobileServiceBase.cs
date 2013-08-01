//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BaseService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Service.Service
{
    using System;
    using System.ServiceModel;

    public abstract class LbkMobileServiceBase<T>
        where T : class, new()
    {
        private T service;

        ~LbkMobileServiceBase()
        {
            this.Dispose();
        }

        public T Service
        {
            get
            {
                var communicationObject = (ICommunicationObject)this.service;

                if (communicationObject == null || communicationObject.State == CommunicationState.Faulted
                    || communicationObject.State == CommunicationState.Closing
                    || communicationObject.State == CommunicationState.Closed)
                {
                    this.Dispose();
                    this.InitializeProxy();
                }

                return this.service;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                var communicationObject = (ICommunicationObject)this.service;
                if (communicationObject != null)
                {
                    communicationObject.Close();
                    this.service = null;
                }
            }
        }

        private void InitializeProxy()
        {
            this.service = new T();

            var communicationObject = (ICommunicationObject)this.service;
            if (communicationObject != null)
            {
                communicationObject.Open();
            }
        }
    }
}