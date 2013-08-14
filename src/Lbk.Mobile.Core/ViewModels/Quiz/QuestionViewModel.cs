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
    using Lbk.Mobile.Model;

    public class QuestionViewModel : BaseViewModel
    {
        private List<Answer> answers;

        private Question question;

        public event EventHandler QuestionAnswered;

        public QuestionViewModel(Question question)
        {
            this.Question = question;
        }

        public ICommand AnsweredCommand
        {
            get
            {
                return new MvxCommand<Answer>(this.AnsweredCommandExecute);
            }
        }

        public List<Answer> Answers
        {
            get
            {
                return this.answers;
            }
            set
            {
                this.answers = value;
                this.RaisePropertyChanged(() => this.Answers);
            }
        }

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
            string title = answer.Correct ? this.TextSource.GetText("Right") : this.TextSource.GetText("Wrong");

            this.MessageBoxService.Show(
                title,
                answer.Explanation,
                this.TextSource.GetText("Next"),
                () => { this.QuestionAnswered.RaiseEvent(this, EventArgs.Empty); });
        }
    }
}