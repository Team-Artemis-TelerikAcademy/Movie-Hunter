using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using MovieHunter.Api.Models;
using MovieHunter.Data;
using MovieHunter.Models;
using MovieHunter.Services;
using MovieHunter.Services.Contracts;

namespace MovieHunter.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GenresController : ApiController
    {
        private IGenresService service;

        public GenresController()
        {
            var dbContext = new MovieDbContext();
            this.service = new GenresService(new EfRepository<Genre>(dbContext));
        }

        public IHttpActionResult GetAll()
        {
            return this.GetAll(1);
        }

        public IHttpActionResult GetAll(int page)
        {
            return this.Ok(this.service.GetAllGenres().Select(g => g.Name));
        }

        public IHttpActionResult GetById(int id)
        {
            var genre = this.service.GetGenreById(id);
            var viewModel = new
            {
                Id = genre.Id,
                Name = genre.Name,
                Movies = genre.Movies.AsQueryable().Select(MovieViewModel.FromMovie)
            };
            return this.Ok(viewModel);
            // return this.Ok(GenreViewModel.FromGenre.Compile().Invoke(this.service.GetGenreById(id)));
        }

        public IHttpActionResult GetByName(string name)
        {
            var genre = this.service.GetGenreByName(name);

            var viewModel = new
            {
                Id = genre.Id,
                Name = genre.Name,
                Movies = genre.Movies.AsQueryable().Select(MovieViewModel.FromMovie)
            };

            return this.Ok(viewModel);
        }
    }
}
