namespace Lbk.Mobile.UI.Droid.Controls.PullToRefresh
{
    public interface IRefreshListener
    {
        void DoRefresh();
        void RefreshFinished();
    }
}