using System.Linq;
using MovieHunter.Models;
using MovieHunter.Services.Contracts;

namespace MovieHunter.Services
{
    public class MoviesService : IMoviesService
    {
        private IRepository<Movie> movies;

        public MoviesService(IRepository<Movie> moviesRepo)
        {
            this.movies = moviesRepo;
        }

        public IQueryable<Movie> GetAllMovies()
        {
            return this.movies.All()
                .OrderByDescending(m => m.Rating);
        }

        public Movie GetById(int id)
        {
            return this.movies.Find(id);
        }

        public IQueryable<Movie> GetMoviesByGenre(string genre)
        {
            return this.GetAllMovies()
                .Where(movie => movie.Genres.Any(g => g.Name.ToLower() == genre.ToLower()));
        }
    }
}