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
        public string MissingPointsMessage { get; set; }

        public QuizResult Result { get; set; }

        public string ResultPointsMessage { get; set; }

        public void Init(QuizResult quizResult)
        {
            this.Result = quizResult;
        }

        public override void Start()
        {
            base.Start();
            this.InitMessages();
        }

        private void InitMessages()
        {
            this.ResultPointsMessage = this.GetSharedText(
                "QuizResultPoints",
                this.Result.RightPoints,
                this.Result.TotalPoints);

            int missingPoints = this.Result.TotalQuestionCount - this.Result.RightAnswerCount;
            this.MissingPointsMessage = this.GetSharedText(
                missingPoints > 1 ? "QuizMissingPoints" : "QuizMissingPoint",
                missingPoints);
        }
    }
}