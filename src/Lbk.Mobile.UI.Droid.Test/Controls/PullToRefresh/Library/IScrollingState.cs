namespace Lbk.Mobile.UI.Droid.Test.Controls.PullToRefresh.Library
{
    using Android.Views;

    public interface IScrollingState
    {
        bool TouchStopped(MotionEvent motionEvent,
                          PullToRefreshComponent pullToRefreshComponent);

        bool HandleMovement(MotionEvent motionEvent,
                            PullToRefreshComponent pullToRefreshComponent);
    }
}