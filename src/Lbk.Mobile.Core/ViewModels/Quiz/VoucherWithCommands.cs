//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ModelWithCommand.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Quiz
{
    using System;
    using System.Windows.Input;

    using Lbk.Mobile.Model;

    public class VoucherWithCommands : IDisposable
    {

        public VoucherWithCommands(QuizVoucher item, ICommand command1, ICommand useCommand)
        {
            this.ActivateCommand = command1;
            this.UseCommand = useCommand;
            this.Item = item;
        }

        public ICommand ActivateCommand { get; private set; }
        public ICommand UseCommand { get; private set; }
        public QuizVoucher Item { get; private set; }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                var disposableActivateCommand = this.ActivateCommand as IDisposable;
                if (disposableActivateCommand != null)
                {
                    disposableActivateCommand.Dispose();
                }
                this.ActivateCommand = null;

                var disposableUseCommand = this.UseCommand as IDisposable;
                if (disposableUseCommand != null)
                {
                    disposableUseCommand.Dispose();
                }
                this.UseCommand = null;
            }
        }
    }
}