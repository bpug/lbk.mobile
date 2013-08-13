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
    using Lbk.Mobile.Core.Extensions;
    using Lbk.Mobile.Data.Extensions;
    using Lbk.Mobile.Data.Mappings;
    using Lbk.Mobile.Data.Service;
    using Lbk.Mobile.Model;

    public class QuizViewModel : BaseViewModel
    {
        private const int QuestionCount = 10;

        private readonly ILbkMobileService service;

        private int currentPoints;

        private Question currentQuestion;

        private int currentQuestionNumber;

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
            this.LoadCommand.Execute(null);
        }

        private void AbortCommandExecute()
        {
            if (this.AbortQuizQuestion != null)
            {
                this.AbortQuizQuestion(
                    this,
                    new NotificationEventArgs<string, bool>(
                        this.TextSource.GetText("AbortQuizQuestion"),
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
                    this.Quiz = q.ToModel();

                    this.CurrentQuestionNumber = 1;
                    this.CurrentQuestion = this.Quiz.GetNextQuestion();
                    this.TotalQuestionCount = this.Quiz.GetTotalQuestionsCount();
                    this.TotalPoints = this.Quiz.GetTotalPoints();
                   
                });
        }
    }
}