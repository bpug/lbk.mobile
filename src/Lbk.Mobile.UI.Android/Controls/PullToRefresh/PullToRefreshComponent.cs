namespace Lbk.Mobile.UI.Droid.Controls.PullToRefresh
{
    using System;
    using System.Threading;

    using Android.OS;
    using Android.Views;
    using Android.Widget;

    using Java.Lang;

    using Math = System.Math;

    public class PullToRefreshComponent
    {
        public const float MinPullElementHeight = 100;

        private const float MaxPullElementHeight = 200;
        private const float PullElementStandbyHeight = 100;
        private const int EventCount = 3;

        private readonly float[] _lastYs = new float[EventCount];
        private readonly ListView _listView;
        private readonly View _lowerView;
        private readonly Handler _uiThreadHandler;
        private readonly View _upperView;
        private bool _mayPullDownToRefresh;
        private bool _mayPullUpToRefresh = true;

        private IRefreshListener _onPullDownRefreshAction = new NoRefreshListener();
        private IRefreshListener _onPullUpRefreshAction = new NoRefreshListener();

        private IOnReleaseReady _onReleaseLowerReady;
        private IOnReleaseReady _onReleaseUpperReady;
        protected IScrollingState State;

        public PullToRefreshComponent(View upperView, View lowerView,
                                      ListView listView, Handler uiThreadHandler)
        {
            this._upperView = upperView;
            this._lowerView = lowerView;
            this._listView = listView;
            this._uiThreadHandler = uiThreadHandler;
            this.Initialize();
        }

        private void Initialize()
        {
            this.State = new PullToRefreshState();
            this._listView.Touch += (sender, args) =>
                                  {
                                      MotionEvent motionEvent = args.Event;
                                      if (motionEvent.Action == MotionEventActions.Up)
                                      {
                                          this.InitializeYsHistory();
                                          this.State.TouchStopped(motionEvent, this);
                                      }
                                      else if (motionEvent.Action == MotionEventActions.Move)
                                      {
                                          args.Handled = this.State.HandleMovement(motionEvent, this);
                                      }
                                      args.Handled = false;
                                  };
        }

        protected float Average(float[] ysArray)
        {
            float avg = 0;
            for (int i = 0; i < EventCount; i++)
            {
                avg += ysArray[i];
            }
            return avg/EventCount;
        }

        public void BeginPullDownRefresh()
        {
            this.BeginRefresh(this._upperView, this._onPullDownRefreshAction);
        }

        private void BeginRefresh(View viewToUpdate,
                                  IRefreshListener refreshAction)
        {
            ViewGroup.LayoutParams layoutParams = viewToUpdate.LayoutParameters;
            layoutParams.Height = (int) PullElementStandbyHeight;
            viewToUpdate.LayoutParameters = layoutParams;
            //UITrace.Trace("PullDown:refreshing");
            this.State = new RefreshingState();
            ThreadPool.QueueUserWorkItem((ignored) =>
                                             {
                                                 try
                                                 {
                                                     //var start = DateTime.UtcNow;
                                                     refreshAction.DoRefresh();
                                                     //var finish = DateTime.UtcNow;
                                                     //long difference = finish - start;
                                                     //try
                                                     //{
                                                     //    Thread.Sleep(Math.Max(difference, 1500));
                                                     //}
                                                     //catch (InterruptedException e)
                                                     //{
                                                     //}
                                                 }
                                                 catch (RuntimeException e)
                                                 {
                                                     //UITrace.Trace("Error: {0}", e.ToLongString());
                                                     throw e;
                                                 }
                                                 finally
                                                 {
                                                     this.RunOnUiThread(() => this.RefreshFinished(refreshAction));
                                                 }
                                             });
        }


        public void BeginPullUpRefresh()
        {
            this.BeginRefresh(this._lowerView, this._onPullUpRefreshAction);
        }

        /**************************************************************/
        // Listeners
        /**************************************************************/

        public void SetOnPullDownRefreshAction(IRefreshListener onRefreshAction)
        {
            this.EnablePullDownToRefresh();
            this._onPullDownRefreshAction = onRefreshAction;
        }

        public void SetOnPullUpRefreshAction(IRefreshListener onRefreshAction)
        {
            this.EnablePullUpToRefresh();
            this._onPullUpRefreshAction = onRefreshAction;
        }

        public void RefreshFinished(IRefreshListener refreshAction)
        {
            //UITrace.Trace("PullDown: ready");
            this.State = new PullToRefreshState();
            this.InitializeYsHistory();
            this.RunOnUiThread(() =>
                              {
                                  float dp = new Pixel(0, this._listView.Resources).ToDp();
                                  this.SetUpperButtonHeight(dp);
                                  this.SetLowerButtonHeight(dp);
                                  refreshAction.RefreshFinished();
                              });
        }

        private void RunOnUiThread(Action action)
        {
            this._uiThreadHandler.Post(action);
        }

        private void SetUpperButtonHeight(float height)
        {
            this.SetHeight(height, this._upperView);
        }

        private void SetLowerButtonHeight(float height)
        {
            this.SetHeight(height, this._lowerView);
        }

        private void SetHeight(float height, View view)
        {
            if (view == null)
            {
                return;
            }
            ViewGroup.LayoutParams layoutParams = view.LayoutParameters;
            if (layoutParams == null)
            {
                return;
            }
            layoutParams.Height = (int) height;
            view.LayoutParameters = layoutParams;
            view.Parent.RequestLayout();
        }

        public int GetListTop()
        {
            return this._listView.Top;
        }

        public void InitializeYsHistory()
        {
            for (int i = 0; i < EventCount; i++)
            {
                this._lastYs[i] = 0;
            }
        }

        /**************************************************************/
        // HANDLE PULLING
        /**************************************************************/

        public void PullDown(MotionEvent mevent, float firstY)
        {
            float averageY = this.Average(this._lastYs);

            var height = (int) Math.Max(
                Math.Min(averageY - firstY, MaxPullElementHeight), 0);
            this.SetUpperButtonHeight(height);
        }

        public void PullUp(MotionEvent mevent, float firstY)
        {
            float averageY = this.Average(this._lastYs);

            var height = (int) Math.Max(Math.Min(firstY - averageY, MaxPullElementHeight), 0);
            this.SetLowerButtonHeight(height);
        }

        public bool IsPullingDownToRefresh()
        {
            return this._mayPullDownToRefresh && this.IsIncremental()
                   && this.IsFirstVisible();
        }

        public bool IsPullingUpToRefresh()
        {
            return this._mayPullUpToRefresh && this.IsDecremental()
                   && this.IsLastVisible();
        }

        private bool IsFirstVisible()
        {
            if (this._listView.Count == 0)
            {
                return true;
            }
            else if (this._listView.FirstVisiblePosition == 0)
            {
                return this._listView.GetChildAt(0).Top >= this._listView.Top;
            }
            else
            {
                return false;
            }
        }

        private bool IsLastVisible()
        {
            if (this._listView.Count == 0)
            {
                return true;
            }
            else if (this._listView.LastVisiblePosition + 1 == this._listView.Count)
            {
                return this._listView.GetChildAt(this._listView.ChildCount - 1)
                           .Bottom <= this._listView.Bottom;
            }
            else
            {
                return false;
            }
        }

        private bool IsIncremental(int from, int to, int step)
        {
            float realFrom = this._lastYs[from];
            float realTo = this._lastYs[to];
            //UITrace.Trace("pull to refresh scrolling from " + realFrom);
            //UITrace.Trace("pull to refresh scrolling to " + realTo);
            return this._lastYs[from] != 0 && realTo != 0
                   && Math.Abs(realFrom - realTo) > 50 && realFrom*step < realTo;
        }

        private bool IsIncremental()
        {
            return this.IsIncremental(0, EventCount - 1, +1);
        }

        private bool IsDecremental()
        {
            return this.IsIncremental(0, EventCount - 1, -1);
        }

        public void UpdateEventStates(MotionEvent motionEvent)
        {
            for (int i = 0; i < EventCount - 1; i++)
            {
                this._lastYs[i] = this._lastYs[i + 1];
            }

            float y = motionEvent.GetY();
            int top = this._listView.Top;
            //UITrace.Trace("Pulltorefresh event y:" + y);
            //UITrace.Trace("Pulltorefresh list top:" + top);
            this._lastYs[EventCount - 1] = y + top;
        }

        /**************************************************************/
        // State Change
        /**************************************************************/

        public void SetPullingDown(MotionEvent motionEvent)
        {
            //UITrace.Trace("PullDown pulling down");
            this.State = new PullingDownState(motionEvent);
        }

        public void SetPullingUp(MotionEvent motionEvent)
        {
            //UITrace.Trace("PullDown pulling up");
            this.State = new PullingUpState(motionEvent);
        }

        public float GetBottomViewHeight()
        {
            return this._lowerView.Height;
        }

        public IRefreshListener GetOnUpperRefreshAction()
        {
            return this._onPullDownRefreshAction;
        }

        public IRefreshListener GetOnLowerRefreshAction()
        {
            return this._onPullUpRefreshAction;
        }

        public void DisablePullUpToRefresh()
        {
            this._mayPullUpToRefresh = false;
        }

        public void DisablePullDownToRefresh()
        {
            this._mayPullDownToRefresh = false;
        }

        public void EnablePullUpToRefresh()
        {
            this._mayPullUpToRefresh = true;
        }

        public void EnablePullDownToRefresh()
        {
            this._mayPullDownToRefresh = true;
        }

        public void SetOnReleaseUpperReady(IOnReleaseReady onReleaseUpperReady)
        {
            this._onReleaseUpperReady = onReleaseUpperReady;
        }

        public void SetOnReleaseLowerReady(IOnReleaseReady onReleaseUpperReady)
        {
            this._onReleaseLowerReady = onReleaseUpperReady;
        }

        public void ReadyToReleaseUpper(bool ready)
        {
            if (this._onReleaseUpperReady != null)
            {
                this._onReleaseUpperReady.ReleaseReady(ready);
            }
        }

        public void ReadyToReleaseLower(bool ready)
        {
            if (this._onReleaseLowerReady != null)
            {
                this._onReleaseLowerReady.ReleaseReady(ready);
            }
        }
    }
}