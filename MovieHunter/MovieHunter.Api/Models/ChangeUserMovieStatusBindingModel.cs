using MovieHunter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHunter.Api.Models
{
    public class ChangeUserMovieStatusBindingModel
    {
        [Required]
        public int MovieId { get; set; }

        [Required]
        public State State { get; set; }
    }
}
