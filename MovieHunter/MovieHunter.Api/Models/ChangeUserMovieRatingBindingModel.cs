using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHunter.Api.Models
{
    public class ChangeUserMovieRatingBindingModel
    {
        [Required]
        public int MovieId { get; set; }

        [Required]
        [Range(0,10)]
        public int Rating { get; set; }
    }
}
