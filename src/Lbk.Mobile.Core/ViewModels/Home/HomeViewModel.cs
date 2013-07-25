// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeViewModel.cs" company="ip-connect GmbH">
//   Copyright (c) ip-connect GmbH. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

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
    using Lbk.Mobile.Core.ViewModels.MenuOfTheDay;
    using Lbk.Mobile.Core.ViewModels.Quiz;
    using Lbk.Mobile.Core.ViewModels.Reservation;
    using Lbk.Mobile.Core.ViewModels.Room;
    using Lbk.Mobile.Core.ViewModels.Video;

    public class HomeViewModel : BaseViewModel
    {
        private readonly IMvxTextProvider textProvider;

        public HomeViewModel(IMvxTextProvider textProvider)
        {
            this.textProvider = textProvider;
        }

        public IMvxTextProvider TextProvider
        {
            get
            {
                return textProvider;
            }
        }

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

        public ICommand ShowMenuOfTheDayCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<MenuOfTheDayViewModel>());
            }
        }

        public ICommand ShowQuizCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<QuizHomeViewModel>());
            }
        }

        public ICommand ShowReservationCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<ReservationViewModel>());
            }
        }

        public ICommand ShowRoomCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<RoomViewModel>());
            }
        }

        public ICommand ShowVideoCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<VideoViewModel>());
            }
        }

        public ICommand ShowRecommendCommand
        {
            
            get
            {
                var subject = TextProvider.GetText(Constants.GeneralNamespace, "Recommend", "MailSubject");
                var body = TextProvider.GetText(Constants.GeneralNamespace, "Recommend", "MailBody");
                return
                    new MvxCommand(
                        () =>
                        ComposeEmail("", subject, body));
            }
        }
    }
}