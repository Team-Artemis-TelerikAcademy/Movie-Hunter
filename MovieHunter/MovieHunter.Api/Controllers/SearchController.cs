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
    public class SearchController : ApiController
    {
        private IActorsService actorsService;
        private IGenresService genresService;
        private IMoviesService moviesService;

        public SearchController()
        {
            var dbContext = new MovieDbContext();
            this.moviesService = new MoviesService(new EfRepository<Movie>(dbContext));
            this.actorsService = new ActorsService(new EfRepository<Actor>(dbContext));
            this.genresService = new GenresService(new EfRepository<Genre>(dbContext));
        }

        [Route("api/search")]
        public IHttpActionResult GetResult(string pattern)
        {
            pattern = pattern.ToLower();
            var movies = this.moviesService.GetAllMovies().Where(m => m.Title.ToLower().Contains(pattern)).Select(m => m.Title).ToList();
            var actors = this.actorsService.GetAllActors().Where(a => a.FullName.ToLower().Contains(pattern)).Select(a => a.FullName).ToList();
            var genres = this.genresService.GetAllGenres().Where(g => g.Name.ToLower().Contains(pattern)).Select(g=>g.Name).ToList();
            var result = new //SearchResultResponseModel
            {
                Movies = movies,
                Actors = actors,
                Genres = genres

            };

            return this.Ok(result);
        }
    }
}