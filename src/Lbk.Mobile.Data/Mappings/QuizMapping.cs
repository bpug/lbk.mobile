//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuizMapping.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Mappings
{
    using Lbk.Mobile.Common.Extensions;
    using Lbk.Mobile.Model;

    public static class QuizMapping
    {
        public static Quiz ToModel(this LbkMobileService.Quiz source)
        {
            var quiz = new Quiz
            {
                PointsProAnswer = source.PointsProAnswer,
                Id = source.Id
            };

            if (!source.Questions.IsNullOrEmpty())
            {
                quiz.Questions = source.Questions.ToModel();
            }

            return quiz;
        }
    }
}