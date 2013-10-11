//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuizViewModelExtensions.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Extensions
{
    using Lbk.Mobile.Core.ViewModels.Quiz;
    using Lbk.Mobile.Model;

    public static class QuizViewModelExtensions
    {
        public static QuizResult ToQuizResult(this QuizViewModel source)
        {
            var result = new QuizResult
            {
                TotalPoints = source.TotalPoints,
                TotalQuestionCount = source.TotalQuestionCount,
                RightAnswerCount = source.RightAnswerCount,
                RightPoints = source.CurrentPoints,
                QuizId = source.Quiz.Id
            };
            return result;
        }
    }
}