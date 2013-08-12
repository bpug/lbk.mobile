//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuizViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Quiz
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Data.LbkMobileService;
    using Lbk.Mobile.Data.Service;

    public class QuizViewModel : BaseViewModel
    {
        private const int QuestionCount = 10;

        private readonly ILbkMobileService service;

        private List<Question> questions;

        private Quiz quiz;

        public QuizViewModel(ILbkMobileService service)
        {
            this.service = service;
        }

        public ICommand AbortCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<QuizStartViewModel>());
            }
        }

        public ICommand LoadCommand
        {
            get
            {
                return new MvxCommand(async () => await this.LoadExecute());
            }
        }

        public List<Question> Questions
        {
            get
            {
                return this.questions;
            }
            set
            {
                this.questions = value;
                this.RaisePropertyChanged(() => this.Questions);
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
                if (this.quiz != null && this.quiz.Questions != null)
                {
                    this.Questions = this.quiz.Questions.ToList();
                }
                this.RaisePropertyChanged(() => this.Quiz);
            }
        }

        public void Init()
        {
            this.LoadCommand.Execute(null);
        }

        private async Task LoadExecute()
        {
            await this.AsyncExecute(() => this.service.GetQuizAsync(QuestionCount), list => { this.Quiz = list; });
        }
    }
}