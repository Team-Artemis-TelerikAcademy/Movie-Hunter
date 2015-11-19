using MovieHunter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

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
                    Image = trailer.Movie.ImageUrl,
                    ReleaseDate = trailer.ReleaseDate
                };
            }
        }

        public DateTime ReleaseDate { get; set; }

        public string Movie { get; set; }

        public string Image { get; set; }

        public string Url { get; set; }
    }
}