namespace MovieHunter.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Actor
    {
        private ICollection<Movie> movies;

        public Actor()
        {
            this.Movies = new HashSet<Movie>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }


        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }
        
        [Column(TypeName = "ntext")]
        public string PersonalInfo { get; set; }

        public virtual ICollection<Movie> Movies
        {
            get
            {
                return this.movies;
            }
            set
            {
                this.movies = value;
            }
        }
    }
}
