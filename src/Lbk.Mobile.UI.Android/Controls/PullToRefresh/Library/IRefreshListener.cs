namespace Lbk.Mobile.UI.Droid.Controls.PullToRefresh.Library
{
    public interface IRefreshListener
    {
        void DoRefresh();
        void RefreshFinished();
    }
}