namespace Lbk.Mobile.UI.Droid.Controls.PullToRefresh
{
    using Android.Views;

    public class PullToRefreshState : IScrollingState
    {
        #region IScrollingState Members

        public bool TouchStopped(MotionEvent motionEvent, PullToRefreshComponent onTouchListener)
        {
            return false;
        }

        public bool HandleMovement(MotionEvent motionEvent,
                                   PullToRefreshComponent pullToRefreshComponent)
        {
            pullToRefreshComponent.UpdateEventStates(motionEvent);
            if (pullToRefreshComponent.IsPullingDownToRefresh())
            {
                pullToRefreshComponent.SetPullingDown(motionEvent);
                return true;
            }
            else if (pullToRefreshComponent.IsPullingUpToRefresh())
            {
                pullToRefreshComponent.SetPullingUp(motionEvent);
                return true;
            }
            return false;
        }

        #endregion
    }
}