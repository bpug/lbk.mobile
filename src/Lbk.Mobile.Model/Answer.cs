//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Answer.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Model
{
    public class Answer
    {
        public bool Correct { get; set; }
        public string Explanation { get; set; }
        public string Text { get; set; }
        public int Number { get; set; }
    }
}