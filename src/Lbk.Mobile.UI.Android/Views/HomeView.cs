
using Android.App;
using Cirrious.MvvmCross.Binding.Droid.Views;


namespace Lbk.Mobile.UI.Droid.Views
{
    using Lbk.Mobile.Portable.Core.ViewModels;

    [Activity(Label = "Löwenbräukeller", Icon = "@drawable/icon")]
    public class HomeView
        : MvxBindingActivityView<HomeViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.Page_Home);
        }
    }
}