﻿//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IQuizVoucherDataService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Service.Interfaces
{
    using System.Collections.Generic;

    using Lbk.Mobile.Model;

    public interface IQuizVoucherDataService
    {
        int Count();

        QuizVoucher Get(int id);

        IEnumerable<QuizVoucher> GetAll();

        QuizVoucher GetForQuiz(int quizId);

        IEnumerable<QuizVoucher> GetNotUsed();

        void Update(QuizVoucher voucher);
    }
}