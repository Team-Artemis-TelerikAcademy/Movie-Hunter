using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MovieHunter.Models;

namespace MovieHunter.Api.Models
{
    public class GenreViewModel
    {
        public static Expression<Func<Genre, GenreViewModel>> FromGenre
        {
            get
            {
                return genre => new GenreViewModel()
                {
                    Id = genre.Id,
                    Name = genre.Name,
                    Movies = genre.Movies.AsQueryable().Select(MovieViewModel.FromMovie).ToList()
                };
            }
        }

        public List<MovieViewModel> Movies { get; set; }

        public string Name { get; set; }

        public int Id { get; set; }
    }
}