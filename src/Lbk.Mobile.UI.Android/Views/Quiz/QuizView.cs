//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="QuizView.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.UI.Droid.Views.Quiz
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Android.Views;

    using Lbk.Mobile.Core.ViewModels.Quiz;

    [Activity(Label = "Bavaria Quiz Gaudi", Icon = "@drawable/ic_launcher", NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class QuizView : BaseFragmentActivity<QuizViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Quiz_Page);

            var question = new QuestionFragment
            {
                ViewModel = this.ViewModel.QuestionViewModel
            };

            var trans = this.SupportFragmentManager.BeginTransaction();
            trans.Replace(Resource.Id.question_holder, question);
            trans.Commit();
        }
       

        public override bool OnKeyDown(Android.Views.Keycode keyCode, Android.Views.KeyEvent e)
        {
            if (keyCode == Keycode.Back)
            {

                this.ViewModel.AbortCommand.Execute(null);
                return false;

            }
            return base.OnKeyDown(keyCode, e);
        }
    }
}