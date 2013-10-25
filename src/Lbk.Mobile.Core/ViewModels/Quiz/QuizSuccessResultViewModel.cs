//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuizSuccessResultViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Quiz
{
    using System;
    using System.Threading.Tasks;

    using Lbk.Mobile.Common.Utils;
    using Lbk.Mobile.Core.Messages;
    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Data.Services;
    using Lbk.Mobile.Model;

    public class QuizSuccessResultViewModel : BaseViewModel
    {
        private readonly ILbkMobileService service;
        private readonly IQuizVoucherRepository voucherRepository;

        public QuizSuccessResultViewModel(ILbkMobileService service, IQuizVoucherRepository voucherRepository)
        {
            this.voucherRepository = voucherRepository;
            this.service = service;
        }

        public QuizResult Result { get; set; }
        public string ResultPointsMessage { get; set; }

        public QuizVoucher Voucher { get; set; }
        public string WinVoucherMessage { get; set; }

        public bool ShowPitcher { get; set; }

        public void Init(QuizResult quizResult)
        {
            this.Result = quizResult;
        }

        public override void Start()
        {
            base.Start();
            this.ResultPointsMessage = this.GetSharedText(
                "QuizResultPoints",
                this.Result.RightPoints,
                this.Result.TotalPoints);
            this.CheckVoucher();
        }


        private async Task ActivateVoucher(QuizVoucher quizVoucher)
        {
            await this.AsyncExecute(
                () => this.service.ActivateVoucherAsync(quizVoucher),
                isActivated =>
                {
                    if (isActivated)
                    {
                        quizVoucher.IsActivated = true;
                        this.voucherRepository.Update(quizVoucher);
                        this.Voucher = quizVoucher;
                        this.WinVoucherMessage = this.GetSharedText("QuizWinVoucher");
                        this.MvxMessenger.Publish(new VoucherActivatedMessage(this));
                    }
                    else
                    {
                        //Wurde schon aufgelöst ????
                        //???? - Copy from Touch logik
                        quizVoucher.IsUsed = true;
                        quizVoucher.Deleted = true;
                        this.voucherRepository.Update(quizVoucher);
                        this.WinVoucherMessage = this.GetSharedText("QuizAlreadyReceiveVoucher");
                    }
                    ShowPitcher = true;
                },
                this.OnLoadErrorActivate);
        }

        private async void CheckVoucher()
        {
            var newVoucher = this.GetNewVoucher();
            if (newVoucher != null)
            {
                await this.ActivateVoucher(newVoucher);
            }
            else
            {
                this.WinVoucherMessage = this.GetSharedText("QuizAlreadyReceiveVoucher");
                ShowPitcher = true;
            }
        }

        private QuizVoucher GetNewVoucher()
        {
            var savedVoucher = this.voucherRepository.GetForQuiz(this.Result.QuizId);
            if (savedVoucher == null)
            {
                var newVoucher = new QuizVoucher
                {
                    Code = Utility.GetRandomString(5),
                    QuizId = this.Result.QuizId
                };
                this.voucherRepository.Update(newVoucher);

                return newVoucher;
            }
            return null;
        }

        private void OnLoadErrorActivate(Exception obj)
        {
        }
    }
}