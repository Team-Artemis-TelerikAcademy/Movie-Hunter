using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MovieHunter.Api.Models;
using MovieHunter.Data;
using MovieHunter.Models;
using MovieHunter.Services;
using MovieHunter.Services.Contracts;

namespace MovieHunter.Api.Controllers
{
    public class MoviesController : ApiController
    {
        private IMoviesService service;
        private const int PageSize = 10;

        public MoviesController()
        {
            var dbContext = new MovieDbContext();
            this.service = new MoviesService(new EfRepository<Movie>(dbContext));
        }

        public IHttpActionResult GetAll()
        {
            return this.GetAll(1);
        }

        public IHttpActionResult GetAll(int page)
        {   
            return this.Ok(this.service.GetAllMovies()
                .Skip((page-1)*PageSize)
                .Take(PageSize)
                .Select(MovieViewModel.FromMovie));
        }
   

        public IHttpActionResult GetById(int id)
        {
            return this.Ok(MovieViewModel.FromMovie.Compile().Invoke(this.service.GetById(id)));
        }

        //api/movies?genre=value
        public IHttpActionResult GetByGenre(string genre)
        {
            return this.GetByGenre(genre, 1);
        }

        public IHttpActionResult GetByGenre(string genre, int page)
        {
            return this.Ok(this.service.GetMoviesByGenre(genre)
                .Skip((page - 1)*PageSize)
                .Take(PageSize)
                .Select(MovieViewModel.FromMovie));
        }

        //api/movies/released
        [Route("api/movies/released")]
        public IHttpActionResult GetAllReleasedMovies()
        {
            return this.GetAllReleasedMovies(1);
        }

        public IHttpActionResult GetAllReleasedMovies(int page)
        {
            return this.Ok(this.service.GetAllMovies()
                                        .Where(movie => movie.ReleaseDate <= DateTime.Today)
                                        .Skip((page-1)*PageSize)
                                        .Take(PageSize).Select(MovieViewModel.FromMovie));
        }
        //api/movies/comming-soon
        [Route("api/movies/comming-soon")]
        public IHttpActionResult GetAllCommingSoonMovies()
        {
            return this.GetAllCommingSoonMovies(1);
        }

        public IHttpActionResult GetAllCommingSoonMovies(int page)
        {
            return this.Ok(this.service.GetAllMovies()
                                        .Where(movie => movie.ReleaseDate > DateTime.Now)
                                        .Skip((page-1)*PageSize)
                                        .Take(PageSize).Select(MovieViewModel.FromMovie));
        }
    }
}
