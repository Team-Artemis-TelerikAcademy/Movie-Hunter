using MovieHunter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHunter.Services.Contracts
{
    public interface IMyMoviesService
    {
        IQueryable<UserMovies> GetAllWantToWatchMoviesByUser(User user);
        void Add(User user, int movieId, State state);
        IQueryable<UserMovies> GetAllWatchedMoviesByUser(User user);
        IQueryable<UserMovies> GetAllMoviesByUser(User user);
    }
}
