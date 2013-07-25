namespace Lbk.Mobile.Core
{
    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Core.ApplicationObjects;

    public class LbkApp : LbkAppBase
    {
        public LbkApp()
        {
            this.InitialiseStartNavigation();
        }

        protected sealed override void InitialiseStartNavigation()
        {
            var startApplicationObject = new AppStart(true);
            Mvx.RegisterSingleton<IMvxAppStart>(startApplicationObject);
        }
    }
}
