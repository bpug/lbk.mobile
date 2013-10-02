//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GalleryView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Gallery
{
    using Android.App;
    using Android.OS;
    using Android.Util;
    using Android.Widget;

    using Cirrious.MvvmCross.Binding.Droid.Views;

    using Lbk.Mobile.Core.ViewModels.Gallery;
    using Lbk.Mobile.UI.Droid.Extensions;

    [Activity(Label = "Bilder", Icon = "@drawable/ic_launcher")]
    public class GalleryView : BaseView<GalleryViewModel>
    {
        private int columnWidth;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Gallery);
            //var gridView = this.FindViewById<MvxGridView>(Resource.Id.gallery_gridview);
            //InitilizeGridLayout(gridView);
        }

        private void InitilizeGridLayout(MvxGridView gridView)
        {
            float padding = TypedValue.ApplyDimension(
                ComplexUnitType.Dip,
                Constants.Gallery.GridPadding,
                this.Resources.DisplayMetrics);
            this.columnWidth =
                (int)
                    ((this.GetScreenWidth() - ((Constants.Gallery.GridColumnsCount + 1) * padding))
                     / Constants.Gallery.GridColumnsCount);

            gridView.SetNumColumns(Constants.Gallery.GridColumnsCount);
            gridView.SetColumnWidth(this.columnWidth);
            gridView.StretchMode = StretchMode.NoStretch;
            gridView.SetPadding((int)padding, (int)padding, (int)padding, (int)padding);
            gridView.SetHorizontalSpacing((int)padding);
            gridView.SetVerticalSpacing((int)padding);
        }
    }
}

//<Mvx.MvxGridView xmlns:android="http://schemas.android.com/apk/res/android"
//    xmlns:local="http://schemas.android.com/apk/res-auto"
//    android:id="@+id/gallery_gridview"
//    android:layout_width="fill_parent"
//    android:layout_height="fill_parent"
//    android:numColumns="auto_fit"
//    android:gravity="center"
//    android:stretchMode="columnWidth"
//    android:background="#000000"
//    local:MvxBind="ItemsSource Pictures; ItemClick ShowPictureCommand"
//    local:MvxItemTemplate="@layout/gallery_item" />