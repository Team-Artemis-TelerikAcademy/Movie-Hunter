using MovieHunter.Api.Models;
using MovieHunter.Data;
using MovieHunter.Models;
using MovieHunter.Services;
using MovieHunter.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MovieHunter.Api.Controllers
{
    [RoutePrefix("api/my-movies")]
    public class MyMoviesController : ApiController
    {
        private IMyMoviesService myMovieService;
        private IUsersService usersService;
        private const int PageSize = 10;
        private IMoviesService moviesService;

        public MyMoviesController()
        {
            var dbContext = new MovieDbContext();
            this.myMovieService = new MyMoviesService(new EfRepository<UserMovies>(dbContext));
            this.usersService = new UsersService(new EfRepository<User>(dbContext));
            this.moviesService = new MoviesService(new EfRepository<Movie>(dbContext));
        }

        public MyMoviesController(IMyMoviesService service)
        {
            this.myMovieService = service;
        }

        [Authorize]
        public IHttpActionResult GetAll()
        {
            return this.GetAll(1);
        }

        [Authorize]
        public IHttpActionResult GetAll(int page)
        {
            var username = this.User.Identity.Name;
            var user = usersService.GetByName(username);
            var movies = myMovieService.GetAllWachedMoviesByUser(user);
            return this.Ok(movies.Skip((page-1)*PageSize).Take(PageSize).Select(UserMovieViewModel.FromUserMovie));
        }

        [Authorize]
        [Route("want-to-watch")]
        public IHttpActionResult GetWantToWachMovies()
        {
            return this.GetWantToWachMovies(1);
        }

        [Authorize]
        [Route("want-to-watch")]
        public IHttpActionResult GetWantToWachMovies(int page)
        {
            var username = this.User.Identity.Name;
            var user = usersService.GetByName(username);
            var movies = myMovieService.GetAllWachedMoviesByUser(user);
            return this.Ok(movies.Skip((page-1)*PageSize).Take(PageSize).Select(UserMovieViewModel.FromUserMovie));
        }

        [Authorize]
        [Route("watched")]
        public IHttpActionResult GetWachedMovies()
        {
            return this.GetWachedMovies(1);
        }

        [Authorize]
        [Route("want-to-watch")]
        public IHttpActionResult GetWachedMovies(int page)
        {
            var username = this.User.Identity.Name;
            var user = usersService.GetByName(username);
            var movies = myMovieService.GetAllWachedMoviesByUser(user);
            return this.Ok(movies.Skip((page-1)*PageSize).Take(PageSize).Select(UserMovieViewModel.FromUserMovie));
        }

        [Authorize]
        public IHttpActionResult PostMyMovie(MovieBindingModel movie)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            var username = this.User.Identity.Name;
            var user = usersService.GetByName(username);
            var movieToAdd = moviesService.GetById(movie.MovieId);
            this.myMovieService.Add(user, movie.MovieId, movie.State, movieToAdd);

            //api/movies/{id}
            return this.Created("api/movies/"+movie.MovieId, movie);
        }

        
    }
}
