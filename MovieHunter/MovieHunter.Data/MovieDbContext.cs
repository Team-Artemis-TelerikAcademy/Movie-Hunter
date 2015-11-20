namespace MovieHunter.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class MovieDbContext : IdentityDbContext<User>, IMovieDbContext
    {

        public const string DefaultConnection = "MovieHunterConnection";
        public const string TestConnection = "MovieHunterTestConnection";

        public MovieDbContext()
            : base(DefaultConnection)
        {
        }

        public MovieDbContext(string connection)
            : base(connection)
        {
        }

        public static MovieDbContext Create()
        {
            return new MovieDbContext();
        }

        public IDbSet<Genre> Genres { get; set; }

        public IDbSet<Message> Messages { get; set; }

        public IDbSet<Movie> Movies { get; set; }

        public override IDbSet<User> Users { get; set; }

        public IDbSet<Trailer> Trailers { get; set; }

        public IDbSet<Actor> Actors { get; set; }

        public new DbContextConfiguration Configuration
        {
            get
            {
                return base.Configuration;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}
