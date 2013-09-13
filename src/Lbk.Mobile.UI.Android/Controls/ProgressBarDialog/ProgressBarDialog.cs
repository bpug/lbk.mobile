//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ProgressBarDialog.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Controls.ProgressBarDialog
{
    using Android.App;
    using Android.Content;
    using Android.Views;
    using Android.Widget;

    public class ProgressBarDialog : Dialog
    {
        public ProgressBarDialog(Context context)
            : base(context, Resource.Style.ProgressBarDialog)
        {
        }
       
        public static ProgressBarDialog Show(Context context, string title, string message)
        {
            return Show(context, title, message, false);
        }

        public static ProgressBarDialog Show(Context context, string title, string message, bool indeterminate)
        {
            return Show(context, title, message, indeterminate, false, null);
        }

        public static ProgressBarDialog Show(
            Context context,
            string title,
            string message,
            bool indeterminate,
            bool cancelable)
        {
            return Show(context, title, message, indeterminate, cancelable, null);
        }

        public static ProgressBarDialog Show(
            Context context,
            string title,
            string message,
            bool indeterminate,
            bool cancelable,
            IDialogInterfaceOnCancelListener cancelListener)
        {
            var dialog = new ProgressBarDialog(context);
            dialog.SetTitle(title);
            dialog.SetCancelable(cancelable);
            dialog.SetOnCancelListener(cancelListener);
            /* The next line will add the ProgressBar to the dialog. */
            dialog.AddContentView(
                new ProgressBar(context),
                new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent));
            dialog.Show();

            return dialog;
        }

        
    }
}