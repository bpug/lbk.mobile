namespace Lbk.Mobile.UI.Droid.Controls.PullToRefresh
{
    using Android.Views;

    public class PullingDownState : IScrollingState
    {
        private readonly float _firstY;

        public PullingDownState(MotionEvent motionEvent)
        {
            this._firstY = motionEvent.GetY();
        }

        #region IScrollingState Members

        public bool TouchStopped(MotionEvent motionEvent,
                                 PullToRefreshComponent pullToRefreshComponent)
        {
            if (pullToRefreshComponent.GetListTop() > PullToRefreshComponent.MinPullElementHeight)
            {
                pullToRefreshComponent.BeginPullDownRefresh();
            }
            else
            {
                pullToRefreshComponent.RefreshFinished(pullToRefreshComponent.GetOnUpperRefreshAction());
            }
            return true;
        }

        public bool HandleMovement(MotionEvent motionEvent,
                                   PullToRefreshComponent pullToRefreshComponent)
        {
            pullToRefreshComponent.UpdateEventStates(motionEvent);
            pullToRefreshComponent.PullDown(motionEvent, this._firstY);
            pullToRefreshComponent.ReadyToReleaseUpper(pullToRefreshComponent
                                                           .GetListTop() >
                                                       PullToRefreshComponent.MinPullElementHeight);
            return true;
        }

        #endregion
    }
}