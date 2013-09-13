//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ErrorDisplayer.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid
{
    using Cirrious.CrossCore;
    using Cirrious.CrossCore.Droid.Platform;
    using Cirrious.MvvmCross.Binding.BindingContext;
    using Cirrious.MvvmCross.Binding.Droid.BindingContext;
    using Cirrious.MvvmCross.Plugins.Messenger;

    using Android.App;
    using Android.Content;
    using Android.Views;
    using Android.Widget;

    using Lbk.Mobile.Core.Messages;

    public class ErrorDisplayer
    {
        private readonly Context _applicationContext;

        private IMvxMessenger _messenger;

        private MvxSubscriptionToken _token;

        public ErrorDisplayer(Context applicationContext)
        {
            this._applicationContext = applicationContext;

            //var source = Mvx.Resolve<IErrorService>();

            //source.ErrorReported += (sender, args) => ShowError(args.Message);
            this._token = this.Messenger.SubscribeOnMainThread<ErrorMessage>(this.ShowError);
        }

        protected IMvxMessenger Messenger
        {
            get
            {
                this._messenger = this._messenger ?? Mvx.Resolve<IMvxMessenger>();
                return this._messenger;
            }
        }

        private void ShowError(ErrorMessage errorMessage)
        {
            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity as IMvxBindingContextOwner;
            var alertDialog = new AlertDialog.Builder((Activity)activity).Create();
            alertDialog.SetTitle("Sorry!");
            alertDialog.SetMessage(errorMessage.Message);
            alertDialog.SetButton("OK", (sender, args) => { });
            alertDialog.Show();
        }

        private void ShowError(string message)
        {
            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity as IMvxBindingContextOwner;
            var alertDialog = new AlertDialog.Builder((Activity)activity).Create();
            alertDialog.SetTitle("Sorry!");
            alertDialog.SetMessage(message);
            alertDialog.SetButton("OK", (sender, args) => { });
            alertDialog.Show();
        }

        private void ShowError2(ErrorMessage errorMessage)
        {
            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity as IMvxBindingContextOwner;
            // note that we're not using Binding in this Inflation - but the overhead is minimal - so use it anyway!
            var layoutView = activity.BindingInflate(Resource.Layout.ToastLayout_Error, null);
            var text1 = layoutView.FindViewById<TextView>(Resource.Id.ErrorText1);
            text1.Text = "Sorry!";
            var text2 = layoutView.FindViewById<TextView>(Resource.Id.ErrorText2);
            text2.Text = errorMessage.Message;

            var toast = new Toast(this._applicationContext);
            toast.SetGravity(GravityFlags.CenterVertical, 0, 0);
            toast.Duration = ToastLength.Long;
            toast.View = layoutView;
            toast.Show();
        }

        private void ShowError2(string message)
        {
            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity as IMvxBindingContextOwner;
            // note that we're not using Binding in this Inflation - but the overhead is minimal - so use it anyway!
            var layoutView = activity.BindingInflate(Resource.Layout.ToastLayout_Error, null);
            var text1 = layoutView.FindViewById<TextView>(Resource.Id.ErrorText1);
            text1.Text = "Sorry!";
            var text2 = layoutView.FindViewById<TextView>(Resource.Id.ErrorText2);
            text2.Text = message;

            var toast = new Toast(this._applicationContext);
            toast.SetGravity(GravityFlags.CenterVertical, 0, 0);
            toast.Duration = ToastLength.Long;
            toast.View = layoutView;
            toast.Show();
        }
    }
}