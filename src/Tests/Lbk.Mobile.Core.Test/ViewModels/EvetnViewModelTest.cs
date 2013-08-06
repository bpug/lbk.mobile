using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lbk.Mobile.Core.Test.ViewModels
{
    using Cirrious.MvvmCross.Plugins.DeviceIdentifier.Interfaces;

    using Lbk.Mobile.Core.ViewModels.Event;
    using Lbk.Mobile.Data.Service.Interfaces;
    using Lbk.Mobile.Data.Service.Service;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class EvetnViewModelTest : TestBase
    {
        [Test]
        public async void LodEvents()
        {
            Action<string> onDeviceUidSuccess = null;
            Action<Exception> onDeviceUidError = null;

            var mock = new Mock<IDeviceUidService>();
            mock.Setup(s => s.GetDeviceUid(It.IsAny<Action<string>>(), It.IsAny<Action<Exception>>()))
                .Callback(
                    (Action<string> id, Action<Exception> error) =>
                    {
                        onDeviceUidSuccess = id;
                        onDeviceUidError = error;
                    });
            
            var service = new LbkMobileService(mock.Object);

            var eventViewModel = new EventListViewModel(service);
            onDeviceUidSuccess(Constants.DeviceUidTest);

            //var result = await eventViewModel.service.GetEventsAsync();

            //eventViewModel.LoadCommand.Execute(null);

            var test = eventViewModel.Events;

            
        }
    }
}
