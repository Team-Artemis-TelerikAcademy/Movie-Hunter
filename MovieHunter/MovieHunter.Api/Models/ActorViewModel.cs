using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MovieHunter.Models;

namespace MovieHunter.Api.Models
{
    public class ActorViewModel
    {
        public static Expression<Func<Actor, ActorViewModel>> FromActor
        {
            get
            {
                return actor => new ActorViewModel()
                {
                    Id = actor.Id,
                    FirstName = actor.FirstName,
                    LastName = actor.LastName,
                    FullName = actor.FullName,
                    Movies = actor.Movies.AsQueryable().Select(MovieViewModel.FromMovie).ToList()
                };
            }
        }

        public List<MovieViewModel> Movies { get; set; }

        public string FullName { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public int Id { get; set; }
    }
}