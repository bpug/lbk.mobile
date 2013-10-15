//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuizStartViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Quiz
{
    using System.Linq;
    using System.Windows.Input;

    using Cirrious.MvvmCross.Plugins.Messenger;
    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Core.Messages;
    using Lbk.Mobile.Data.Repositories;

    public class QuizStartViewModel : BaseViewModel
    {
        private readonly MvxSubscriptionToken token;

        private readonly IQuizVoucherRepository voucherRepository;

        public QuizStartViewModel(IQuizVoucherRepository voucherRepository)
        {
            this.voucherRepository = voucherRepository;
            this.token = this.SubscribeOnMainThread<VoucherActivatedMessage>(this.OnVoucherActivated);
        }

        public bool ExistNotUsedVoucher { get; set; }

        public ICommand InstructionsCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<InstructionsViewModel>());
            }
        }
       
        public ICommand ShowVouchersCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<VoucherListViewModel>(), this.IsExistNotUsedVoucher);
            }
        }

        public ICommand StartCommand
        {
            get
            {
                return new MvxCommand(this.StartExecute);
            }
        }

        public ICommand StartQuizCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<QuizViewModel>());
            }
        }

        public override void Start()
        {
            base.Start();
            this.IsExistNotUsedVoucher();
        }

        private bool IsExistNotUsedVoucher()
        {
            bool result = this.voucherRepository.GetNotUsed().Any();
            this.ExistNotUsedVoucher = result;
            return result;
        }

        private void OnVoucherActivated(VoucherActivatedMessage voucherActivatedMessage)
        {
            this.IsExistNotUsedVoucher();
        }

        private void StartExecute()
        {
            if (!Settings.YouthProtection)
            {
                this.MessageBoxService.Confirm(
                    this.GetText("YouthProtectionQuestion"),
                    string.Empty,
                    this.GetSharedText("ButtonYes"),
                    this.GetSharedText("ButtonNo"),
                    b =>
                    {
                        if (b)
                        {
                            Settings.YouthProtection = true;
                            this.StartQuizCommand.Execute(null);
                        }
                    });
            }
            else
            {
                this.StartQuizCommand.Execute(null);
            }
        }

        //public event EventHandler<NotificationEventArgs<string, bool>> YouthProtectionQuestion;

        //public ICommand StartCommandWithMessenger
        //{
        //    get
        //    {
        //        return new MvxCommand(this.StartMessengerExecute);
        //    }
        //}

        //private void StartMessengerExecute()
        //{
        //    if (!Settings.YouthProtection)
        //    {
        //        var message = new YouthProtectionMessage(this);
        //        this.MvxMessenger.Publish(message);
        //        if (message.Result == DialogResult.Ok)
        //        {
        //            Settings.YouthProtection = true;
        //            this.StartQuizCommand.Execute(null);
        //        }

        //        //// Notify view 
        //        //this.YouthProtectionQuestion.RaiseEvent(this, new NotificationEventArgs<string, bool>(
        //        //            this.TextSource.GetText("YouthProtectionQuestion"),
        //        //            string.Empty,
        //        //            result =>
        //        //            {
        //        //                if (result)
        //        //                {
        //        //                    Settings.YouthProtection = true;
        //        //                    this.StartQuizCommand.Execute(null);
        //        //                }
        //        //            }));
        //    }
        //    else
        //    {
        //        this.StartQuizCommand.Execute(null);
        //    }
        //}
    }
}