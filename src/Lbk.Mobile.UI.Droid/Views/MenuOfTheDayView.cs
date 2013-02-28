// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuOfTheDayView.cs" company="ip-connect GmbH">
//   Copyright (c) ip-connect GmbH. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views
{
    using Android.App;

    using Cirrious.MvvmCross.Binding.Droid.Views;

    using Lbk.Mobile.Portable.Core.ViewModels.MenuOfTheDay;

    [Activity(Label = "Speisekarte")]
    public class MenuOfTheDay : MvxBindingActivityView<MenuOfTheDayViewModel>
    {
        protected override void OnViewModelSet()
        {
            this.SetContentView(Resource.Layout.Page_MenuOfTheDay);
        }
    }
}