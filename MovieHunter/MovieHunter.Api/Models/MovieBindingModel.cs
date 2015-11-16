using MovieHunter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHunter.Api.Models
{
    public class MovieBindingModel
    {
        [Required]
        public int MovieId { get; set; }

        [Required]
        public State State { get; set; }

        public Movie Movie { get; set; }
    }
}
