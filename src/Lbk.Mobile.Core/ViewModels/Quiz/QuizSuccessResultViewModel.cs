//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuizSuccessResultViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Quiz
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Common.Utils;
    using Lbk.Mobile.Core.ViewModels.Helpers;
    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Data.Services;
    using Lbk.Mobile.Model;

    public class QuizSuccessResultViewModel : BaseViewModel
    {
        private readonly IQuizVoucherRepository voucherRepository;
        private readonly ILbkMobileService service;

        private QuizResult result;

        private QuizVoucher voucher;

        public QuizSuccessResultViewModel(ILbkMobileService service, IQuizVoucherRepository voucherRepository)
        {
            this.voucherRepository = voucherRepository;
            this.service = service;
        }

        public QuizResult Result
        {
            get
            {
                return this.result;
            }
            set
            {
                this.result = value;
                this.RaisePropertyChanged(() => this.Result);
            }
        }

        public QuizVoucher Voucher
        {
            get
            {
                return this.voucher;
            }
            set
            {
                this.voucher = value;
                this.RaisePropertyChanged(() => this.Voucher);
            }
        }

        public void Init(QuizResult quizResult)
        {
            this.Result = quizResult;
        }

        public override void Start()
        {
            base.Start();

            Voucher = this.GetNewVoucher();
            if (!Voucher.IsActivated)
            {
                
            }
        }

        private QuizVoucher GetNewVoucher()
        {
            var savedVoucher = voucherRepository.GetForQuiz(this.Result.QuizId);
            if (savedVoucher == null)
            {
                var newVoucher = new QuizVoucher
                {
                    Code = Utility.GetRandomString(5),
                    QuizId = this.Result.QuizId
                };
                voucherRepository.Update(newVoucher);

                return newVoucher;
            }
            return savedVoucher;
        }

        private async Task ActivateVoucher(QuizVoucher quizVoucher)
        {
            await this.AsyncExecute(() => this.service.ActivateVoucherAsync(quizVoucher), OnSuccessActivate, this.OnLoadErrorActivate);
        }

        private void OnSuccessActivate(bool b)
        {
            throw new NotImplementedException();
        }

        private void OnLoadErrorActivate(Exception obj)
        {
            throw new NotImplementedException();
        }
    }
}