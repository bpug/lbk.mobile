namespace Lbk.Mobile.UI.Android.Controls.PullToRefresh
{
    public interface IOnPullingAction
    {
        void HandlePull(bool down, int height);
    }
}