namespace Lbk.Mobile.UI.Android.Controls.PullToRefresh
{
    using global::Android.Views;

    public interface IScrollingState
    {
        bool TouchStopped(MotionEvent motionEvent,
                          PullToRefreshComponent pullToRefreshComponent);

        bool HandleMovement(MotionEvent motionEvent,
                            PullToRefreshComponent pullToRefreshComponent);
    }
}