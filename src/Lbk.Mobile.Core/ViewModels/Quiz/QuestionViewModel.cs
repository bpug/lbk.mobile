//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuestionViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Quiz
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Common.Extensions;
    using Lbk.Mobile.Core.Extensions;
    using Lbk.Mobile.Model;

    public class QuestionViewModel : BaseViewModel
    {
        private Question question;

        public QuestionViewModel(Question question)
        {
            this.Question = question;
        }

        public QuestionViewModel()
        {
        }

        public event EventHandler QuestionAnswered;

        public ICommand AnsweredCommand
        {
            get
            {
                return new MvxCommand<Answer>(this.AnsweredCommandExecute);
            }
        }

        public List<Answer> Answers { get; set; }

        public Question Question
        {
            get
            {
                return this.question;
            }
            set
            {
                this.question = value;
                if (this.question != null)
                {
                    this.Answers = this.question.Answers;
                }
                this.RaisePropertyChanged(() => this.Question);
            }
        }

        private void AnsweredCommandExecute(Answer answer)
        {
            this.Question.IsRight = answer.Correct;
            string explanation = this.Question.GetRightAnswer().Explanation;
            string isCorrect = answer.Correct ? this.GetText("Right") : this.GetText("Wrong");

            this.MessageBoxService.Alert(
                explanation,
                isCorrect,
                this.GetText("Next"),
                () => { this.QuestionAnswered.RaiseEvent(this, EventArgs.Empty); });
        }
    }
}