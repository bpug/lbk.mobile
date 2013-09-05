//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TodaysMenuView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Android.Views
{
    using System;

    using Cirrious.MvvmCross.Droid.Views;

    using global::Android.App;
    using global::Android.OS;

    [Activity(Label = "Tageskarte" )]
    public class TodaysMenuView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            this.Title = DateTime.Now.ToShortDateString();
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.TodaysMenu);
            
        }
       
    }
}