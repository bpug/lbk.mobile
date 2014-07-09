namespace Lbk.Mobile.UI.Droid.Test.Controls.PullToRefresh.Library
{
    using System;

    using Android.Content;
    using Android.OS;
    using Android.Runtime;
    using Android.Util;
    using Android.Views;
    using Android.Widget;

    using Cirrious.MvvmCross.Binding.Droid.BindingContext;

    public class PullToRefreshListView
        : RelativeLayout
        
    {
        private  ListView _listView;
        private  Handler _uiThreadHandler;
        private ViewGroup _lowerButton;
        private PullToRefreshComponent _pullToRefresh;
        private ViewGroup _upperButton;

        public PullToRefreshListView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public PullToRefreshListView(Context context, IAttributeSet attrs, int whichResourceId)
            : base(context, attrs)
        {
            var bindingContext = MvxAndroidBindingContextHelpers.Current();
            bindingContext.BindingInflate(whichResourceId, this);
            this._listView = this.FindViewById<ListView>(Android.Resource.Id.List);
            this._uiThreadHandler = new Handler();
            this.InitializePullToRefreshList();
        }

        public PullToRefreshListView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            
        }

        public void Init(int whichResourceId)
        {
            var bindingContext = MvxAndroidBindingContextHelpers.Current();
            bindingContext.BindingInflate(whichResourceId, this);
            this._listView = this.FindViewById<ListView>(Android.Resource.Id.List);
            this._uiThreadHandler = new Handler();
            this.InitializePullToRefreshList();
        }

        public ListView UnderlyingListView
        {
            get { return this._listView; }
        }

        private void InitializePullToRefreshList()
        {
            this.InitializeRefreshUpperButton();
            this.InitializeRefreshLowerButton();
            this._pullToRefresh = new PullToRefreshComponent(
                this._upperButton, this._lowerButton, this._listView,
                this._uiThreadHandler);

            this._pullToRefresh.SetOnReleaseUpperReady(new OnReleaseReadyUpper(this));
            this._pullToRefresh.SetOnReleaseLowerReady(new OnReleaseReadyLower(this));
        }

        public void SetOnPullDownRefreshAction(IRefreshListener listener)
        {
            this._pullToRefresh.SetOnPullDownRefreshAction(new PullDownRefreshListener(this, listener));
        }

        protected void RunOnUIThread(Action action)
        {
            this._uiThreadHandler.Post(action);
        }

        public void SetOnPullUpRefreshAction(IRefreshListener listener)
        {
            this._pullToRefresh.SetOnPullUpRefreshAction(new PullUpRefreshListener(this, listener));
        }

        private void InitializeRefreshLowerButton()
        {
            this._upperButton = this.FindViewById<ViewGroup>(Resource.Id.refresh_upper_button);
        }

        private void InitializeRefreshUpperButton()
        {
            this._lowerButton = this.FindViewById<ViewGroup>(Resource.Id.refresh_lower_button);
        }

        protected void SetPullToRefresh(ViewGroup refreshView)
        {
            string text = "";
            if (refreshView == this._upperButton)
            {
                text = "PullDownToRefresh";
            }
            else
            {
                text = "PullUpToRefresh";
            }

            RefreshTextView(refreshView).Text = (text);
            RefreshProgressBar(refreshView).Visibility = ViewStates.Invisible;
            RefreshImage(refreshView).Visibility = ViewStates.Visible;
        }

        private static View RefreshImage(ViewGroup refreshView)
        {
            return refreshView.FindViewById(Resource.Id.pull_to_refresh_image);
        }

        private static View RefreshProgressBar(ViewGroup refreshView)
        {
            return refreshView.FindViewById(Resource.Id.pull_to_refresh_progress);
        }

        private static TextView RefreshTextView(ViewGroup refreshView)
        {
            return (refreshView.FindViewById<TextView>(Resource.Id.pull_to_refresh_text));
        }

        protected void SetRefreshing(ViewGroup refreshView)
        {
            RefreshTextView(refreshView).Text = "RefreshStarted";
            RefreshProgressBar(refreshView).Visibility = ViewStates.Visible;
            RefreshImage(refreshView).Visibility = ViewStates.Invisible;
        }

        public void DisablePullUpToRefresh()
        {
            this._pullToRefresh.DisablePullUpToRefresh();
        }

        public void DisablePullDownToRefresh()
        {
            this._pullToRefresh.DisablePullDownToRefresh();
        }

        #region Nested type: BaseNested

        private class BaseNested
        {
            protected readonly PullToRefreshListView Parent;

            public BaseNested(PullToRefreshListView parent)
            {
                this.Parent = parent;
            }
        }

        #endregion

        #region Nested type: OnReleaseReadyLower

        private class OnReleaseReadyLower : BaseNested, IOnReleaseReady
        {
            public OnReleaseReadyLower(PullToRefreshListView parent)
                : base(parent)
            {
            }

            #region IOnReleaseReady Members

            public void ReleaseReady(bool ready)
            {
                string textKey = ready
                                  ? "ReleaseToRefresh"
                                  : "PullUpToRefresh";
                RefreshTextView(this.Parent._lowerButton).Text = textKey;
            }

            #endregion
        }

        #endregion

        #region Nested type: OnReleaseReadyUpper

        private class OnReleaseReadyUpper : BaseNested, IOnReleaseReady
        {
            public OnReleaseReadyUpper(PullToRefreshListView parent)
                : base(parent)
            {
            }

            #region IOnReleaseReady Members

            public void ReleaseReady(bool ready)
            {
                string textKey = ready
                                  ? "ReleaseToRefresh"
                                  : "PullDownToRefresh";
                RefreshTextView(this.Parent._upperButton).Text = textKey;
            }

            #endregion
        }

        #endregion

        #region Nested type: PullDownRefreshListener

        private class PullDownRefreshListener : BaseNested, IRefreshListener
        {
            private readonly IRefreshListener _listener;

            public PullDownRefreshListener(PullToRefreshListView parent, IRefreshListener listener)
                : base(parent)
            {
                this._listener = listener;
            }

            #region IRefreshListener Members

            public void DoRefresh()
            {
                this.Parent._uiThreadHandler.Post(() =>
                                                {
                                                    this.Parent.SetRefreshing(this.Parent._upperButton);
                                                    this.Parent.Invalidate();
                                                });
                this._listener.DoRefresh();
            }

            public void RefreshFinished()
            {
                this.Parent.RunOnUIThread(() =>
                                         {
                                             this.Parent.SetPullToRefresh(this.Parent._upperButton);
                                             this.Parent.Invalidate();
                                             this._listener.RefreshFinished();
                                         });
            }

            #endregion
        }

        #endregion

        #region Nested type: PullUpRefreshListener

        private class PullUpRefreshListener : BaseNested, IRefreshListener
        {
            private readonly IRefreshListener _listener;

            public PullUpRefreshListener(PullToRefreshListView parent, IRefreshListener listener)
                : base(parent)
            {
                this._listener = listener;
            }

            #region IRefreshListener Members

            public void DoRefresh()
            {
                this.Parent._uiThreadHandler.Post(() =>
                                                {
                                                    this.Parent.SetRefreshing(this.Parent._lowerButton);
                                                    this.Parent.Invalidate();
                                                });
                this._listener.DoRefresh();
            }

            public void RefreshFinished()
            {
                this.Parent.SetPullToRefresh(this.Parent._lowerButton);
                this.Parent.Invalidate();
                this._listener.RefreshFinished();
            }

            #endregion
        }

        #endregion
    }
}