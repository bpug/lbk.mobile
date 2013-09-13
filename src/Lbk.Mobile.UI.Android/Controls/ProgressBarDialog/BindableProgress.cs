//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BindableProgress.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Controls.ProgressBarDialog
{
    using Android.App;
    using Android.Content;

    public class BindableProgress
    {
        private readonly Context context;

        private ProgressBarDialog dialog;

        public BindableProgress(Context context)
        {
            this.context = context;
        }

        
        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.dialog != null)
                {
                    this.dialog.Dismiss();
                }
            }
        }

        public bool Visible
        {
            get
            {
                return this.dialog != null;
            }
            set
            {
                if (value == this.Visible)
                {
                    return;
                }

                if (value)
                {
                    //this.dialog = new ProgressDialog(this.context);
                    //this.dialog.SetProgressStyle(ProgressDialogStyle.Spinner);
                    ////this.dialog.SetTitle("Working...");
                    //this.dialog.Show();
                    this.dialog = ProgressBarDialog.Show(this.context, string.Empty, string.Empty, true);
                }
                else
                {
                    this.dialog.Hide();
                    this.dialog.Dismiss();
                    this.dialog = null;
                }
            }
        }
    }
}