//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TestBase.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Test
{
    using Cirrious.MvvmCross.Test.Core;

    using NUnit.Framework;

    public class TestBase : MvxIoCSupportingTest
    {
        [SetUp]
        public void Init()
        {
            this.Setup();
        }
    }
}