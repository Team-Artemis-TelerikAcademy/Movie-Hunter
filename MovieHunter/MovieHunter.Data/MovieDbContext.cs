namespace MovieHunter.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Models;

    public class MovieDbContext : DbContext, IMovieDbContext
    {
        public const string DefaultConnection = "MovieHunterConnection";

        public MovieDbContext()
            : base(DefaultConnection)
        {
        }

        public IDbSet<Genre> Genres { get; set; }

        public IDbSet<Message> Messages { get; set; }

        public IDbSet<Movie> Movies { get; set; }

        public IDbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}
