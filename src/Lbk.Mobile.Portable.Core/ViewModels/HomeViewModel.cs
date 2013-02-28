// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeViewModel.cs" company="ip-connect GmbH">
//   Copyright (c) ip-connect GmbH. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Portable.Core.ViewModels
{
    using System.Windows.Input;

    using Cirrious.MvvmCross.Commands;
    using Cirrious.MvvmCross.ViewModels;

    using Lbk.Mobile.Portable.Core.ViewModels.Contact;
    using Lbk.Mobile.Portable.Core.ViewModels.Event;
    using Lbk.Mobile.Portable.Core.ViewModels.Gallery;
    using Lbk.Mobile.Portable.Core.ViewModels.History;
    using Lbk.Mobile.Portable.Core.ViewModels.Menu;
    using Lbk.Mobile.Portable.Core.ViewModels.MenuOfTheDay;
    using Lbk.Mobile.Portable.Core.ViewModels.Quiz;
    using Lbk.Mobile.Portable.Core.ViewModels.Reservation;
    using Lbk.Mobile.Portable.Core.ViewModels.Room;
    using Lbk.Mobile.Portable.Core.ViewModels.Video;

    public class HomeViewModel : MvxViewModel
    {
        public ICommand ShowContactCommand
        {
            get
            {
                return new MvxRelayCommand(() => this.RequestNavigate<ContactViewModel>());
            }
        }

        public ICommand ShowEventCommand
        {
            get
            {
                return new MvxRelayCommand(() => this.RequestNavigate<EventListViewModel>());
            }
        }

        public ICommand ShowGalleryCommand
        {
            get
            {
                return new MvxRelayCommand(() => this.RequestNavigate<GalleryViewModel>());
            }
        }

        public ICommand ShowHistoryCommand
        {
            get
            {
                return new MvxRelayCommand(() => this.RequestNavigate<HistoryViewModel>());
            }
        }

        public ICommand ShowMenuCommand
        {
            get
            {
                return new MvxRelayCommand(() => this.RequestNavigate<MenuViewModel>());
            }
        }

        public ICommand ShowMenuOfTheDayCommand
        {
            get
            {
                return new MvxRelayCommand(() => this.RequestNavigate<MenuOfTheDayViewModel>());
            }
        }

        public ICommand ShowQuizCommand
        {
            get
            {
                return new MvxRelayCommand(() => this.RequestNavigate<QuizHomeViewModel>());
            }
        }

        public ICommand ShowReservationCommand
        {
            get
            {
                return new MvxRelayCommand(() => this.RequestNavigate<ReservationViewModel>());
            }
        }

        public ICommand ShowRoomCommand
        {
            get
            {
                return new MvxRelayCommand(() => this.RequestNavigate<RoomViewModel>());
            }
        }

        public ICommand ShowVideoCommand
        {
            get
            {
                return new MvxRelayCommand(() => this.RequestNavigate<VideoViewModel>());
            }
        }
    }
}