//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuestionViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Quiz
{
    using System.Collections.Generic;
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Common.Extensions;
    using Lbk.Mobile.Model;

    public class QuestionViewModel : BaseViewModel
    {
        private Question question;

        public Question Question
        {
            get
            {
                return this.question;
            }
            set
            {
                this.question = value;
                if (question!= null)
                {
                    this.Answers = question.Answers;
                }
                this.RaisePropertyChanged(() => this.Question);
            }
        }


        private List<Answer> answers;

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

        public ICommand CheckAnswerCommand
        {
            get
            {
                return new MvxCommand<Answer>(CheckAnswerCommandExecute);
            }
        }

        private void CheckAnswerCommandExecute(Answer answer)
        {
              

        }
    }
}