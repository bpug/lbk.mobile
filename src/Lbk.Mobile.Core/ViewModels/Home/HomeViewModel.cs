//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="HomeViewModel.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Core.ViewModels.Home
{
    using System.Windows.Input;

    using Cirrious.MvvmCross.Localization;
    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Core.ViewModels.Contact;
    using Lbk.Mobile.Core.ViewModels.Event;
    using Lbk.Mobile.Core.ViewModels.Gallery;
    using Lbk.Mobile.Core.ViewModels.History;
    using Lbk.Mobile.Core.ViewModels.Menu;
    using Lbk.Mobile.Core.ViewModels.Quiz;
    using Lbk.Mobile.Core.ViewModels.Reservation;
    using Lbk.Mobile.Core.ViewModels.Room;
    using Lbk.Mobile.Core.ViewModels.TodaysMenu;
    using Lbk.Mobile.Core.ViewModels.Video;

    public class HomeViewModel : BaseViewModel
    {

        public ICommand ShowContactCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<ContactViewModel>());
            }
        }

        public ICommand ShowEventCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<EventListViewModel>());
            }
        }

        public ICommand ShowGalleryCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<GalleryViewModel>());
            }
        }

        public ICommand ShowHistoryCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<HistoryViewModel>());
            }
        }

        public ICommand ShowMenuCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<MenuViewModel>());
            }
        }

        public ICommand ShowTodaysMenuCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<TodaysMenuViewModel>());
            }
        }

        public ICommand ShowQuizCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<Quiz.QuizStartViewModel>());
            }
        }

        public ICommand ShowRecommendCommand
        {
            get
            {
                //string subject = this.TextProvider.GetText(Constants.GeneralNamespace, "Recommend", "Recommend.MailSubject");
                string subject = this.TextSource.GetText("Recommend.MailSubject");
                string body = this.TextSource.GetText("Recommend.MailBody");
                return new MvxCommand(() => this.ComposeEmail("", subject, body));
            }
        }


        public ICommand ShowFacebookCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowWebPage(Constants.FacebookUrl));
            }
        }

        

        public ICommand ShowReservationCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<ReservationFormViewModel>());
            }
        }

        public ICommand ShowRoomCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<RoomListViewModel>());
            }
        }

        public ICommand ShowVideoCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<VideoListViewModel>());
            }
        }

        
    }
}