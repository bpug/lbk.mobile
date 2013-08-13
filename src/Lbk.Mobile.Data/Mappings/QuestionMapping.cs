//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuestionMapping.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Mappings
{
    using System.Collections.Generic;
    using System.Linq;

    using Lbk.Mobile.Common.Extensions;
    using Lbk.Mobile.Model;
    using Lbk.Mobile.Model.Enums;

    public static class QuestionMapping
    {
        public static Question ToModel(this LbkMobileService.Question source)
        {
            var question = new Question()
            {
                Text = source.Description,
                Category = (QuestionCategory)source.Category,
                Points = source.Points
            };

            if (!source.Answers.IsNullOrEmpty())
            {
                question.Answers = source.Answers.ToLbkAnswer();
            }

            return question;
        }

        public static List<Question> ToModel(this IEnumerable<LbkMobileService.Question> sourceList)
        {
            return sourceList.Select(p => p.ToModel()).ToList();
        }
    }
}