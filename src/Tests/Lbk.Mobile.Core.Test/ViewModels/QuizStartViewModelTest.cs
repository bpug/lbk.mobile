//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EvetnViewModelTest.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Test.ViewModels
{
    using System;
    using System.Windows.Forms;

    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.Plugins.Messenger;
    using Cirrious.MvvmCross.Plugins.Network.Reachability;

    using Lbk.Mobile.Core.Messages;
    using Lbk.Mobile.Core.Test.Implementation;
    using Lbk.Mobile.Core.Test.Services;
    using Lbk.Mobile.Core.ViewModels;
    using Lbk.Mobile.Core.ViewModels.Event;
    using Lbk.Mobile.Core.ViewModels.Quiz;
    using Lbk.Mobile.Data.Service.Service;
    using Lbk.Mobile.Plugin.DeviceIdentifier;
    using Lbk.Mobile.Plugin.Settings;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class QuizStartViewModelTest : TestBase
    {
        [Test]
        public async void LoadQuiz()
        {
            Settings.YouthProtection = false;

            Action<string> onDeviceUidSuccess = null;
            Action<Exception> onDeviceUidError = null;

            var mvxMessenger = this.Ioc.Resolve<IMvxMessenger>();

            mvxMessenger.Subscribe<YouthProtectionMessage>(ShowAlert, MvxReference.Strong);

            var mock = new Mock<IDeviceUidService>();
            mock.Setup(s => s.GetDeviceUid(It.IsAny<Action<string>>(), It.IsAny<Action<Exception>>()))
                .Callback(
                    (Action<string> id, Action<Exception> error) =>
                    {
                        onDeviceUidSuccess = id;
                        onDeviceUidError = error;
                    });

            var service = new LbkMobileService(mock.Object);

            var viewModel = new HomeViewModel(service);
            onDeviceUidSuccess(Constants.DeviceUidTest);

           viewModel.StartCommand.Execute(null);
        }

        private void ShowAlert(YouthProtectionMessage message)
        {
            var result = MessageBox.Show("Is Ok", "Caption", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                message.Result = Model.Enums.DialogResult.Ok;
            }
           
        }

        protected override void AdditionalSetup()
        {
            this.Ioc.RegisterType<IMvxReachability, MvxTestReachability>();
            this.Ioc.RegisterSingleton<IMvxMessenger>( new MvxMessengerHub());

            var userSettings = this.Ioc.IoCConstruct<UserSettings>();
            this.Ioc.RegisterSingleton<ISettings>(userSettings);
            
        }
    }
}