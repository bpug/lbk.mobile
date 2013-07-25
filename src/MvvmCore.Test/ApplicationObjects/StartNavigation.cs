using Cirrious.MvvmCross.Interfaces.ViewModels;
using Cirrious.MvvmCross.ViewModels;
using MvvmCore.Test.ViewModels;

namespace MvvmCore.Test.ApplicationObjects
{
    public class StartNavigation
        : MvxApplicationObject
        , IMvxStartNavigation
    {
        public void Start()
        {
            RequestNavigate<HomeViewModel>();
        }

        public bool ApplicationCanOpenBookmarks
        {
            get { return true; }
        }
    }
}
