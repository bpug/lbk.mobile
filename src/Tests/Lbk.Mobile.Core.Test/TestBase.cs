//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TestBase.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Test
{
    using Cirrious.CrossCore.Core;
    using Cirrious.MvvmCross.Test.Core;
    using Cirrious.MvvmCross.Views;

    using Lbk.Mobile.Core.Test.Mocks;

    using Moq;

    using NUnit.Framework;

    public class TestBase : MvxIoCSupportingTest
    {
        [SetUp]
        public void Init()
        {
            this.Setup();
        }

        protected MockMvxViewDispatcher CreateMockNavigation()
        {
            var viewDispatcherMock = new Mock<IMvxViewDispatcher>();
            var dispatcher = new MockMvxViewDispatcher(viewDispatcherMock.Object);
            //var dispatcher = new MockMvxViewDispatcher();
            Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(dispatcher);
            Ioc.RegisterSingleton<IMvxViewDispatcher>(dispatcher);
            return dispatcher;
        }
    }

}