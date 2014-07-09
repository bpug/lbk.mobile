namespace Lbk.Mobile.UI.Droid.Test.Controls.PullToRefresh.Library
{
    public interface IOnPullingAction
    {
        void HandlePull(bool down, int height);
    }
}