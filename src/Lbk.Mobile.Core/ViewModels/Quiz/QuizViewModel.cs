//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuizViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Quiz
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Data.LbkMobileService;
    using Lbk.Mobile.Data.Service;

    public class QuizViewModel : BaseViewModel
    {
        private const int QuestionCount = 10;

        private readonly ILbkMobileService service;

        private Quiz quiz;

        public QuizViewModel(ILbkMobileService service)
        {
            this.service = service;
        }

        public ICommand LoadCommand
        {
            get
            {
                return new MvxCommand(async () => await this.LoadExecute());
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

        private async Task LoadExecute()
        {
            await this.AsyncExecute(() => this.service.GetQuizAsync(QuestionCount), list => { this.Quiz = list; });
        }
    }
}