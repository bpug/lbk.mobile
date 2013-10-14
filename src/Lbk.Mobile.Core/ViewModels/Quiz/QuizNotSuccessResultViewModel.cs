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
       public QuizResult Result { get; set; }

        public void Init(QuizResult quizResult)
        {
            this.Result = quizResult;
        }
    }
}