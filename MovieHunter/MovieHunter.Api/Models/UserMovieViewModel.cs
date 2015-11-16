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
                return movie => new UserMovieViewModel()
                {
                    Title = movie.Movie.Title
                };
            }
        }
    }
}