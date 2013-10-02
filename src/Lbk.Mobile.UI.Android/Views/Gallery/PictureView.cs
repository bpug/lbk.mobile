//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="PictureView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Gallery
{
    using Android.App;
    using Android.Content.PM;
    using Android.Content.Res;
    using Android.OS;
    using Android.Views;
    using Android.Widget;

    using Cheesebaron.MvvmCross.Bindings.Droid;

    using Cirrious.MvvmCross.Binding.BindingContext;

    using DK.Ostebaronen.Droid.ViewPagerIndicator;

    using Lbk.Mobile.Core.ViewModels.Gallery;
    using Lbk.Mobile.UI.Droid.Controls;

    using Resource = Lbk.Mobile.UI.Droid.Resource;

    [Activity(Icon = "@drawable/ic_launcher",
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class PictureView : BaseView<PictureViewModel>
    {
        private const int Tolerance = 50;

        private TextView description;

        private bool isVisible = true;

        private BindableViewPager pager;

        private TextView title;

        private float touchPointX;

        private float touchPointY;

        public override bool DispatchTouchEvent(MotionEvent ev)
        {
            //Trace.Info("DispatchTouchEvent: " + ev.Action);
            switch (ev.Action)
            {
                case MotionEventActions.Down:
                    this.touchPointX = ev.GetX();
                    this.touchPointY = ev.GetY();
                    break;
                case MotionEventActions.Up:
                    bool sameX = this.touchPointX + Tolerance > ev.GetX() && this.touchPointX - Tolerance < ev.GetX();
                    bool sameY = this.touchPointY + Tolerance > ev.GetY() && this.touchPointY - Tolerance < ev.GetY();
                    if (sameX && sameY)
                    {
                        ExpandCollapseAnimation titleAnimation;
                        ExpandCollapseAnimation descrAnimation;
                        const int Duration = 300;

                        if (this.isVisible)
                        {
                            titleAnimation = new ExpandCollapseAnimation(
                                this.title,
                                Duration,
                                ExpandCollapseAnimation.Type.Collapse,
                                this);
                            descrAnimation = new ExpandCollapseAnimation(
                                this.description,
                                Duration,
                                ExpandCollapseAnimation.Type.Collapse,
                                this);
                        }
                        else
                        {
                            titleAnimation = new ExpandCollapseAnimation(
                                this.title,
                                Duration,
                                ExpandCollapseAnimation.Type.Expand,
                                this);
                            descrAnimation = new ExpandCollapseAnimation(
                                this.description,
                                Duration,
                                ExpandCollapseAnimation.Type.Expand,
                                this);
                        }

                        this.isVisible = !this.isVisible;

                        this.title.StartAnimation(titleAnimation);
                        this.description.StartAnimation(descrAnimation);
                    }
                    break;
            }
            return base.DispatchTouchEvent(ev);
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            if (!this.isVisible)
            {
                this.title.Visibility = ViewStates.Gone;
                this.description.Visibility = ViewStates.Gone;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            this.RequestWindowFeature(WindowFeatures.NoTitle);
            //this.Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.Gallery_Picture);

            this.title = this.FindViewById<TextView>(Resource.Id.PictureTitle);
            this.description = this.FindViewById<TextView>(Resource.Id.PictureDescription);

            this.pager = this.FindViewById<BindableViewPager>(Resource.Id.picture_pager);

            var indicator = this.FindViewById<UnderlinePageIndicator>(Resource.Id.picture_pager_indicator);
            indicator.SetViewPager(this.pager);
            indicator.PageSelected +=
                (sender, args) => { this.ViewModel.Current = this.ViewModel.Pictures[args.Position]; };

            var set = this.CreateBindingSet<PictureView, PictureViewModel>();
            set.Bind(indicator).For(v => v.CurrentItem).To(vm => vm.Current.Index);
            set.Apply();

           
        }
    }
}