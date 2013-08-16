//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuizStartViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Quiz
{
    using System;
    using System.Linq;
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Common;
    using Lbk.Mobile.Common.Extensions;
    using Lbk.Mobile.Core.Messages;
    using Lbk.Mobile.Data.Repositories;
    using Lbk.Mobile.Model.Enums;

    public class QuizStartViewModel : BaseViewModel
    {
        public event EventHandler<NotificationEventArgs<string, bool>> YouthProtectionQuestion;

        private readonly IQuizVoucherRepository voucherRepository;

        public QuizStartViewModel(IQuizVoucherRepository voucherRepository)
        {
            this.voucherRepository = voucherRepository;
        }

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
                return new MvxCommand(() => this.ShowViewModel<InstructionsViewModel>(), () => this.voucherRepository.GetNotUsed().Any());
            }
        }


        public ICommand StartCommand
        {
            get
            {
                return new MvxCommand(this.StartExecute);
            }
        }

        public ICommand StartCommandWithMessenger
        {
            get
            {
                return new MvxCommand(this.StartMessengerExecute);
            }
        }

        public ICommand StartQuizCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<QuizViewModel>());
            }
        }

        private void StartExecute()
        {
            if (!Settings.YouthProtection)
            {
                // Notify view 
                this.YouthProtectionQuestion.RaiseEvent(this, new NotificationEventArgs<string, bool>(
                            this.TextSource.GetText("YouthProtectionQuestion"),
                            string.Empty,
                            result =>
                            {
                                if (result)
                                {
                                    Settings.YouthProtection = true;
                                    this.StartQuizCommand.Execute(null);
                                }
                            }));

                ////With MessageBoxService
                //this.ShowMessage(this.TextSource.GetText("YouthProtectionQuestion"), null,
                //    result =>
                //    {
                //        if (result)
                //        {
                //            Settings.YouthProtection = true;
                //            this.StartQuizCommand.Execute(null);
                //        }
                //    });
            }
            else
            {
                this.StartQuizCommand.Execute(null);
            }
        }

        private void StartMessengerExecute()
        {
            if (!Settings.YouthProtection)
            {
                var message = new YouthProtectionMessage(this);
                this.MvxMessenger.Publish(message);
                if (message.Result == DialogResult.Ok)
                {
                    Settings.YouthProtection = true;
                    this.StartQuizCommand.Execute(null);
                }
            }
            else
            {
                this.StartQuizCommand.Execute(null);
            }
        }
    }
}