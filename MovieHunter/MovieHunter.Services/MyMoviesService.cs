using MovieHunter.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieHunter.Models;

namespace MovieHunter.Services
{
    public class MyMoviesService : IMyMoviesService
    {
        private IRepository<UserMovies> userMoviesRepository;

        public MyMoviesService(EfRepository<UserMovies> userMoviesRepo)
        {
            this.userMoviesRepository = userMoviesRepo;

        }


        public void Add(User user, int movieId, State state)
        {
            var newUserMovie = new UserMovies()
            {
                MovieId = movieId,
                State = state,
                UserId = user.Id,
                DateAdded = DateTime.Now
            };

            this.userMoviesRepository.Add(newUserMovie);
            this.userMoviesRepository.SaveChanges();
        }

        public IQueryable<UserMovies> GetAllMoviesByUser(User user)
        {
            return user.UserMovies.AsQueryable()
                .OrderByDescending(userMovie => userMovie.DateAdded);
        }

        public IQueryable<UserMovies> GetAllWantToWatchMoviesByUser(User user)
        {
            return this.GetAllMoviesByUser(user)
                    .Where(userMovie => userMovie.State == State.WantToWatch);
        }

        public IQueryable<UserMovies> GetAllWatchedMoviesByUser(User user)
        {
            return this.GetAllMoviesByUser(user)
                    .Where(userMovie => userMovie.State == State.Watched);
        }
    }
}
