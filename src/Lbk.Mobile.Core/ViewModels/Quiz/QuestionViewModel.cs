//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuestionViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Quiz
{
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
                this.RaisePropertyChanged(() => this.Question);
            }
        }
    }
}