using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHunter.Models
{
    public class UserMovies
    {
        [Key, Column(Order = 1)]
        public int MovieId { get; set; }

        [Key, Column(Order = 2)]
        public string UserId { get; set; }


        public virtual Movie Movie { get; set; }
        public int? Rating { get; set; }

        public State State { get; set; }
    }
}
