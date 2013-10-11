//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuizNotSuccessResultViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Quiz
{
    using Lbk.Mobile.Model;

    public class QuizNotSuccessResultViewModel : BaseViewModel
    {
        private QuizResult result;

        public QuizResult Result
        {
            get
            {
                return this.result;
            }
            set
            {
                this.result = value;
                this.RaisePropertyChanged(() => this.Result);
            }
        }

        public void Init(QuizResult quizResult)
        {
            this.Result = quizResult;
        }
    }
}