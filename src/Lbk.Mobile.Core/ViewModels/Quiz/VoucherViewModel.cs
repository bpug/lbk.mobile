//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="VoucherViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Quiz
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Common;
    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Data.Services;
    using Lbk.Mobile.Model;

    public class VoucherViewModel : BaseViewModel
    {
        private readonly ILbkMobileService lbkMobileservice;

        private readonly IQuizVoucherRepository voucherRepository;

        public VoucherViewModel(ILbkMobileService lbkMobileservice, IQuizVoucherRepository voucherRepository)
        {
            this.lbkMobileservice = lbkMobileservice;
            this.voucherRepository = voucherRepository;
        }

        public event EventHandler<NotificationEventArgs<string, bool>> ActivateVoucher;
        public event EventHandler<NotificationEventArgs<string, bool>> UseVoucher;

        public ICommand ActivateVoucherCommand
        {
            get
            {
                return new MvxCommand<QuizVoucher>(this.ActivateVoucherExecute);
            }
        }

        public ICommand UseVoucherCommand
        {
            get
            {
                return new MvxCommand<QuizVoucher>(this.UseVoucherExecute);
            }
        }

        public List<QuizVoucher> Vouchers { get; set; }

        public void Init()
        {
            this.Load();
        }

        private void ActivateVoucherExecute(QuizVoucher voucher)
        {
            if (this.ActivateVoucher != null)
            {
                this.ActivateVoucher(
                    this,
                    new NotificationEventArgs<string, bool>(
                        string.Format(this.GetText("ActivateVoucherQuestion"), voucher.Code),
                        string.Empty,
                        isActivated =>
                        {
                            if (isActivated)
                            {
                                this.AsyncExecute(
                                    () => this.lbkMobileservice.ActivateVoucherAsync(voucher),
                                    serviceResult =>
                                    {
                                        if (serviceResult)
                                        {
                                            voucher.IsActivated = true;
                                            this.voucherRepository.Update(voucher);
                                            this.Load();
                                            // TODO: DialogService-Alert: string.Format(this.TextSource.GetText("VoucherActvated"), voucher.Code) 
                                            this.ShowAlert(this.GetText("VoucherActvated", voucher.Code), null);
                                        }
                                        else
                                        {
                                            voucher.IsUsed = true;
                                            voucher.Deleted = true;
                                            this.voucherRepository.Update(voucher);
                                            // TODO: DialogService-Alert: string.Format(this.TextSource.GetText("VoucherAlreadyActvated"), voucher.Code) 
                                            this.ShowAlert(this.GetText("VoucherAlreadyActvated", voucher.Code), null);
                                        }
                                    });
                            }
                        }));
            }
        }

        private void Load()
        {
            this.Vouchers = this.voucherRepository.GetNotUsed().ToList();
        }

        private void UseVoucherExecute(QuizVoucher voucher)
        {
            if (this.UseVoucher != null)
            {
                this.UseVoucher(
                    this,
                    new NotificationEventArgs<string, bool>(
                        this.GetText("UseVoucherQuestion"),
                        string.Empty,
                        result =>
                        {
                            if (result)
                            {
                                voucher.IsUsed = true;
                                this.voucherRepository.Update(voucher);
                                //Navigate to QuizStartViewModel ?
                                this.ShowViewModel<QuizStartViewModel>();
                            }
                        }));
            }
        }
    }
}