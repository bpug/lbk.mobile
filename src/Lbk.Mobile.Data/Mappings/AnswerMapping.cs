//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="AnswerMapping.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Mappings
{
    using System.Collections.Generic;
    using System.Linq;

    using Lbk.Mobile.Model;

    public static class AnswerMapping
    {
        public static List<Answer> ToModel(this IEnumerable<LbkMobileService.Answer> sourceList)
        {
            return sourceList.Select(p => p.ToModel()).ToList();
        }

        public static Answer ToModel(this LbkMobileService.Answer source)
        {
            var answer = new Answer()
            {
                Text = source.Description,
                Correct = source.IsRight,
                Explanation = source.Explanation
            };

            return answer;
        }
    }
}