//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuizViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Quiz
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Core.Extensions;
    using Lbk.Mobile.Data.Services;
    using Lbk.Mobile.Model;

    public class QuizViewModel : BaseViewModel
    {
        
        private readonly ILbkMobileService service;

        public QuizViewModel(ILbkMobileService service)
        {
            this.service = service;
        }

        public ICommand AbortCommand
        {
            get
            {
                return new MvxCommand(this.AbortCommandExecute);
            }
        }

        public int CurrentPoints { get; set; }

        public string CurrentPointsText
        {
            get
            {
                return this.GetText("CurrentPoints", this.CurrentPoints, this.TotalPoints);
            }
        }

        public Question CurrentQuestion { get; set; }

        public int CurrentQuestionNumber { get; set; }

        public string CurrentQuestionNumberText
        {
            get
            {
                return this.GetText("CurrentQuestionNumber", this.CurrentQuestionNumber, this.TotalQuestionCount);
            }
        }

        public ICommand LoadCommand
        {
            get
            {
                return new MvxCommand(async () => await this.LoadExecute());
            }
        }

        public QuestionViewModel QuestionViewModel { get; set; }

        public List<Question> Questions { get; set; }

        public Quiz Quiz { get; set; }

        public int RightAnswerCount { get; set; }

        public int TotalPoints { get; set; }

        public int TotalQuestionCount { get; set; }

        public void Init()
        {
            this.CurrentQuestionNumber = 0;
            this.QuestionViewModel = new QuestionViewModel();
            this.QuestionViewModel.QuestionAnswered += this.OnQuestionAnswered;
        }

        public override void Start()
        {
            base.Start();
            this.LoadCommand.Execute(null);
        }

        private void AbortCommandExecute()
        {
            this.MessageBoxService.Confirm(
                this.GetText("AbortQuizQuestion"),
                string.Empty,
                this.GetSharedText("ButtonYes"),
                this.GetSharedText("ButtonNo"),
                b =>
                {
                    if (b)
                    {
                        this.Close(this);
                    }
                });
        }

        private void CalculateQiuz()
        {
            this.CurrentPoints = this.Quiz.GetRightPoints();
            this.RightAnswerCount = this.Quiz.GetRightAnswerCount();
            this.TotalQuestionCount = this.Quiz.GetTotalQuestionsCount();
            this.TotalPoints = this.Quiz.GetTotalPoints();
        }

        private async Task LoadExecute()
        {
            await this.AsyncExecute(() => this.service.GetQuizAsync(Constants.UserSettings.QuizQuestionCount), this.SetCurrentQuiz);
        }

        private void OnQuestionAnswered(object sender, EventArgs eventArgs)
        {
            var qv = sender as QuestionViewModel;
            if (qv == null)
            {
                return;
            }

            var nextQuestion = this.Quiz.GetNextQuestion();
            this.CalculateQiuz();

            if (nextQuestion != null)
            {
                this.SetCurrentQuestion(nextQuestion);
            }
            else
            {
                var result = this.ToQuizResult();

                if (this.Quiz.IsRightAnswered())
                {
                    // http://stackoverflow.com/questions/16524236/custom-types-in-navigation-parameters-in-v3/16540174#16540174
                    // http://stackoverflow.com/questions/19058173/passing-complex-navigation-parameters-with-mvvmcross-showviewmodel/19059938#19059938
                    //var bundle = new MvxBundle();
                    //bundle.Write(result);
                    //bundle.Write(voucher);
                    this.ShowViewModel<QuizSuccessResultViewModel>(result);
                }
                else
                {
                    this.ShowViewModel<QuizNotSuccessResultViewModel>(result);
                }
            }
        }

        private void SetCurrentQuestion(Question current)
        {
            this.CurrentQuestionNumber++;
            this.CurrentQuestion = current;
            this.QuestionViewModel.Question = this.CurrentQuestion;
        }

        private void SetCurrentQuiz(Quiz quiz)
        {
            this.Quiz = quiz;
            if (quiz != null && quiz.Questions != null)
            {
                this.Questions = quiz.Questions;
            }
            this.CalculateQiuz();
            this.SetCurrentQuestion(quiz.GetNextQuestion());
        }
    }
}