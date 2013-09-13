namespace Lbk.Mobile.UI.Droid.Controls.PullToRefresh
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