//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EventListView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Android.Views
{
    using global::Android.App;
    using global::Android.OS;

    using Lbk.Mobile.Core.ViewModels.Event;

    [Activity(Label = "Events")]
    public class EventListView : BaseView<EventListViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //this.SetContentView(Resource.Layout.TodaysMenu);

            this.ViewModel.PropertyChanged += (sender, args) =>
            {
                var vm = (EventListViewModel)sender;
                switch (args.PropertyName)
                {
                    case "Events":
                        break;
                }
            };
        }
    }
}