namespace Lbk.Mobile.UI.Droid.Controls.PullToRefresh.Library
{
    public interface IOnPullingAction
    {
        void HandlePull(bool down, int height);
    }
}