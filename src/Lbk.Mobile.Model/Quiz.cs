//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Quiz.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Model
{
    using System.Collections.Generic;

    public partial class Quiz
    {
        public int Id { get; set; }

        public int PointsProAnswer { get; set; }

        public List<Question> Questions { get; set; }
    }
}