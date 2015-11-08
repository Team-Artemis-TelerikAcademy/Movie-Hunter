using System.Linq;
using MovieHunter.Models;

namespace MovieHunter.Services.Contracts
{
    public interface IMoviesService
    {
        IQueryable<Movie> GetAllMovies();
        Movie GetById(int id);
        IQueryable<Movie> GetMoviesByGenre(string genre);
    }
}