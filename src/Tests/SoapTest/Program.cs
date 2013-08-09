using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoapTest
{
    //using Lbk.Mobile.Data.Service.LbkMobileService;
    //using Lbk.Mobile.Data.Service.Service;

    using System.Diagnostics;
    using System.Net.Http;

    using ServiceClient;
    using ServiceClient.Pcl;

    using SoapTest.MyService3;

    class Program
    {
        static void Main(string[] args)
        {

           

           

            //var servicePcl = new ServiceTestPcl();
            //servicePcl.GetEventsAsync("test");

           //ServiceTest();

            //GetTest();

            TestLocalService();

            Console.ReadLine();
        }


        private static async void TestLocalService()
        {
            var sercice = new Service1SoapClient();
            var test = await sercice.EventAsyncTask("test");
        }

        private static void ServiceTest()
        {
            //var sercice = new LbkMobileService();
            //var task = sercice.GetEventsAsync("test");

            //Task.Factory.StartNew(() => DoSomething(item));

            //if (task == await Task.WhenAny(task, Task.Delay(5000)))
            //{
            //    var test = await task;
            //    Console.WriteLine(test.Result.Length);
            //}
            //else Console.WriteLine("Timed out");



          

        }

        private static async void GetTest()
        {
            Task<string> getWebPageTask = GetWebPageAsync("http://msdn.microsoft.com");

            Console.WriteLine("In startButton_Click before await");
            string webText = await getWebPageTask;
            Console.WriteLine("Characters received: " + webText.Length.ToString());
        }

        private static async Task<string> GetWebPageAsync(string url)
        {
            // Start an async task. 
            Task<string> getStringTask = (new HttpClient()).GetStringAsync(url);

            // Await the task. This is what happens: 
            // 1. Execution immediately returns to the calling method, returning a 
            //    different task from the task created in the previous statement. 
            //    Execution in this method is suspended. 
            // 2. When the task created in the previous statement completes, the 
            //    result from the GetStringAsync method is produced by the Await 
            //    statement, and execution continues within this method. 
            Console.WriteLine("In GetWebPageAsync before await");
            string webText = await getStringTask;
            Console.WriteLine("In GetWebPageAsync after await");

            return webText;
        }

        private static void ServiceLocalOnGetEventsCompleted(object sender, GetEventsCompletedEventArgs getEventsCompletedEventArgs)
        {
            var test = getEventsCompletedEventArgs;
        }
    }
}
