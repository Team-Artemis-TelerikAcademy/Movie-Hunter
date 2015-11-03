namespace MovieHunter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Movie
    {
        private ICollection<Genre> genres;

        public Movie()
        {
            this.Genres = new HashSet<Genre>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public decimal Rating { get; set; }

        [Required]
        public int Duration { get; set; }

        public string ImageUrl { get; set; }

        public string TrailerUrl { get; set; }

        [Required]
        [MaxLength(1000)]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public virtual ICollection<Genre> Genres
        {
            get
            {
                return this.genres;
            }

            set
            {
                this.genres = value;
            }
        }
    }
}
