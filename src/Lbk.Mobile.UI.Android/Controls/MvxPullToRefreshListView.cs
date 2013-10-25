//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MvxPullToRefreshList.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Controls
{
    using System;
    using System.Windows.Input;

    using Android.Content;
    using Android.Util;
    using Android.Views;

    using Cirrious.MvvmCross.Binding.Droid.Views;

    using PullToRefresharp.Android.Delegates;
    using PullToRefresharp.Android.Views;

    public class MvxPullToRefreshListView : MvxListView, IPullToRefresharpWrappedView
    {
        private readonly ListViewDelegate ptrDelegate;

        private bool isRefreshing;

        public MvxPullToRefreshListView(Context context, IAttributeSet attrs)
            : this(context, attrs, new MvxAdapter(context))
        {
        }

        public MvxPullToRefreshListView(Context context, IAttributeSet attrs, IMvxAdapter adapter)
            : base(context, attrs, adapter)
        {
            this.ptrDelegate = new ListViewDelegate(this);

            this.RefreshActivated += (object sender, EventArgs e) =>
            {
                var command = this.RefreshCommand;
                if (command == null)
                {
                    return;
                }

                command.Execute(null);
            };
        }

        public event EventHandler RefreshActivated
        {
            add
            {
                this.ptrDelegate.RefreshActivated += value;
            }
            remove
            {
                this.ptrDelegate.RefreshActivated -= value;
            }
        }

        public event EventHandler RefreshCompleted
        {
            add
            {
                this.ptrDelegate.RefreshCompleted += value;
            }
            remove
            {
                this.ptrDelegate.RefreshCompleted -= value;
            }
        }

        public bool IgnoreTouchEvents
        {
            get
            {
                return this.ptrDelegate.IgnoreTouchEvents;
            }
            set
            {
                this.ptrDelegate.IgnoreTouchEvents = value;
            }
        }

        public bool IsAtTop
        {
            get
            {
                return this.ptrDelegate.IsAtTop;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is refreshing.
        /// </summary>
        /// <value><c>true</c> if this instance is refreshing; otherwise, <c>false</c>.</value>
        public bool IsRefreshing
        {
            get
            {
                return this.isRefreshing;
            }
            set
            {
                this.isRefreshing = value;
                if (this.isRefreshing)
                {
                    //OnRefreshActivated();
                }

                else
                {
                    this.OnRefreshCompleted();
                }
            }
        }

        public bool PullToRefreshEnabled
        {
            get
            {
                return (this.Parent as ViewWrapper).IsPullEnabled;
            }
            set
            {
                (this.Parent as ViewWrapper).IsPullEnabled = value;
            }
        }

        public ICommand RefreshCommand { get; set; }

        public PullToRefresharpRefreshState RefreshState
        {
            get
            {
                return (this.Parent as ViewWrapper).State;
            }
        }

        public void ForceHandleTouchEvent(MotionEvent e)
        {
            base.OnTouchEvent(e);
        }

        public void OnRefreshActivated()
        {
            this.ptrDelegate.OnRefreshActivated();
        }

        public void OnRefreshCompleted()
        {
            this.ptrDelegate.OnRefreshCompleted();
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (this.Parent is ViewWrapper)
            {
                return (this.Parent as ViewWrapper).OnTouchEvent(e) || this.IgnoreTouchEvents || base.OnTouchEvent(e);
            }
            else
            {
                return base.OnTouchEvent(e);
            }
        }

        public void SetTopMargin(int margin)
        {
            this.ptrDelegate.SetTopMargin(margin);
        }
    }
}