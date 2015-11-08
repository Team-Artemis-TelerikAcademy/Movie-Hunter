using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using MovieHunter.Models;

namespace MovieHunter.Api.Models
{
    public class MovieViewModel
    {
        public static Expression<Func<Movie, MovieViewModel>> FromMovie
        {
            get
            {
                return movie => new MovieViewModel()
                {
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    Rating  = movie.Rating,
                    Genres = movie.Genres.Select((g=>g.Name))
                };
            }
        }

        public decimal Rating { get; set; }

        public IEnumerable<string> Genres { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Title { get; set; }
    }
}