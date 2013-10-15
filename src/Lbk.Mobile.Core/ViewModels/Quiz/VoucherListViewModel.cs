//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="VoucherListViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Quiz
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;

    using Cirrious.MvvmCross.Plugins.Messenger;
    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Core.Messages;
    using Lbk.Mobile.Core.ViewModels.Helpers;
    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Data.Services;
    using Lbk.Mobile.Model;

    public class VoucherListViewModel : BaseViewModel
    {
        private readonly ILbkMobileService lbkMobileservice;
        private readonly IQuizVoucherRepository voucherRepository;

        public VoucherListViewModel(ILbkMobileService lbkMobileservice, IQuizVoucherRepository voucherRepository)
        {
            this.lbkMobileservice = lbkMobileservice;
            this.voucherRepository = voucherRepository;
        }

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

        //public List<QuizVoucher> Vouchers { get; set; }
        public List<VoucherWithCommands> Vouchers { get; set; }

        public override void Start()
        {
            base.Start();
            this.Load();
        }

        private void ActivateVoucherExecute(QuizVoucher voucher)
        {
            this.MessageBoxService.Confirm(
                this.GetText("ActivateVoucherQuestion", voucher.Code),
                string.Empty,
                this.GetSharedText("ButtonYes"),
                this.GetSharedText("ButtonNo"),
                async result =>
                {
                    if (result)
                    {
                        await
                            this.AsyncExecute(
                                () => this.lbkMobileservice.ActivateVoucherAsync(voucher),
                                serviceResult =>
                                {
                                    if (serviceResult)
                                    {
                                        voucher.IsActivated = true;
                                        this.voucherRepository.Update(voucher);
                                        this.Load();
                                        this.ShowAlert(this.GetText("VoucherActvated", voucher.Code), null);
                                    }
                                    else
                                    {
                                        voucher.IsUsed = true;
                                        voucher.Deleted = true;
                                        this.voucherRepository.Update(voucher);
                                        this.ShowAlert(this.GetText("VoucherAlreadyActvated", voucher.Code), null);
                                    }
                                });
                    }
                });
        }

        private void Load()
        {
            //this.Vouchers = this.voucherRepository.GetNotUsed().ToList();
            var notUsed = this.voucherRepository.GetNotUsed();
            var wrapped =
                notUsed.Select(
                    x =>
                        new VoucherWithCommands(
                            x,
                            new MvxCommand(() => this.ActivateVoucherExecute(x)),
                            new MvxCommand(() => this.UseVoucherExecute(x)))).ToList();
            this.Vouchers = wrapped;
        }

        private void UseVoucherExecute(QuizVoucher voucher)
        {
            this.MessageBoxService.Confirm(
                this.GetText("UseVoucherQuestion"),
                string.Empty,
                this.GetSharedText("ButtonYes"),
                this.GetSharedText("ButtonNo"),
                result =>
                {
                    if (result)
                    {
                        voucher.IsUsed = true;
                        this.voucherRepository.Update(voucher);
                        this.MvxMessenger.Publish(new VoucherActivatedMessage(this));
                        this.Close(this);
                    }
                });
        }

        //public event EventHandler<NotificationEventArgs<string, bool>> ActivateVoucher;
        //public event EventHandler<NotificationEventArgs<string, bool>> UseVoucher;

        //private void ActivateVoucherExecuteOld(QuizVoucher voucher)
        //{
        //    if (this.ActivateVoucher != null)
        //    {
        //        this.ActivateVoucher(
        //            this,
        //            new NotificationEventArgs<string, bool>(
        //                string.Format(this.GetText("ActivateVoucherQuestion"), voucher.Code),
        //                string.Empty,
        //                async isActivated =>
        //                {
        //                    if (isActivated)
        //                    {
        //                        await
        //                            this.AsyncExecute(
        //                                () => this.lbkMobileservice.ActivateVoucherAsync(voucher),
        //                                serviceResult =>
        //                                {
        //                                    if (serviceResult)
        //                                    {
        //                                        voucher.IsActivated = true;
        //                                        this.voucherRepository.Update(voucher);
        //                                        this.Load();
        //                                        // TODO: DialogService-Alert: string.Format(this.TextSource.GetText("VoucherActvated"), voucher.Code) 
        //                                        this.ShowAlert(this.GetText("VoucherActvated", voucher.Code), null);
        //                                    }
        //                                    else
        //                                    {
        //                                        voucher.IsUsed = true;
        //                                        voucher.Deleted = true;
        //                                        this.voucherRepository.Update(voucher);
        //                                        // TODO: DialogService-Alert: string.Format(this.TextSource.GetText("VoucherAlreadyActvated"), voucher.Code) 
        //                                        this.ShowAlert(
        //                                            this.GetText("VoucherAlreadyActvated", voucher.Code),
        //                                            null);
        //                                    }
        //                                });
        //                    }
        //                }));
        //    }
        //}

        //private void UseVoucherExecuteOld(QuizVoucher voucher)
        //{
        //    if (this.UseVoucher != null)
        //    {
        //        this.UseVoucher(
        //            this,
        //            new NotificationEventArgs<string, bool>(
        //                this.GetText("UseVoucherQuestion"),
        //                string.Empty,
        //                result =>
        //                {
        //                    if (result)
        //                    {
        //                        voucher.IsUsed = true;
        //                        this.voucherRepository.Update(voucher);
        //                        //Navigate to QuizStartViewModel ?
        //                        this.ShowViewModel<QuizStartViewModel>();
        //                    }
        //                }));
        //    }
        //}
    }
}