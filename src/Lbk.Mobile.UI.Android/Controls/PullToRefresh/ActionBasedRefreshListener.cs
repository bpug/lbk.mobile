namespace Lbk.Mobile.UI.Droid.Controls.PullToRefresh
{
    using System;

    public class ActionBasedRefreshListener : IRefreshListener
    {
        private readonly Action _action;

        public ActionBasedRefreshListener(Action action)
        {
            this._action = action;
        }

        #region IRefreshListener Members

        public void DoRefresh()
        {
            this._action();
        }

        public void RefreshFinished()
        {
        }

        #endregion
    }
}