//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuizVoucherDataService.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Cirrious.MvvmCross.Plugins.Sqlite;

    using Lbk.Mobile.Model;

    public class QuizVoucherRepository : RepositoryBase, IQuizVoucherRepository
    {
        public QuizVoucherRepository(ISQLiteConnectionFactory factory)
            : base(factory)
        {
            this.Connection.CreateTable<QuizVoucher>();
        }

        public int Count()
        {
            return this.Connection.Table<QuizVoucher>().Count();
        }

        public QuizVoucher Get(int id)
        {
            var result = this.Connection.Table<QuizVoucher>().FirstOrDefault(p => p.Id == id);
            return result;
        }

        public IEnumerable<QuizVoucher> GetAll()
        {
            return this.Connection.Table<QuizVoucher>().Where(p => (p.Deleted == false));
        }

        public QuizVoucher GetForQuiz(int quizId)
        {
            return this.Connection.Table<QuizVoucher>().FirstOrDefault(p => (p.Deleted == false && p.QuizId == quizId));
        }

        public IEnumerable<QuizVoucher> GetNotUsed()
        {
            return this.Connection.Table<QuizVoucher>().Where(p => (p.Deleted == false && p.IsUsed == false));
        }

        public void Update(QuizVoucher voucher)
        {
            voucher.ModifyAt = DateTime.Now;
            if (voucher.Id == 0)
            {
                voucher.CreateAt = DateTime.Now;
                this.Connection.Insert(voucher);
            }
            else
            {
                this.Connection.Update(voucher);
            }
        }
    }
}