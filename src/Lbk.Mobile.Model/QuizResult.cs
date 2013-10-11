//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuizResult.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Model
{
    public class QuizResult
    {
        public int RightAnswerCount { get; set; }
        public int RightPoints { get; set; }
        public int TotalPoints { get; set; }
        public int TotalQuestionCount { get; set; }
        public int QuizId { get; set; }
        public bool Success
        {
            get
            {
                return TotalQuestionCount == RightAnswerCount;
            }
        }

    }
}