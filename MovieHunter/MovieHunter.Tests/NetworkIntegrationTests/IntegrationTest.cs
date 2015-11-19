using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MovieHunter.Api;
using MovieHunter.Tests.TestObjects;
using MovieHunter.Services.Contracts;
using MovieHunter.Api.Controllers;
using System.Threading;
using System.Threading.Tasks;
using MovieHunter.Data;
using MovieHunter.Models;

namespace MovieHunter.Tests.NetworkIntegrationTests
{
    [TestClass]
    public class IntegrationTests
    {
        private static MovieDbContext db;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            db = new MovieDbContext(MovieDbContext.TestConnection);
            db.Database.Delete();
            db.Users.Add(new User()
            {
                UserName = "Test User"
            });
            db.SaveChanges();
        }

        [TestMethod]
        public void TrailersShouldReturnCorrectResponse()
        {
            using (var webApp = WebApp.Start<Startup>("http://moviehunterproject.azurewebsites.net"))
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("http://moviehunterproject.azurewebsites.net");

                    var result = httpClient.GetAsync("/api/Trailers").Result;

                    Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
                }
            }
        }

        [TestMethod]
        public void MoviesShouldReturnCorrectResponse()
        {
            using (var webApp = WebApp.Start<Startup>("http://moviehunterproject.azurewebsites.net"))
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("http://moviehunterproject.azurewebsites.net");

                    var result = httpClient.GetAsync("/api/Movies").Result;

                    Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
                }
            }
        }

        [TestMethod]
        public void ActorsShouldReturnCorrectResponse()
        {
            using (var webApp = WebApp.Start<Startup>("http://moviehunterproject.azurewebsites.net"))
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("http://moviehunterproject.azurewebsites.net");

                    var result = httpClient.GetAsync("/api/Actors").Result;

                    Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
                }
            }
        }

        [TestMethod]
        public void CommentsShouldReturnCorrectResponse()
        {
            using (var webApp = WebApp.Start<Startup>("http://moviehunterproject.azurewebsites.net"))
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("http://moviehunterproject.azurewebsites.net");

                    var result = httpClient.GetAsync("/api/movies/1/Comments").Result;

                    Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
                }
            }
        }

        [TestMethod]
        public void GenresShouldReturnCorrectResponse()
        {
            using (var webApp = WebApp.Start<Startup>("http://moviehunterproject.azurewebsites.net"))
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("http://moviehunterproject.azurewebsites.net");

                    var result = httpClient.GetAsync("/api/Genres").Result;

                    Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
                }
            }
        }

        [TestMethod]
        public async Task MyMoviesShouldReturnCorrectResponse()
        {
            var controller = new MyMoviesController(db);
            var request = new HttpRequestMessage();
            controller.Request = request;
            controller.Configuration = new HttpConfiguration();
            controller.User = new MockedPrincipal();
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            var result = controller.GetAll();
            var response = await result.ExecuteAsync(token);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [ClassCleanup]
        public static void CleanUp()
        {
            db.Database.Delete();
            db.Dispose();
        }
    }
}