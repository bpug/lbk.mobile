using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lbk.Mobile.Core.Test.Mocks
{
    using Cirrious.MvvmCross.ViewModels;
    using Cirrious.MvvmCross.Views;

    public class MockMvxViewDispatcher : IMvxViewDispatcher
    {
        public List<IMvxViewModel> CloseRequests = new List<IMvxViewModel>();
        public List<MvxShowViewModelRequest> NavigateRequests = new List<MvxShowViewModelRequest>();

        #region IMvxViewDispatcher implementation

        public bool RequestNavigate(MvxShowViewModelRequest request)
        {
            NavigateRequests.Add(request);
            return true;
        }

        public bool RequestClose(IMvxViewModel whichViewModel)
        {
            CloseRequests.Add(whichViewModel);
            return true;
        }

        public bool RequestRemoveBackStep()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IMvxMainThreadDispatcher implementation

        public bool RequestMainThreadAction(Action action)
        {
            action();
            return true;
        }

        #endregion
    }
}
