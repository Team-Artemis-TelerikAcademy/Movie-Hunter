using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieHunter.Models;

namespace MovieHunter.Api.Models
{
    public class SearchResultResponseModel
    {
        public IEnumerable<Actor> Actors { get; internal set; }
        public IEnumerable<Genre> Genres { get; internal set; }
        public IEnumerable<Movie> Movies { get; internal set; }
    }
}
