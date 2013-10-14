using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Lbk.Mobile.UI.Droid.Views.Contact
{
    using Lbk.Mobile.Core.ViewModels.Contact;
    using Lbk.Mobile.Core.ViewModels.Event;

    [Activity(Label = "Kontakt", Icon = "@drawable/ic_launcher")]
    public class ContactView : BaseView<ContactViewModel>
    {
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