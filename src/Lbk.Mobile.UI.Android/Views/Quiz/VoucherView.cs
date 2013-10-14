//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuizStartView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Quiz
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;

    using Lbk.Mobile.Core.ViewModels.Quiz;

    [Activity(Label = "Bavaria Quiz Gaudi", Icon = "@drawable/ic_launcher")]
    public class VoucherView : BaseView<VoucherViewModel>
    {
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Quiz_Start);
        }
    }
}