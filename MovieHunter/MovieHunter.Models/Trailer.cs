using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHunter.Models
{
    public class Trailer
    {
        public int Id { get; set; }

        [Required]
        [MinLength(7)]
        public string Url { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
        
    }
}
