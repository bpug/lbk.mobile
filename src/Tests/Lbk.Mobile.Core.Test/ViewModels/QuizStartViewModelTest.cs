//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuizStartViewModelTest.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Test.ViewModels
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Cirrious.MvvmCross.Localization;
    using Cirrious.MvvmCross.Plugins.Messenger;
    using Lbk.Mobile.Common;
    using Lbk.Mobile.Core.Messages;
    using Lbk.Mobile.Core.Services;
    using Lbk.Mobile.Core.Test.Implementation;
    using Lbk.Mobile.Core.ViewModels;
    using Lbk.Mobile.Core.ViewModels.Quiz;
    using Lbk.Mobile.Data.LbkMobileService;
    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Localization;
    using Lbk.Mobile.Plugin.Reachability;
    using Lbk.Mobile.Plugin.Settings;
    using Lbk.Mobile.Plugin.UserInteraction;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class QuizStartViewModelTest : TestBase
    {
        [Test]
        public void GetQuiz()
        {
            // Need for viewModel.PropertyChanged
            this.CreateMockDispatcher();
            var mockService = this.CreateMockLbkMobileService();

            var result = this.GetQuizData();

            var tcs = new TaskCompletionSource<Quiz>();
            tcs.SetResult(result);
            mockService.Setup(s => s.GetQuizAsync(It.IsAny<int>())).Returns(tcs.Task);

            var viewModel = new QuizViewModel(mockService.Object);
            viewModel.PropertyChanged += (sender, args) =>
            {
                var vm = (QuizViewModel)sender;
                switch (args.PropertyName)
                {
                    case "Quiz":
                        Assert.IsNotNull(vm.Quiz);
                        Assert.AreEqual(2, vm.Questions.Count);
                        break;
                }
            };

            viewModel.Init();

            mockService.Verify(quiz => quiz.GetQuizAsync(It.IsAny<int>()), Times.Once());
            //Assert.IsNotNull(viewModel.Quiz);
            //Assert.AreEqual(2, viewModel.Questions.Count);
        }

        [Test]
        public void Instructions()
        {
            var mockNavigation = this.CreateMockDispatcher();

            var mockDataService = new Mock<IQuizVoucherRepository>();

            var viewModel = new QuizStartViewModel(mockDataService.Object);

            // Test InstructionsCommand
            viewModel.InstructionsCommand.Execute(null);
            Assert.AreEqual(1, mockNavigation.Requests.Count);
            var request = mockNavigation.Requests[0];
            Assert.AreEqual(typeof(InstructionsViewModel), request.ViewModelType);
        }

        //[Test]
        //public async void YouthProtectionMesenger()
        //{
        //    var mockNavigation = this.CreateMockNavigation();
        //    var mvxMessenger = this.Ioc.Resolve<IMvxMessenger>();
        //    Settings.YouthProtection = false;

        //    mvxMessenger.Subscribe<YouthProtectionMessage>(this.ShowAlert, MvxReference.Strong);

        //    var viewModel = new QuizStartViewModel();

        //    viewModel.StartMessengerCommand.Execute(null);
        //}

        [Test]
        public void YouthProtectionNo()
        {
            var mockNavigation = this.CreateMockDispatcher();

            Settings.YouthProtection = false;

            var mockDataService = new Mock<IQuizVoucherRepository>();

            var viewModel = new QuizStartViewModel(mockDataService.Object);

            viewModel.YouthProtectionQuestion += (sender, args) => args.Completed(false);

            viewModel.StartCommand.Execute(null);

            Assert.IsFalse(Settings.YouthProtection);

            Assert.AreEqual(0, mockNavigation.Requests.Count);
        }

        [Test]
        public void YouthProtectionYes()
        {
            var mockNavigation = this.CreateMockDispatcher();

            Settings.YouthProtection = false;

            var mockDataService = new Mock<IQuizVoucherRepository>();

            var viewModel = new QuizStartViewModel(mockDataService.Object);
            viewModel.YouthProtectionQuestion += (sender, args) => args.Completed(true);
            viewModel.StartCommand.Execute(null);

            Assert.IsTrue(Settings.YouthProtection);
            Assert.AreEqual(1, mockNavigation.Requests.Count);

            var request = mockNavigation.Requests.First();
            Assert.AreEqual(typeof(QuizViewModel), request.ViewModelType);
        }

        protected override void AdditionalSetup()
        {
            this.Ioc.RegisterType<IReachability, MvxTestReachability>();
            this.Ioc.RegisterSingleton<IMvxMessenger>(new MvxMessengerHub());

            var userSettings = this.Ioc.IoCConstruct<UserSettings>();
            this.Ioc.RegisterSingleton<ISettings>(userSettings);

            this.Ioc.RegisterSingleton<IMvxTextProvider>(new ResxTextProvider(Strings.ResourceManager));

            this.Ioc.RegisterSingleton<IMessageBoxService>(new MessageBoxService());
        }

        private Quiz GetQuizData()
        {
            var result = new Quiz
            {
                Questions = new Question[2]
                {
                    new Question
                    {
                        Description = "Q1"
                    },
                    new Question
                    {
                        Description = "Q2"
                    }
                }
            };
            return result;
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