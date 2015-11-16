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


        public void Add(User user, int movieId, State state, Movie movieToAdd)
        {
            var newUserMovie = new UserMovies()
            {
                MovieId = movieId,
                State = state,
                UserId = user.Id,
                Movie = movieToAdd
               
            };

            this.userMoviesRepository.Add(newUserMovie);
            this.userMoviesRepository.SaveChanges();
        }

        public IQueryable<UserMovies> GetAllWachedMoviesByUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
