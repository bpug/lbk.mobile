﻿//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MockMvxViewDispatcher.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Test.Mocks
{
    using System;
    using System.Collections.Generic;

    using Cirrious.CrossCore.Core;
    using Cirrious.MvvmCross.ViewModels;
    using Cirrious.MvvmCross.Views;

    public class MockMvxViewDispatcher : MvxMainThreadDispatcher, IMvxViewDispatcher
    {
        public readonly List<MvxViewModelRequest> Requests = new List<MvxViewModelRequest>();
        
        private readonly IMvxViewDispatcher _decorated;

        public MockMvxViewDispatcher(IMvxViewDispatcher decorated)
        {
            this._decorated = decorated;
        }

        public bool ChangePresentation(MvxPresentationHint hint)
        {
            return this._decorated.ChangePresentation(hint);
        }

        public bool RequestMainThreadAction(Action action)
        {
            return this._decorated.RequestMainThreadAction(action);
        }

        public bool ShowViewModel(MvxViewModelRequest request)
        {
              Requests.Add(request);
            return this._decorated.ShowViewModel(request);
        }
    }
}