//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FirstViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace PortableTest.ViewModels
{
    using Cirrious.MvvmCross.ViewModels;

    public class FirstViewModel : MvxViewModel
    {
        private string _hello = "Hello MvvmCross";

        public string Hello
        {
            get
            {
                return this._hello;
            }
            set
            {
                this._hello = value;
                this.RaisePropertyChanged(() => this.Hello);
            }
        }
    }
}