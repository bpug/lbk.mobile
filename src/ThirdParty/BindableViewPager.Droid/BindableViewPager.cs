// BindableViewPager.cs
// (c) Copyright 2013 Tomasz Cielecki http://ostebaronen.dk
//
// Derivative of BindableViewPager.cs by Steve Dunford
// http://slodge.blogspot.com/2013/02/binding-to-androids-horizontal-pager.html
// which is a derivative of MvxBindableListView.cs from the
// MvvmCross project by Stuart Lodge
// https://github.com/slodge/MvvmCross/blob/vnext/Cirrious/Cirrious.MvvmCross.Binding.Droid/Views/MvxBindableListView.cs
//
// Licensed using Microsoft Public License (Ms-PL)
// Full License description can be found in the LICENSE
// file.
// 
// Project Lead - Tomasz Cielecli, @Cheesebaron, tomasz@ostebaronen.dk

using System.Collections;
using System.Windows.Input;
using Android.Content;
using Android.Util;
using Cirrious.MvvmCross.Binding.Attributes;
using Cirrious.MvvmCross.Binding.Droid.Views;

namespace Cheesebaron.MvvmCross.Bindings.Droid
{
   

    using Android.Views;

    using Java.Lang;

    public class BindableViewPager
        : Android.Support.V4.View.ViewPager
    {
        public BindableViewPager(Context context, IAttributeSet attrs)
            : this(context, attrs, new MvxBindablePagerAdapter(context))
        { }

        public BindableViewPager(Context context, IAttributeSet attrs, MvxBindablePagerAdapter adapter)
            : base(context, attrs)
        {
            var itemTemplateId = MvxAttributeHelpers.ReadListItemTemplateId(context, attrs);
            adapter.ItemTemplateId = itemTemplateId;
            Adapter = adapter;
        }

        public new MvxBindablePagerAdapter Adapter
        {
            get { return base.Adapter as MvxBindablePagerAdapter; }
            set
            {
                var existing = Adapter;
                if (existing == value)
                    return;

                if (existing != null && value != null)
                {
                    value.ItemsSource = existing.ItemsSource;
                    value.ItemTemplateId = existing.ItemTemplateId;
                }

                base.Adapter = value;
            }
        }

        [MvxSetToNullAfterBinding]
        public IEnumerable ItemsSource
        {
            get { return Adapter.ItemsSource; }
            set { Adapter.ItemsSource = value; }
        }

        public int ItemTemplateId
        {
            get { return Adapter.ItemTemplateId; }
            set { Adapter.ItemTemplateId = value; }
        }

        private ICommand _itemPageSelected;
        public ICommand ItemPageSelected
        {
            get { return _itemPageSelected; }
            set { _itemPageSelected = value; if (_itemPageSelected != null) EnsureItemPageSelectedOverloaded(); }
        }

        private bool _itemPageSelectedOverloaded;
        private void EnsureItemPageSelectedOverloaded()
        {
            if (_itemPageSelectedOverloaded)
                return;

            _itemPageSelectedOverloaded = true;
            base.PageSelected += (sender, args) => ExecuteCommandOnItem(ItemPageSelected, args.Position);
        }

        protected virtual void ExecuteCommandOnItem(ICommand command, int position)
        {
            if (command == null)
                return;

            var item = Adapter.GetRawItem(position);
            if (item == null)
                return;

            if (!command.CanExecute(item))
                return;

            command.Execute(item);
        }

        private ICommand _pageSelected;
        public new ICommand PageSelected
        {
            get { return _pageSelected; }
            set { _pageSelected = value; if (_pageSelected != null) EnsurePageSelectedOverloaded(); }
        }

        private bool _pageSelectedOverloaded;
        private void EnsurePageSelectedOverloaded()
        {
            if (_pageSelectedOverloaded)
                return;

            _pageSelectedOverloaded = true;
            base.PageSelected += (sender, args) => ExecuteCommand(PageSelected, args.Position);
        }

        protected virtual void ExecuteCommand(ICommand command, int toPage)
        {
            if (command == null)
                return;

            if (!command.CanExecute(toPage))
                return;

            command.Execute(toPage);
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);

            // find the first child view
            View view = GetChildAt(0);

            if (view != null)
            {
                // measure the first child view with the specified measure spec
                view.Measure(widthMeasureSpec, heightMeasureSpec);
            }

            SetMeasuredDimension(MeasuredWidth, MeasureHeight(heightMeasureSpec, view));
        }

        /**
     * Determines the height of this view
     *
     * @param measureSpec A measureSpec packed into an int
     * @param view the base view with already measured height
     *
     * @return The height of the view, honoring constraints from measureSpec
     */
        private int MeasureHeight(int measureSpec, View view)
        {
            int result = 0;
            var specMode = MeasureSpec.GetMode(measureSpec);
            int specSize = MeasureSpec.GetSize(measureSpec);

            if (specMode == MeasureSpecMode.Exactly)
            {
                result = specSize;
            }
            else
            {
                // set the height from the base view if available
                if (view != null)
                {
                    result = view.MeasuredHeight;
                }
                if (specMode == MeasureSpecMode.AtMost)
                {
                    result = Math.Min(result, specSize);
                }
            }
            return result;
        }
    } 
}