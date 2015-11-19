namespace MovieHunter.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Models;

    public interface IMovieDbContext : IDisposable
    {
        IDbSet<Movie> Movies { get; set; }

        IDbSet<Actor> Actors { get; set; }

        IDbSet<Genre> Genres { get; set; }

        IDbSet<User> Users { get; set; }

        IDbSet<Message> Messages { get; set; }

        IDbSet<Trailer> Trailers { get; set; }

        DbContextConfiguration Configuration { get; }

        int SaveChanges();
    }
}