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

        public QuizSuccessResultViewModel(ILbkMobileService service, IQuizVoucherRepository voucherRepository)
        {
            this.voucherRepository = voucherRepository;
            this.service = service;
        }

        public QuizResult Result { get; set; }

        public QuizVoucher Voucher { get; set; }

        public void Init(QuizResult quizResult)
        {
            this.Result = quizResult;
        }

        public override  void Start()
        {
            base.Start();
            CheckVoucher();
        }

        private async void CheckVoucher()
        {
             var newVoucher = this.GetNewVoucher();
            if (newVoucher != null)
            {
               await ActivateVoucher(newVoucher);
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
            return null;
        }

        private async Task ActivateVoucher(QuizVoucher quizVoucher)
        {
            await this.AsyncExecute(() => this.service.ActivateVoucherAsync(quizVoucher),
                isActivated =>
                {
                    if (isActivated)
                    {
                        quizVoucher.IsActivated = true;
                        voucherRepository.Update(quizVoucher);
                        this.Voucher = quizVoucher;
                    }
                    else
                    {
                        //Wurde schon aufgelöst ????
                        //???? - Copy from Touch logik
                        quizVoucher.IsUsed = true;
                        quizVoucher.Deleted = true;
                        voucherRepository.Update(quizVoucher);
                    }
                }, this.OnLoadErrorActivate);
        }

        private void OnLoadErrorActivate(Exception obj)
        {
            
        }
    }
}