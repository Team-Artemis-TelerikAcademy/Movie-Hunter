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
        IQueryable<UserMovies> GetAllWachedMoviesByUser(User user);
        void Add(User user, int movieId, State state, Movie movieToAdd);
    }
}
