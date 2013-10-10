//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="HelpViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Quiz
{
    public class InstructionsViewModel : BaseViewModel
    {
       
        private  string text;
        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
                this.RaisePropertyChanged(() => this.Text);
            }
        }
    }
}