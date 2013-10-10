//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="InstructionsView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Quiz
{
    using Android.App;
    using Android.OS;

    using Lbk.Mobile.Core.ViewModels.Quiz;

    [Activity(Label = "Spielanleitung", Icon = "@drawable/ic_launcher")]
    public class InstructionsView : BaseView<InstructionsViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Quiz_Instructions);
        }
    }
}