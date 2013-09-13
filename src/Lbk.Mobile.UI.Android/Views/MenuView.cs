//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MenuView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views
{
    using Android.App;
    using Android.OS;

    using Lbk.Mobile.Core.ViewModels.Menu;

    [Activity(Label = "Speisekarte")]
    public class MenuView : BaseView<MenuViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Menu);
        }
    }
}