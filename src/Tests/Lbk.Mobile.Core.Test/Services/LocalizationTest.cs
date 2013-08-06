//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LocalizationTest.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Test.Services
{
    using System.Globalization;
    using System.Threading;

    using Cirrious.MvvmCross.Localization;
    using Cirrious.MvvmCross.Test.Core;

    using Lbk.Mobile.Core;
    using Lbk.Mobile.Core.Services;
    using Lbk.Mobile.Localization;

    using NUnit.Framework;

    [TestFixture]
    public class LocalizationTest : MvxIoCSupportingTest
    {
        [Test]
        public void LocalizeStringTest()
        {
            const string TestDe = "Deutsch";
            const string TestEn = "English";

            base.ClearAll();
            var textProvider = this.Ioc.Resolve<IMvxTextProvider>();
            var text = textProvider.GetText(Constants.GeneralNamespace, "Test", "Text");
            Assert.AreEqual(text, TestDe);

            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");
            base.ClearAll();
            textProvider = this.Ioc.Resolve<IMvxTextProvider>();
            text = textProvider.GetText(Constants.GeneralNamespace, "Test", "Text");
            Assert.AreEqual(text, TestEn);
        }

        protected override void AdditionalSetup()
        {
            this.Ioc.RegisterSingleton<IMvxTextProvider>(new ResxTextProvider(Strings.ResourceManager));
        }
    }
}