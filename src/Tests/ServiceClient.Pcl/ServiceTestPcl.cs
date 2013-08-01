namespace ServiceClient.Pcl
{
    using System;

    using ServiceClient.Pcl.Hampel;
    using ServiceClient.Pcl.MyService;
    //using bpwsalib01.ServiceReference1;

    public class ServiceTestPcl
    {
        public void GetEventsAsync(string fingerprint)
        {

            var servise = new Service1SoapClient();
            servise.GetEventsCompleted += ServiseOnGetEventsCompleted;
            servise.GetEventsAsync("test");

            var serviceHampel = new Hampel.ArticleWebServiceSoapClient();
            serviceHampel.getArtilceCompleted += ServiceHampelOnGetArtilceCompleted;
            serviceHampel.getArtilceAsync();
        }

        private void ServiceHampelOnGetArtilceCompleted(object sender, getArtilceCompletedEventArgs getArtilceCompletedEventArgs)
        {
            var test = getArtilceCompletedEventArgs;
        }

        private void ServiseOnGetVideosCompleted(object sender, GetVideosCompletedEventArgs getVideosCompletedEventArgs)
        {
            var test = getVideosCompletedEventArgs;
        }

        private void ServiseOnGetEventsCompleted(object sender, GetEventsCompletedEventArgs getEventsCompletedEventArgs)
        {
            var test = getEventsCompletedEventArgs;
        }
    }
}
