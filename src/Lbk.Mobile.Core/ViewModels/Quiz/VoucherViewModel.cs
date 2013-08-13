//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="HelpViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Quiz
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using System.Xml.Linq;

    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Common;
    using Lbk.Mobile.Data.Service;
    using Lbk.Mobile.Model;

    public class VoucherViewModel : BaseViewModel
    {
        public event EventHandler<NotificationEventArgs<string, bool>> UseVoucherQuestion;

        public event EventHandler<NotificationEventArgs<string, bool>> ActivateVoucherQuestion;

        private readonly IQuizVoucherDataService voucherDataService;
        private readonly ILbkMobileService lbkMobileservice;

        public VoucherViewModel(ILbkMobileService lbkMobileservice, IQuizVoucherDataService voucherDataService)
        {
            this.lbkMobileservice = lbkMobileservice;
            this.voucherDataService = voucherDataService;
        }

        public void Init()
        {
            this.Load();
        }

        private List<QuizVoucher> vouchers;
        public List<QuizVoucher> Vouchers
        {
            get
            {
                return this.vouchers;
            }
            set
            {
                this.vouchers = value;
                this.RaisePropertyChanged(() => this.Vouchers);
            }
        }

        public ICommand UseVoucherCommand
        {
            get
            {
                return new MvxCommand<QuizVoucher>(this.UseVoucherExecute);
            }
        }

        public ICommand ActivateVoucherCommand
        {
            get
            {
                return new MvxCommand<QuizVoucher>(this.ActivateVoucherExecute);
            }
        }


        private void ActivateVoucherExecute(QuizVoucher voucher)
        {
            if (this.ActivateVoucherQuestion != null)
            {
                this.ActivateVoucherQuestion(
                    this,
                    new NotificationEventArgs<string, bool>(
                        string.Format(this.TextSource.GetText("ActivateVoucherQuestion"), voucher.Code),
                        string.Empty,
                        isActivated =>
                        {
                            if (isActivated)
                            {
                                this.AsyncExecute( () => this.lbkMobileservice.ActivateVoucherAsync(voucher),
                                    serviceResult =>
                                    {
                                        if (serviceResult)
                                        {
                                            voucher.IsActivated = true;
                                            this.voucherDataService.Update(voucher);
                                            this.Load();
                                            // TODO: DialogService-Alert: string.Format(this.TextSource.GetText("VoucherActvated"), voucher.Code) 
                                        }
                                        else
                                        {
                                            voucher.IsUsed = true;
                                            voucher.Deleted = true;
                                            this.voucherDataService.Update(voucher);
                                            // TODO: DialogService-Alert: string.Format(this.TextSource.GetText("VoucherAlreadyActvated"), voucher.Code) 
                                        }
                                    });
                            }
                        }));
            }
        }

        private void UseVoucherExecute(QuizVoucher voucher)
        {
            if (this.UseVoucherQuestion != null)
            {
                this.UseVoucherQuestion(
                    this,
                    new NotificationEventArgs<string, bool>(
                        this.TextSource.GetText("UseVoucherQuestion"),
                        string.Empty,
                        result =>
                        {
                            if (result)
                            {
                                voucher.IsUsed = true;
                                this.voucherDataService.Update(voucher);
                                //Navigate to QuizStartViewModel ?
                                this.ShowViewModel<QuizStartViewModel>();
                            }
                        }));
            }
        }

        private void Load()
        {
            this.Vouchers =  this.voucherDataService.GetNotUsed().ToList();
        }
    }
}