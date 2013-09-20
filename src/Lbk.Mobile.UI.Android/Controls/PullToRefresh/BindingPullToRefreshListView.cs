namespace Lbk.Mobile.UI.Droid.Controls.PullToRefresh
{
    using System;
    using System.Threading;
    using System.Windows.Input;

    using Android.App;
    using Android.Content;
    using Android.Util;

    using Lbk.Mobile.UI.Droid.Controls.PullToRefresh.Library;

    public abstract class BindingPullToRefreshListView
        : PullToRefreshListView
    {
        protected static  int ResourceId;

        protected BindingPullToRefreshListView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            this.Initialize();
            this.SetOnPullUpRefreshAction(new BindingListener(this.OnPullUpRefreshRequested, this.WaitForTail));
            this.SetOnPullDownRefreshAction(new BindingListener(this.OnPullDownRefreshRequested, this.WaitForHead));
        }


        //protected BindingPullToRefreshListView(Context context, IAttributeSet attrs)
        //    : base(context, attrs, ResourceId)
        //{
        //    this.Initialize();
        //    this.SetOnPullUpRefreshAction(new BindingListener(this.OnPullUpRefreshRequested, this.WaitForTail));
        //    this.SetOnPullDownRefreshAction(new BindingListener(this.OnPullDownRefreshRequested, this.WaitForHead));
        //}

        protected abstract void Initialize();

        private class BindingListener : IRefreshListener
        {
            readonly Action _requestAction;
            readonly Action _waitAction;

            public BindingListener(Action requestAction, Action waitAction)
            {
                this._requestAction = requestAction;
                this._waitAction = waitAction;
            }

            public void DoRefresh()
            {
                this._requestAction();                
                this._waitAction();
            }

            public void RefreshFinished()
            {
                // ignored
            }
        }

        public ICommand PullDownRefreshRequested { get; set; }

        public void OnPullDownRefreshRequested()
        {
            var handler = this.PullDownRefreshRequested;
            this.SyncFireHandler(handler);
        }

        private readonly ManualResetEvent _manualResetEventHead = new ManualResetEvent(true);
        public bool IsRefreshingHead
        {
            get { return this._manualResetEventHead.WaitOne(0); }
            set
            {
                if (value)
                    this._manualResetEventHead.Reset();
                else
                    this._manualResetEventHead.Set();
            }
        }

        private void WaitForHead()
        {
            this._manualResetEventHead.WaitOne();
        }

        public ICommand PullUpRefreshRequested { get; set; }

        public void OnPullUpRefreshRequested()
        {
            var handler = this.PullUpRefreshRequested;
            this.SyncFireHandler(handler);
        }

        private readonly ManualResetEvent _manualResetEventTail = new ManualResetEvent(true);
        public bool IsRefreshingTail
        {
            get { return this._manualResetEventTail.WaitOne(0); }
            set
            {
                if (value)
                    this._manualResetEventTail.Reset();
                else
                    this._manualResetEventTail.Set();
            }
        }

        private void WaitForTail()
        {
            this._manualResetEventTail.WaitOne();
        }

        private void SyncFireHandler(ICommand handler)
        {
            if (handler != null)
            {
                var waitForHandler = new ManualResetEvent(false);
                ((Activity)this.Context).RunOnUiThread(
                    () =>
                    {
                        handler.Execute(null);
                        waitForHandler.Set();
                    });
                waitForHandler.WaitOne();
            }
        }
    }
}