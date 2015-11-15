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

namespace MovieHunter.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MoviesController : ApiController
    {
        private const int PageSize = 10;
        private IMoviesService service;

        //public MoviesController()
        //{
        //    var dbContext = new MovieDbContext();
        //    this.service = new MoviesService(new EfRepository<Movie>(dbContext));
        //}

        public MoviesController(IMoviesService moviesService)
        {
            this.service = moviesService;
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
            return this.Ok(MovieDetailViewModel.FromMovie.Compile()
                                                         .Invoke(this.service
                                                                       .GetById(id)));
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
    }
}
