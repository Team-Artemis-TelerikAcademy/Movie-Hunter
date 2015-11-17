using MovieHunter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MovieHunter.Api.Models
{
    public class UserMovieViewModel : MovieViewModel
    {
        public static Expression<Func<UserMovies, UserMovieViewModel>> FromUserMovie
        {
            get
            {
                return userMovie => new UserMovieViewModel()
                {
                    Title = userMovie.Movie.Title,
                    Id = userMovie.Movie.Id,
                    Actors = userMovie.Movie.Actors.Select(a => a.FullName).ToList(),
                    Image = userMovie.Movie.ImageUrl,
                    Rating = userMovie.Movie.Rating,
                    Genres = userMovie.Movie.Genres.Select(g => g.Name).ToList(),
                    ReleaseDate = userMovie.Movie.ReleaseDate,
                    DateAdded = userMovie.DateAdded,
                    Trailers = userMovie.Movie.Trailers.Select(t => t.Url).ToList()
                };
            }
        }

        public DateTime DateAdded { get; private set; }
        public List<string> Trailers { get; private set; }
    }
}