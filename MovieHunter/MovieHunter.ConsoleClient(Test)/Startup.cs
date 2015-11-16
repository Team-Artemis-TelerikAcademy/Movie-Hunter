namespace MovieHunter.ConsoleClientTest
{
    using Data;
    using Models;
    using System;
    using Data.Migrations;
    using System.Data.Entity;
    using System.Linq;

    public static class Startup
    {
        public static void Main()
        {
            //using (var db = new MovieDbContext())
            //{

            //    db.Movies.Add(new Movie()
            //    {
            //        Duration = 90, // standartno, kazva magi
            //        Rating = 1.1M,
            //        Title = "obyrkani kursisti pishat otborna",
            //        ReleaseDate = DateTime.Now,
            //        Description = "tyrkalqt se po barbaronite i vrunkat pesho da im proverqva koda"
            //    });

            //    db.SaveChanges();
            //}

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MovieDbContext, Configuration>());
            using (IMovieDbContext db = new MovieDbContext())
            {
                var e = db.Movies.FirstOrDefault();

                Console.WriteLine(e.Duration);
            }
        }
    }
}
