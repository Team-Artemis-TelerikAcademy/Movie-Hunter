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
        void UpdateMovieStatus(User user, int movieId, State state);
        void UpdateMovieRating(User user, int movieId, int rating);
        void RemoveMovie(User user, int movieId);
        Movie GetMovieById(int id, User user);
    }
}
