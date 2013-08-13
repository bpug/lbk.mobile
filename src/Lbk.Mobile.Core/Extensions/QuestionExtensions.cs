//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuestionExtensions.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Extensions
{
    using System.Linq;

    using Lbk.Mobile.Model;

    public static class QuestionExtensions
    {
        public static Answer GetRightAnswer(this Question source)
        {
            var rightAnswer = source.Answers.FirstOrDefault(p => p.Correct);
            return rightAnswer;
        }
    }
}