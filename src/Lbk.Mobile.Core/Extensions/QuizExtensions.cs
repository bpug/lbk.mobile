//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuizExtensions.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Extensions
{
    using System.Linq;

    using Lbk.Mobile.Model;

    public static class QuizExtensions
    {
        public static Question GetNextQuestion(this Quiz source)
        {
            return source.Questions.FirstOrDefault(p => p.IsRight == null);
        }

        public static int GetRightAnswerCount(this Quiz source)
        {
            int rightCount = source.Questions.Count(p => p.IsRight == true);

            return rightCount;
        }

        public static int GetRightPoints(this Quiz source)
        {
            int rightPoints = source.Questions.Where(p => p.IsRight == true).Sum(p => p.Points);

            return rightPoints;
        }

        public static int GetTotalPoints(this Quiz source)
        {
            return source.Questions.Sum(p => p.Points);
        }

        public static int GetTotalQuestionsCount(this Quiz source)
        {
            return source.Questions.Count;
        }

        public static bool IsRightAnswered(this Quiz source)
        {
            int answerCount = source.Questions.Count(p => (p.IsRight == false || p.IsRight == null));

            return answerCount == 0;
        }

        /*
        public static int GetTotalPoints (this Quiz source)
        {
            return  source.Questions.Count * source.PointsProAnswer;
			
        }
		
        public static int GetRightPoints (this Quiz source)
        {
            int rightCount = source.GetRightAnswerCount ();
			
            return rightCount * source.PointsProAnswer;
        }
		
        */
    }
}