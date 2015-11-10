﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MovieHunter.Models;

namespace MovieHunter.Api.Models
{
    public class MovieDetailViewModel : MovieViewModel
    {
        public static Expression<Func<Movie, MovieDetailViewModel>> FromMovie
        {
            get
            {
                return movie => new MovieDetailViewModel()
                {
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    Rating = movie.Rating,
                    Image = movie.ImageUrl,
                    Actors = movie.Actors.Select(a => a.FullName),
                    Genres = movie.Genres.Select((g => g.Name)).ToList(),
                    Trailers = movie.Trailers.Select(tr => tr.Url),
                    Description = movie.Description,
                    Duration = movie.Duration,
                    Category = movie.Restriction
                };
            }
        }

        public Restrictions Category { get; set; }

        public int Duration { get; set; }

        public string Description { get; set; }

        public IEnumerable<string> Trailers { get; set; }
    }
}