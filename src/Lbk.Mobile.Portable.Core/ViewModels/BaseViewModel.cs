// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseViewModel.cs" company="ip-connect GmbH">
//   Copyright (c) ip-connect GmbH. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Portable.Core.ViewModels
{
    using Cirrious.MvvmCross.ExtensionMethods;
    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Portable.Core.Interfaces.Errors;

    public class BaseViewModel : MvxViewModel
    {
        private bool isLoading;

        public bool IsLoading
        {
            get
            {
                return this.isLoading;
            }

            set
            {
                this.isLoading = value;
                this.RaisePropertyChanged(() => this.IsLoading);
            }
        }

        public void ReportError(string error)
        {
            this.GetService<IErrorReporter>().ReportError(error);
        }
    }
}