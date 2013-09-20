//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GalleryView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Gallery
{
    using Android.App;
    using Android.OS;
    using Android.Views;

    using Lbk.Mobile.Core.ViewModels.Gallery;

    [Activity(Label = "Bilder", Icon = "@drawable/ic_launcher")]
    public class GalleryView : BaseView<GalleryViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.Gallery);
        }
    }
}