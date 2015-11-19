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
            //validation if user already has this movie
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

        public Movie GetMovieById(int id, User user)
        {
            var userMovie = user.UserMovies.FirstOrDefault(um => um.MovieId == id);
            return userMovie.Movie;
        }

        public void RemoveMovie(User user, int movieId)
        {
            var userMovie = user.UserMovies.FirstOrDefault(um => um.MovieId == movieId);
            this.userMoviesRepository.Delete(userMovie);
            this.userMoviesRepository.SaveChanges();
        }

        

        public void UpdateMovieStatus(User user, int movieId, State state)
        {
            var userMovie = user.UserMovies.FirstOrDefault(um => um.MovieId == movieId);
            userMovie.State = state;
            this.userMoviesRepository.Update(userMovie);
            this.userMoviesRepository.SaveChanges();
        }

    }
}
