using System.Collections.Generic;
using System.Linq;
using MovieHunter.Models;
using MovieHunter.Services.Contracts;

namespace MovieHunter.Services
{
    public class GenresService :IGenresService
    {
        private EfRepository<Genre> genres;

        public GenresService(EfRepository<Genre> genresRepo)
        {
            this.genres = genresRepo;
        }

        public IQueryable<Genre> GetAllGenres()
        {
            return genres.All();
        }

        public Genre GetGenreById(int id)
        {
            return this.genres.Find(id);
        }
    }
}