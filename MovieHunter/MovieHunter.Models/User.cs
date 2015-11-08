namespace MovieHunter.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        private ICollection<UserMovies> userMovies;

        public User()
        {
            this.UserMovies = new HashSet<UserMovies>();
        }

        // TODO: Web API ClaimsIdentity for authentication

        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(50)]
        [MinLength(4)]
        public string Username { get; set; }

        public bool IsAdmin { get; set; }

        public virtual ICollection<UserMovies> UserMovies { get; set; }
    }
}
