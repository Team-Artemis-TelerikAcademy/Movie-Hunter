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

        //public MyMoviesController() : this(new MovieDbContext())
        //{
        //}

        //public MyMoviesController(MovieDbContext dbContext)
        //{
        //    this.myMovieService = new MyMoviesService(new EfRepository<UserMovies>(dbContext));
        //    this.usersService = new UsersService(new EfRepository<User>(dbContext));
        //}

        public MyMoviesController(IMyMoviesService service, IUsersService users)
        {
            this.myMovieService = service;
            this.usersService = users;
        }

        [Authorize]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return this.GetAll(1);
        }

        [Authorize]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            var username = this.User.Identity.Name;
            var user = usersService.GetByName(username);
            return this.Ok(UserMovieViewModel.FromMovie.Compile()
                                                         .Invoke(this.myMovieService
                                                                       .GetMovieById(id, user)));
        }

        [Authorize]
        [Route("")]
        public IHttpActionResult GetAll(int page)
        {
            var username = this.User.Identity.Name;
            var user = usersService.GetByName(username);
            var movies = myMovieService.GetAllMoviesByUser(user);
            return this.Ok(movies.Skip((page - 1) * PageSize).Take(PageSize).Select(UserMovieViewModel.FromUserMovie));
        }

        [Authorize]
        [Route("want-to-watch")]
        public IHttpActionResult GetWantToWatchMovies()
        {
            return this.GetWantToWatchMovies(1);
        }

        [Authorize]
        [Route("want-to-watch")]
        public IHttpActionResult GetWantToWatchMovies(int page)
        {
            var username = this.User.Identity.Name;
            var user = usersService.GetByName(username);
            var movies = myMovieService.GetAllWantToWatchMoviesByUser(user);
            return this.Ok(movies.Skip((page - 1) * PageSize).Take(PageSize).Select(UserMovieViewModel.FromUserMovie));
        }

        [Authorize]
        [Route("watched")]
        public IHttpActionResult GetWatchedMovies()
        {
            return this.GetWatchedMovies(1);
        }

        [Authorize]
        [Route("watched")]
        public IHttpActionResult GetWatchedMovies(int page)
        {
            var username = this.User.Identity.Name;
            var user = usersService.GetByName(username);
            var movies = myMovieService.GetAllWatchedMoviesByUser(user);
            return this.Ok(movies.Skip((page - 1) * PageSize).Take(PageSize).Select(UserMovieViewModel.FromUserMovie));
        }

        [Authorize]
        [Route("")]
        public IHttpActionResult PostMyMovie(MovieBindingModel movie)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            var username = this.User.Identity.Name;
            var user = usersService.GetByName(username);

            this.myMovieService.Add(user, movie.MovieId, movie.State);

            //api/movies/{id}
            return this.Created("api/movies/" + movie.MovieId, movie);
        }

        [Authorize]
        [Route("")]
        [HttpPut]
        public IHttpActionResult ChangeStatus(ChangeUserMovieStatusBindingModel movie)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            var username = this.User.Identity.Name;
            var user = usersService.GetByName(username);

            this.myMovieService.UpdateMovieStatus(user, movie.MovieId, movie.State);

            return this.Ok(movie);
        }

        

        [Authorize]
        [Route("{id}")]
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var username = this.User.Identity.Name;
            var user = usersService.GetByName(username);
            this.myMovieService.RemoveMovie(user, id);
        }
    }
}