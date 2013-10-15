//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ContactView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Contact
{
    using Android.App;
    using Android.OS;
    using Android.Views;

    using Lbk.Mobile.Core.ViewModels.Contact;

    [Activity(Label = "Kontakt", Icon = "@drawable/ic_launcher")]
    public class ContactView : BaseView<ContactViewModel>
    {
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var inflater = this.MenuInflater;
            inflater.Inflate(Resource.Menu.contact_actions, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_imressum:
                    this.ViewModel.ShowImpressumCommand.Execute(null);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Contact_Page);
        }

        protected override void OnDestroy()
        {
            this.ViewModel.StopWatcher();
            base.OnDestroy();
        }
    }
}