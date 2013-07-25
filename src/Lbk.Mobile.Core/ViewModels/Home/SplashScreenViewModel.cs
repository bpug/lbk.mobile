using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lbk.Mobile.Core.ViewModels.Home
{
    using Cirrious.MvvmCross.ViewModels;

    public class SplashScreenViewModel
        : MvxViewModel
    {
        public SplashScreenViewModel()
        {
            SplashScreenComplete = false;
            this.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "IsSearching")
                {
                    MoveForwardsIfPossible();
                }
            };
        }

        private bool splashScreenComplete;
        public bool SplashScreenComplete
        {
            get { return this.splashScreenComplete; }
            set
            {
                this.splashScreenComplete = value;
                MoveForwardsIfPossible();
            }
        }

        private void MoveForwardsIfPossible()
        {
            if (!SplashScreenComplete)
                return;

            ShowViewModel<HomeViewModel>(true);
        }
    }
}
