//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Question.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Model
{
    using System.Collections.Generic;

    using Lbk.Mobile.Model.Enums;

    public partial class Question
    {
        public List<Answer> Answers { get; set; }

        public QuestionCategory Category { set; get; }

        public bool? IsRight { set; get; }

        public int Points { get; set; }

        public string Text { get; set; }
    }
}