namespace Lbk.Mobile.UI.Droid
{
    using System;
    using System.Collections.Specialized;
    using System.Windows.Input;

    using Android.Views;
    using Android.Widget;

    // This class is never actually executed, but when Xamarin linking is enabled it does how to ensure types and properties
    // are preserved in the deployed app
    public class LinkerPleaseInclude
    {
        public void Include(Button button)
        {
            button.Click += (s, e) => button.Text = button.Text + "";
        }

        public void Include(ImageButton imageButton)
        {
            imageButton.Click += (s, e) => { };
        }

        public void Include(CheckBox checkBox)
        {
            checkBox.CheckedChange += (sender, args) => checkBox.Checked = !checkBox.Checked;
        }

        public void Include(TextView text)
        {
            text.TextChanged += (sender, args) => text.Text = "" + text.Text;
            text.Hint = "" + text.Hint;
        }

        public void Include(CompoundButton cb)
        {
            cb.CheckedChange += (sender, args) => cb.Checked = !cb.Checked;
        }

        public void Include(SeekBar sb)
        {
            sb.ProgressChanged += (sender, args) => sb.Progress = sb.Progress + 1;
        }

        public void Include(INotifyCollectionChanged changed)
        {
            changed.CollectionChanged += (s, e) => { var test = string.Format("{0}{1}{2}{3}{4}", e.Action, e.NewItems, e.NewStartingIndex, e.OldItems, e.OldStartingIndex); };
        }

        public void Include(ICommand command)
        {
            command.CanExecuteChanged += (s, e) => { if (command.CanExecute(null)) command.Execute(null); };
        }

        public void Include(View view)
        {
            view.Visibility = view.Visibility + 1;
        }

        public void IncludeEnabled(Button button)
        {
            button.Enabled = !button.Enabled;
        }

        public void Include(ScrollView scrollView)
        {
        }

        public void Include(TableRow tableRow)
        {
            
        }

        public void Include(DatePicker datePicker)
        {
            datePicker.MinDate = DateTime.Now.Second;
        }

        public void Include(NumberPicker numberPicker)
        {
            numberPicker.MaxValue = 10;
            numberPicker.MinValue = 1;
        }

        //public void Include(RelativeLayout relative)
        //{
        //    relative.Visibility = ViewStates.Visible;
        //}
    }
}
