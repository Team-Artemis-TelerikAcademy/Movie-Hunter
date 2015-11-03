namespace MovieHunter.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        private ICollection<Movie> movies;

        public User()
        {
            this.Movies = new HashSet<Movie>();
        }

        // TODO: Web API ClaimsIdentity for authentication

        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(50)]
        [MinLength(4)]
        public string Username { get; set; }

        public bool IsAdmin { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
