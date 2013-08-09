//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuizStartViewModelTest.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Test.ViewModels
{
    using System.Windows.Forms;

    using Cirrious.MvvmCross.Localization;
    using Cirrious.MvvmCross.Plugins.Messenger;
    using Cirrious.MvvmCross.Plugins.Network.Reachability;

    using Lbk.Mobile.Common;
    using Lbk.Mobile.Core.Messages;
    using Lbk.Mobile.Core.Services;
    using Lbk.Mobile.Core.Test.Implementation;
    using Lbk.Mobile.Core.ViewModels;
    using Lbk.Mobile.Core.ViewModels.Quiz;
    using Lbk.Mobile.Data.Service;
    using Lbk.Mobile.Localization;
    using Lbk.Mobile.Plugin.Settings;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class QuizStartViewModelTest : TestBase
    {
        [Test]
        public async void GetQuiz()
        {
            //this.InitLbkMobileService();
            //var service = this.Ioc.Resolve<ILbkMobileService>();

            var service = this.CreateMockMobileService();

            var viewModel = new QuizViewModel(service.Object);
            this.OnDeviceUidSuccess(Constants.DeviceUidTest);

            viewModel.LoadCommand.Execute(null);
            
            //service.Verify(quiz => quiz.GetQuizAsync(10), Times.Once());
        }

        [Test]
        public async void YouthProtectionEvent()
        {
            var mockNavigation = this.CreateMockNavigation();

            Settings.YouthProtection = false;

            var viewModel = new QuizStartViewModel();

            viewModel.YouthProtectionQuestion += this.ViewModelOnYouthProtectionHandler;

            viewModel.StartCommand.Execute(null);
        }

        [Test]
        public async void YouthProtectionMesenger()
        {
            var mockNavigation = this.CreateMockNavigation();
            var mvxMessenger = this.Ioc.Resolve<IMvxMessenger>();
            Settings.YouthProtection = false;

            mvxMessenger.Subscribe<YouthProtectionMessage>(this.ShowAlert, MvxReference.Strong);

            var viewModel = new QuizStartViewModel();

            viewModel.StartMessengerCommand.Execute(null);
        }

        protected override void AdditionalSetup()
        {
            this.Ioc.RegisterType<IMvxReachability, MvxTestReachability>();
            this.Ioc.RegisterSingleton<IMvxMessenger>(new MvxMessengerHub());

            var userSettings = this.Ioc.IoCConstruct<UserSettings>();
            this.Ioc.RegisterSingleton<ISettings>(userSettings);

            this.Ioc.RegisterSingleton<IMvxTextProvider>(new ResxTextProvider(Strings.ResourceManager));
        }

        private void ShowAlert(YouthProtectionMessage message)
        {
            var result = MessageBox.Show("Is Ok", "Caption", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                message.Result = Model.Enums.DialogResult.Ok;
            }
        }

        private void ViewModelOnYouthProtectionHandler(object sender, NotificationEventArgs<string, bool> e)
        {
            var dialogResult = MessageBox.Show(e.Message, e.Data, MessageBoxButtons.OKCancel);
            e.Completed(dialogResult == DialogResult.OK);
        }
    }
}