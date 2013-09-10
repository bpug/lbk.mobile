//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ModelWithCommand.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Helpers
{
    using System;
    using System.Windows.Input;

    public class ModelWithCommand<T> : IDisposable
    {
        public ModelWithCommand(T item, ICommand command)
        {
            this.Command = command;
            this.Item = item;
        }

        public ICommand Command { get; private set; }
        public T Item { get; private set; }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                var disposableCommand = this.Command as IDisposable;
                if (disposableCommand != null)
                {
                    disposableCommand.Dispose();
                }
                this.Command = null;
            }
        }
    }
}