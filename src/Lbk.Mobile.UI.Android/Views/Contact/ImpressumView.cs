//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ImpressumView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Contact
{
    using Android.App;
    using Android.OS;

    using Lbk.Mobile.Core.ViewModels.Contact;

    [Activity(Label = "Kontakt", Icon = "@drawable/ic_launcher")]
    public class ImpressumView : BaseView<ImpressumViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Impressum_Page);
        }
    }
}