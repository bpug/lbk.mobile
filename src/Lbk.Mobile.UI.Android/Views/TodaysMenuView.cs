//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="TodaysMenuView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Dialog.Droid;
using Cirrious.MvvmCross.Dialog.Droid.Views;
using CrossUI.Droid.Dialog.Elements;

namespace Lbk.Mobile.UI.Android.Views
{
   
    [Activity(Label = "Tageskarte")]
    public class TodaysMenuView : MvxDialogActivity //BaseDialogView<TodaysMenuViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var root = new RootElement(DateTime.Now.ToShortDateString())
            {
                new Section("Section1")
                {
                    new StringElement("Row1"),
                    new StringElement("Row2"),
                    new StringElement("Row3"),
                    new StringElement("Row4"),
                    new StringElement("Row5"),
                }
            };

            this.Root = root;
            //Root = new RootElement("The Dialog")
            //    {
            //        new Section("Strings")
            //            {
            //                new EntryElement("Hello").Bind(this, "Value Hello"),
            //                new EntryElement("Hello2").Bind(this, "Value Hello2"),
            //                new StringElement("Test").Bind(this, "Value Combined"),
            //                new BooleanElement("T or F", false).Bind(this, "Value Option1"),
            //                new BooleanElement("T or F:", true).Bind(this, "Value Option1"),
            //            },
            //        new Section("Dates")
            //            {
            //                new DateElement("The Date", DateTime.Today).Bind(this, "Value TheDate"),
            //                new TimeElement("The Time", DateTime.Today).Bind(this, "Value TheDate"),
            //                new StringElement("Actual").Bind(this, "Value TheDate")
            //            }
            //    };
        }

        //protected override void OnCreate(Bundle bundle)
        //{
        //    //his.Title = DateTime.Now.ToShortDateString();
        //    base.OnCreate(bundle);
        //    //this.SetContentView(Resource.Layout.TodaysMenu);

        //    //var bindings = this.CreateInlineBindingTarget<TodaysMenuViewModel>();

        //    var viewModel = ViewModel as TodaysMenuViewModel;
        //    if (viewModel != null)
        //    {
        //        viewModel.PropertyChanged += (sender, args) =>
        //        {
        //            var vm = (TodaysMenuViewModel)sender;
        //            switch (args.PropertyName)
        //            {
        //                case "MenuCategories":
        //                    BindDishes(vm.MenuCategories);
        //                    break;
        //            }
        //        };
        //    }
        //}

        //private void BindDishes(List<category> categories)
        //{
        //    //var categories = this.ViewModel.MenuCategories;
        //    //var root = new RootElement(Title){
        //    //    from cat in categories
        //    //        select new Section (cat.Title, cat.Subtitle){
        //    //            from  dish in cat.Dishes select (Element)new StringElement(dish.Description)
        //    //        }
        //    //};

        //    var root = new RootElement(DateTime.Now.ToShortDateString()){
        //        new Section("Section1")
        //        {
        //            new StringElement("Row1"),
        //            new StringElement("Row2"),
        //            new StringElement("Row3"),
        //            new StringElement("Row4"),
        //            new StringElement("Row5"),
        //        }
        //    };

        //    Root = root;
        //}
    }
}