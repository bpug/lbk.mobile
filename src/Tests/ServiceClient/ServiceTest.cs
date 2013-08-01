using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClient
{
    using ServiceClient.MyService;

    public class ServiceTest
    {
        public void GetEventsAsync(string fingerprint)
        {

            var servise = new MyService.Service1SoapClient();
            servise.GetEventsCompleted += ServiseOnGetEventsCompleted;
            servise.GetEventsAsync("test");
        }

        private void ServiseOnGetEventsCompleted(object sender, GetEventsCompletedEventArgs getEventsCompletedEventArgs)
        {
            var test = getEventsCompletedEventArgs;
        }
    }
}
