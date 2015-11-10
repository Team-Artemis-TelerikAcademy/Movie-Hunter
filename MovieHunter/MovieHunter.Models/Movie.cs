namespace MovieHunter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Movie
    {
        private ICollection<Genre> genres;
        private ICollection<Actor> actors;
        private ICollection<UserMovies> userMovies;
        private ICollection<Trailer> trailers;

        public Movie()
        {
            this.Genres = new HashSet<Genre>();
            this.Actors = new HashSet<Actor>();
            this.UserMovies = new HashSet<UserMovies>();
            this.Trailers = new HashSet<Trailer>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public decimal Rating { get; set; }

        [Required]
        public int Duration { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<Trailer> Trailers
        {
            get { return this.trailers; }
            set { this.trailers = value; }
        }

        public Restrictions Restriction { get; set; }

        [Required]
        [MaxLength(2000)]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
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

        public virtual ICollection<Actor> Actors
        {
            get
            {
                return this.actors;
            }

            set
            {
                this.actors = value;
            }
        }

        public virtual ICollection<UserMovies> UserMovies
        {
            get { return this.userMovies; }
            set { this.userMovies = value; }
        }
    }

    
}
