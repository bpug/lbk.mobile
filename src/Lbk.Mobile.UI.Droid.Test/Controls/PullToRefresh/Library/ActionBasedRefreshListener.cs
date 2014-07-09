namespace Lbk.Mobile.UI.Droid.Test.Controls.PullToRefresh.Library
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