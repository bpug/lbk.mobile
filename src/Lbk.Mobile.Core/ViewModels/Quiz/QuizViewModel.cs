//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuizViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Quiz
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Common;
    using Lbk.Mobile.Common.Utils;
    using Lbk.Mobile.Core.Extensions;
    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Data.Services;
    using Lbk.Mobile.Model;

    public class QuizViewModel : BaseViewModel
    {
        private const int QuestionCount = 2;

        private readonly ILbkMobileService service;

        

        private int currentPoints;

        private Question currentQuestion;

        private int currentQuestionNumber;

        private QuestionViewModel questionViewModel;

        private List<Question> questions;

        private Quiz quiz;

        private int rightAnswerCount;

        private int totalPoints;

        private int totalQuestionCount;

        public QuizViewModel(ILbkMobileService service)
        {
            this.service = service;
            
        }

        public event EventHandler<NotificationEventArgs<string, bool>> AbortQuizQuestion;

        public ICommand AbortCommand
        {
            get
            {
                return new MvxCommand(this.AbortCommandExecute);
            }
        }

        public int CurrentPoints
        {
            get
            {
                return this.currentPoints;
            }
            set
            {
                this.currentPoints = value;
                this.RaisePropertyChanged(() => this.CurrentPoints);
                this.RaisePropertyChanged(() => this.CurrentPointsText);
            }
        }

        public string CurrentPointsText
        {
            get
            {
                return this.GetText("CurrentPoints", this.CurrentPoints, this.TotalPoints);
            }
        }

        public Question CurrentQuestion
        {
            get
            {
                return this.currentQuestion;
            }
            set
            {
                this.currentQuestion = value;
                this.CurrentPoints = this.Quiz.GetRightPoints();
                this.RightAnswerCount = this.Quiz.GetRightAnswerCount();
                this.QuestionViewModel.Question = this.currentQuestion;
                this.RaisePropertyChanged(() => this.CurrentQuestion);
            }
        }

        public int CurrentQuestionNumber
        {
            get
            {
                return this.currentQuestionNumber;
            }
            set
            {
                this.currentQuestionNumber = value;
                this.RaisePropertyChanged(() => this.CurrentQuestionNumber);
                this.RaisePropertyChanged(() => this.CurrentQuestionNumberText);
            }
        }

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

        public QuestionViewModel QuestionViewModel
        {
            get
            {
                return this.questionViewModel;
            }
            set
            {
                this.questionViewModel = value;
                this.RaisePropertyChanged(() => this.QuestionViewModel);
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

        public int RightAnswerCount
        {
            get
            {
                return this.rightAnswerCount;
            }
            set
            {
                this.rightAnswerCount = value;
                this.RaisePropertyChanged(() => this.RightAnswerCount);
            }
        }

        public int TotalPoints
        {
            get
            {
                return this.totalPoints;
            }
            set
            {
                this.totalPoints = value;
                this.RaisePropertyChanged(() => this.TotalPoints);
            }
        }

        public int TotalQuestionCount
        {
            get
            {
                return this.totalQuestionCount;
            }
            set
            {
                this.totalQuestionCount = value;
                this.RaisePropertyChanged(() => this.TotalQuestionCount);
            }
        }

        public void Init()
        {
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
            if (this.AbortQuizQuestion != null)
            {
                this.AbortQuizQuestion(
                    this,
                    new NotificationEventArgs<string, bool>(
                        this.GetText("AbortQuizQuestion"),
                        string.Empty,
                        result =>
                        {
                            if (result)
                            {
                                this.ShowViewModel<QuizStartViewModel>();
                            }
                        }));
            }
        }

        private async Task LoadExecute()
        {
            await this.AsyncExecute(
                () => this.service.GetQuizAsync(QuestionCount),
                q =>
                {
                    this.Quiz = q;
                    this.CurrentQuestionNumber = 1;
                    this.CurrentQuestion = this.Quiz.GetNextQuestion();
                    this.TotalQuestionCount = this.Quiz.GetTotalQuestionsCount();
                    this.TotalPoints = this.Quiz.GetTotalPoints();
                });
        }

        private void OnQuestionAnswered(object sender, EventArgs eventArgs)
        {
            var qv = sender as QuestionViewModel;
            if (qv == null)
            {
                return;
            }

            this.CurrentQuestionNumber++;
            //this.TotalQuestionCount = this.Quiz.GetTotalQuestionsCount();

            var nextQuestion = this.Quiz.GetNextQuestion();
            if (nextQuestion != null)
            {
                //TODO: In View Change Question-Fagment
                this.CurrentQuestion = nextQuestion;
            }
            else
            {
                var result = this.ToQuizResult();
                
                if (this.Quiz.IsRightAnswered())
                {
                    
                    // http://stackoverflow.com/questions/16524236/custom-types-in-navigation-parameters-in-v3/16540174#16540174
                    // http://stackoverflow.com/questions/19058173/passing-complex-navigation-parameters-with-mvvmcross-showviewmodel/19059938#19059938
                    var bundle = new MvxBundle();
                    bundle.Write(result);
                    //bundle.Write(voucher);
                    this.ShowViewModel<QuizSuccessResultViewModel>(result);
                }
                else
                {
                    this.ShowViewModel<QuizNotSuccessResultViewModel>(result);
                }
                
            }
        }

        
    }
}