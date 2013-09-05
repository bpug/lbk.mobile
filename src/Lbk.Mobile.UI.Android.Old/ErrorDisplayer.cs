using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Interfaces;
using Cirrious.MvvmCross.Binding.Droid.Interfaces.Views;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;


namespace Lbk.Mobile.UI.Droid
{
    using Lbk.Mobile.Portable.Core.Interfaces.Errors;

    public class ErrorDisplayer
        : IMvxServiceConsumer<IErrorSource>
        , IMvxServiceConsumer<IMvxAndroidCurrentTopActivity>
    {
        private readonly Context _applicationContext;

        public ErrorDisplayer(Context applicationContext)
        {
            _applicationContext = applicationContext;

            var source = this.GetService<IErrorSource>();
            source.ErrorReported += (sender, args) => ShowError(args.Message);
        }

        private void ShowError(string message)
        {
            var activity = this.GetService<IMvxAndroidCurrentTopActivity>().Activity as IMvxBindingActivity;
            var alertDialog = new AlertDialog.Builder((Activity)activity).Create();
            alertDialog.SetTitle("Sorry!");
            alertDialog.SetMessage(message);
            alertDialog.SetButton("OK", (sender, args) => { });
            alertDialog.Show();
        }
    }
}