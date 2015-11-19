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
        public int Id { get; set; }

        [Required]
        [Range(0,100)]
        public int Rating { get; set; }
    }
}
