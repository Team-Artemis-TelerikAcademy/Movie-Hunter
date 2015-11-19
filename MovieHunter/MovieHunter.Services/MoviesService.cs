using System;
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


        public void UpdateMovieRating(User user, int movieId, int rating)
        {
            var movie = movies.Find(movieId);
            var count = movie.CountRating + 1;
            var currentRating = movie.Rating;
            movie.Rating = (rating + currentRating) / count;
            this.movies.Update(movie);
            this.movies.SaveChanges();
        }
    }
}