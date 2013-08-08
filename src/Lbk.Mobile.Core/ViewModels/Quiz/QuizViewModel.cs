//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuizViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Quiz
{
    using Lbk.Mobile.Data.Service.LbkMobileService;

    public class QuizViewModel : BaseViewModel
    {
        private int quiz;

        public int Quiz
        {
            get
            {
                return this.quiz;
            }
            set
            {
                this.quiz = value;
                this.RaisePropertyChanged(() => this.Quiz);
            }
        }

        public void Init(int quiz)
        {
            this.Quiz = quiz;
        }
    }
}