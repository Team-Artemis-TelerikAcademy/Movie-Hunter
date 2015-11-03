namespace MovieHunter.Data
{
    using System;
    using System.Data.Entity;
    using Models;

    public interface IMovieDbContext : IDisposable
    {
        IDbSet<Movie> Movies { get; set; }

        IDbSet<Genre> Genres { get; set; }

        IDbSet<User> Users { get; set; }

        IDbSet<Message> Messages { get; set; }

        int SaveChanges();
    }
}
