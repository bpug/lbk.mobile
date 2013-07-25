//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="SplashScreenViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Home
{
    using Cirrious.MvvmCross.ViewModels;

    public class SplashScreenViewModel : MvxViewModel
    {
        private bool splashScreenComplete;

        public SplashScreenViewModel()
        {
            this.SplashScreenComplete = false;
            this.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "IsSearching")
                {
                    this.MoveForwardsIfPossible();
                }
            };
        }

        public bool SplashScreenComplete
        {
            get
            {
                return this.splashScreenComplete;
            }
            set
            {
                this.splashScreenComplete = value;
                this.MoveForwardsIfPossible();
            }
        }

        private void MoveForwardsIfPossible()
        {
            if (!this.SplashScreenComplete)
            {
                return;
            }

            this.ShowViewModel<HomeViewModel>(true);
        }
    }
}