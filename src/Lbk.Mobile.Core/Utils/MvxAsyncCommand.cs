//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MvxAsyncCommand.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.Utils
{
    using System;
    using System.Threading.Tasks;

    using Cirrious.MvvmCross.ViewModels;

    public class MvxAsyncCommand : IMvxCommand
    {
        private readonly Func<bool> canExecute;

        private readonly Func<Task> execute;

        private bool isExecuting;

        public MvxAsyncCommand(Func<Task> execute)
            : this(execute, () => true)
        {
        }

        public MvxAsyncCommand(Func<Task> execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return !this.isExecuting && this.canExecute();
        }

        public bool CanExecute()
        {
            return this.CanExecute((object)null);
        }

        public async void Execute(object parameter)
        {
            this.isExecuting = true;
            this.RaiseCanExecuteChanged();

            try
            {
                await this.execute();
            }
            finally
            {
                this.isExecuting = false;
                this.RaiseCanExecuteChanged();
            }
        }

        public void Execute()
        {
            this.Execute((object)null);
        }

        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, new EventArgs());
            }
        }
    }

    public class MvxAsyncCommand<T> : IMvxCommand
    {
        private readonly Func<T, bool> canExecute;

        private readonly Func<T, Task> execute;

        private bool isExecuting;

        public MvxAsyncCommand(Func<T, Task> execute)
            : this(execute, x => true)
        {
        }

        public MvxAsyncCommand(Func<T, Task> execute, Func<T, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return !this.isExecuting && this.canExecute((T)parameter);
        }

        public bool CanExecute()
        {
            return this.CanExecute((object)null);
        }

        public async void Execute(object parameter)
        {
            this.isExecuting = true;
            this.RaiseCanExecuteChanged();

            try
            {
                await this.execute((T)parameter);
            }
            finally
            {
                this.isExecuting = false;
                this.RaiseCanExecuteChanged();
            }
        }

        public void Execute()
        {
            this.Execute((object)null);
        }

        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, new EventArgs());
            }
        }
    }
}