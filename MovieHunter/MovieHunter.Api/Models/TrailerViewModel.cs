using System;
using System.Linq.Expressions;
using MovieHunter.Models;

namespace MovieHunter.Api.Models
{
    public class TrailerViewModel
    {
        public static Expression<Func<Trailer, TrailerViewModel>> FromTrailer
        {
            get
            {
                return trailer => new TrailerViewModel()
                {
                    Url = trailer.Url,
                    Movie = trailer.Movie.Title,
                    ReleaseDate = trailer.ReleaseDate
                };
            }
        }

        public DateTime ReleaseDate { get; set; }

        public string Movie { get; set; }

        public string Url { get; set; }
    }
}