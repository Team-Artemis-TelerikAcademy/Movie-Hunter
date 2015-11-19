using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MovieHunter.Api;

namespace MovieHunter.Tests.NetworkIntegrationTests
{
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public void TrailersShouldReturnCorrectResponse()
        {
            //using (var webApp = WebApp.Start<Startup>("http://localhost:52189"))
            //{
                using (var httpClient = new HttpClient())
                {
                    //System.Threading.Thread.Sleep(2000000);
                    httpClient.BaseAddress = new Uri("http://localhost:52189");

                    var result = httpClient.GetAsync("/api/Trailers").Result;

                    Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
                }
            //}
        }
    }
}