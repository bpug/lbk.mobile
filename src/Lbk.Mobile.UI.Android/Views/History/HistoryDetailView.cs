//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="RoomDetailView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.History
{
    using Android.App;
    using Android.OS;

    using Lbk.Mobile.Core.ViewModels.History;

    using Resource = Lbk.Mobile.UI.Droid.Resource;

    [Activity(Label = "Historie", Icon = "@drawable/ic_launcher")]
    public class HistoryDetailView : BaseView<HistoryDetailViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.History_Details);
            
        }
    }
}