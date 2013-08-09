//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="HomeViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Quiz
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Core.Messages;
    using Lbk.Mobile.Data.LbkMobileService;
    using Lbk.Mobile.Data.Service;
    using Lbk.Mobile.Model.Enums;

    public class HomeViewModel : BaseViewModel
    {
        private const int QuestionCount = 10;

        private readonly ILbkMobileService service;

        private Quiz quiz;

        public HomeViewModel(ILbkMobileService service)
        {
            this.service = service;
        }

        public ICommand LoadCommand
        {
            get
            {
                return new MvxCommand(async () => await this.OnLoadExecute());
            }
        }

        public ICommand HelpCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<InstructionsViewModel>());
            }
        }

        public Quiz Quiz
        {
            get
            {
                return this.quiz;
            }
            set
            {
                this.quiz = value;
                this.RaisePropertyChanged(() => this.Quiz);
            }
        }

        public ICommand StartCommand
        {
            get
            {
                return new MvxCommand(this.StartExecute);
            }
        }

        private async Task OnLoadExecute()
        {
            await this.AsyncExecute(() => this.service.GetQuizAsync(QuestionCount),
                list =>
                {
                    this.Quiz = list;
                    //this.ShowViewModel<QuizViewModel>(new { quiz = list });
                });
        }

        private void StartExecute()
        {
            if (!Settings.YouthProtection)
            {
                var message = new YouthProtectionMessage(this);
                this.MvxMessenger.Publish(message);
                if (message.Result == DialogResult.Ok)
                {
                    Settings.YouthProtection = true;
                    this.LoadCommand.Execute(null);
                }
            }
            else
            {
                this.LoadCommand.Execute(null);
            }
        }
    }
}