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
using System.Web.Http.Cors;
using MovieHunter.Common.Dropbox;

namespace MovieHunter.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MoviesController : ApiController
    {
        private const int PageSize = 10;
        private IMoviesService service;
        private IUsersService usersService;

        //public MoviesController()
        //{
        //    var dbContext = new MovieDbContext();
        //    this.service = new MoviesService(new EfRepository<Movie>(dbContext));
        //    this.usersService = new UsersService(new EfRepository<User>(dbContext));
        //}

        public MoviesController(IMoviesService moviesService, IUsersService usersService)
        {
            this.service = moviesService;
            this.usersService = usersService;
        }


        public IHttpActionResult GetAll()
        {
            return this.GetAll(1);
        }

        public IHttpActionResult GetAll(int page)
        {
            return this.Ok(this.service.GetAllMovies()
                                        .Skip((page - 1)*PageSize)
                                        .Take(PageSize)
                                        .Select(MovieDetailViewModel.FromMovie));
        }

        public IHttpActionResult GetById(int id)
        {
            var movieById = this.service.GetById(id);
            var result = this.Ok(MovieDetailViewModel.FromMovie.Compile().Invoke(movieById));
            return result;
        }

        public IHttpActionResult GetByGenre(string genre)
        {
            return this.GetByGenre(genre, 1);
        }

        public IHttpActionResult GetByGenre(string genre, int page)
        {
            return this.Ok(this.service.GetMoviesByGenre(genre)
                                        .Skip((page-1)*PageSize)
                                        .Take(PageSize)
                                        .Select(MovieDetailViewModel.FromMovie));
        }

        [Authorize]
        [Route("api/movies/{id}/rating")]
        [HttpPut]
        public IHttpActionResult ChangeRating(ChangeUserMovieRatingBindingModel movie)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            var username = this.User.Identity.Name;
            var user = usersService.GetByName(username);

            this.service.UpdateMovieRating(user, movie.Id, movie.Rating);

            return this.Ok(movie);
        }



        //api/movies/released
        [Route("api/movies/released")]
        public IHttpActionResult GetAllReleasedMovies()
        {
            return this.GetAllReleasedMovies(1);
        }

        [Route("api/movies/released")]
        public IHttpActionResult GetAllReleasedMovies(int page)
        {
            return this.Ok(this.service.GetAllMovies()
                                        .Where(movie=>movie.ReleaseDate<=DateTime.Now)
                                        .Skip((page-1)*PageSize)
                                        .Take(PageSize)
                                        .Select(MovieDetailViewModel.FromMovie));
        }

        //api/movies/comming-soon
        [Route("api/movies/comming-soon")]
        public IHttpActionResult GetAllCommingSoonMovies()
        {
            return this.GetAllCommingSoonMovies(1);
        }

        [Route("api/movies/comming-soon")]
        public IHttpActionResult GetAllCommingSoonMovies(int page)
        {
            return this.Ok(this.service.GetAllMovies()
                                        .Where(movie => movie.ReleaseDate > DateTime.Now)
                                        .Skip((page - 1) * PageSize)
                                        .Take(PageSize)
                                        .Select(MovieDetailViewModel.FromMovie));
        }

        [Route("api/movies/{id}/download-wallpaper")]
        public IHttpActionResult GetImageUrlForDropbox(int id)
        {
            var movie = this.service.GetById(id);
            var dropboxUrl = new DropboxService().GetRedirectionUrl(movie.ImageUrl, movie.Title + "-wallpaper");

            return this.Redirect(dropboxUrl);
        }
    }
}
