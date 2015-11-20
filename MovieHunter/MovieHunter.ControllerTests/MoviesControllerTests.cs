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
using MovieHunter.ControllerTests.Mocks;
using MovieHunter.Services;
using MovieHunter.Api.Models;
using MyTested.WebApi.Builders.Contracts.Controllers;

namespace MovieHunter.ControllerTests
{
    [TestClass]
    public class MoviesControllerTests
    {
        private static Random rng = new Random();

        private static IAndControllerBuilder<MoviesController> moviesController;

        [ClassInitialize]
        public static void ClassInit(TestContext testCtx)
        {
            NinjectWebCommon.DbBindings = k =>
            {
                k.Bind<IRepository<Movie>>().ToConstant(Repositories.Movies);
            };

            MyWebApi.IsRegisteredWith(WebApiConfig.Register);
        }

        [TestInitialize]
        public void TestInit()
        {
            moviesController = MyWebApi
                .Controller<MoviesController>()
                .WithResolvedDependencies(new MoviesService(Repositories.Movies), new UsersService(Repositories.Users));
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
        public void GetShouldReturnMoviesFromRepoWithPaging()
        {
            var movies = Repositories.Movies.All();

            moviesController
            .Calling(x => x.GetAll())
            .ShouldReturn()
            .Ok()
            .WithResponseModelOfType<IQueryable<MovieDetailViewModel>>()
            .Passing(x =>
            {
                x.ForEach(m =>
                {
                    Assert.IsTrue(movies.Any(movie => movie.Title == m.Title));
                });

                var moviesCount = movies.Count();
                Assert.AreEqual(moviesCount > 10 ? 10 : moviesCount, x.Count());
            });
        }

        [TestMethod]
        public void GetByIdShouldReturnMovieWithCorrespondingId()
        {
            var second = Repositories.Movies.All().ToList()[1];

            moviesController
                .Calling(x => x.GetById(2))
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<MovieDetailViewModel>()
                .Passing(x => Assert.AreEqual(x.Title, second.Title));
        }

        [TestMethod]
        public void GetByGenreShouldReturnOnlyMovieWithThatGenre()
        {
            var movies = Repositories.Movies.All().SelectMany(x => x.Genres).ToList();

            moviesController
                .Calling(x => x.GetByGenre("Vampire romance"))
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<IQueryable<MovieDetailViewModel>>()
                .Passing(x =>
                {
                    Assert.IsTrue(x.Any(y => y.Genres.Any(g => g == "Vampire romance")));
                });
        }

        [TestMethod]
        public void GetReleasedMoviesShouldRetunMoviesCorrectDate()
        {
            moviesController
                .Calling(x => x.GetAllReleasedMovies())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<IQueryable<MovieDetailViewModel>>()
                .Passing(x => 
                {
                    x.ForEach(y => Assert.IsTrue(y.ReleaseDate < DateTime.Now));
                });
        }

        [TestMethod]
        public void GetComingSoonMoviesShouldReturnReponseWithCorrectDates()
        {
            moviesController
                .Calling(x => x.GetAllCommingSoonMovies())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<IQueryable<MovieDetailViewModel>>()
                .Passing(x =>
                {
                    x.ForEach(y => Assert.IsTrue(y.ReleaseDate >= DateTime.Now));
                });
        }
    }
}
