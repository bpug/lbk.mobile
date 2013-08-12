//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MockDispatcher.cs" company="ip-connect GmbH">
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

    public class MockDispatcher : MvxMainThreadDispatcher, IMvxViewDispatcher
    {
        public readonly List<MvxViewModelRequest> Requests = new List<MvxViewModelRequest>();

        public bool ChangePresentation(MvxPresentationHint hint)
        {
            throw new NotImplementedException();
        }

        public bool RequestMainThreadAction(Action action)
        {
            action();
            return true;
        }

        public bool ShowViewModel(MvxViewModelRequest request)
        {
            this.Requests.Add(request);
            return true;
        }
    }
}