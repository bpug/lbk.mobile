//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuizVoucher.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Model
{
    public class QuizVoucher : BaseDbEntity
    {
        public string Code { get; set; }
        public bool IsActivated { get; set; }
        public bool IsUsed { get; set; }
        public decimal Price { get; set; }
        public int QuizId { get; set; }
    }
}