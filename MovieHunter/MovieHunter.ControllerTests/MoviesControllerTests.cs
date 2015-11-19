using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTested.WebApi;
using MovieHunter.Api;
using MovieHunter.Api.App_Start;
using Moq;
using MovieHunter.Models;
using MovieHunter.Services.Contracts;
using FLExtensions.Common;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using MyTested;
using MovieHunter.Api.Controllers;

namespace MovieHunter.ControllerTests
{
    [TestClass]
    public class MoviesControllerTests
    {
        private static Random rng = new Random();

        [ClassInitialize]
        public static void ClassInit(TestContext testCtx)
        {
            NinjectWebCommon.DbBindings = k => 
            {
                var mockUserRepo = new Mock<IRepository<Movie>>();
                mockUserRepo.Setup(x => x.All()).Returns(() => 
                {
                    var result = new List<Movie>()
                    {
                        
                    };
                    for (int i = 0; i < 10; i++)
                    {
                        result.Add(new Movie() {
                            Description = rng.NextString(20, 60),
                            Id = rng.Next(),
                            ImageUrl = rng.NextString(30, 40),
                            ReleaseDate = DateTime.Now,
                            Title = rng.NextString(10, 15)
                        });
                    }

                    return result.AsQueryable();
                });
            };

            MyWebApi.IsRegisteredWith(WebApiConfig.Register);
        }

        [TestMethod]
        public void GetWithoutParametersShouldMapToGetAll()
        {
            MyWebApi
                .Routes()
                .ShouldMap(req => req
                                    .WithRequestUri("http://localhost:52189/api/Movies")
                                    .WithMethod("GET")   
                )
                .To<MoviesController>(c => c.GetAll());
        }

        [TestMethod]
        public void GetAllShouldReturnAll()
        {
            MyWebApi
                .Controller<MoviesController>()
                .Calling(c => c.GetAll())
                .ShouldReturn()
                .Json();
        }
    }
}
