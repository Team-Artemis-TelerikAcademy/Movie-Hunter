namespace MovieHunter.Tests
{
    using Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using System;
    using System.Linq;

    [TestClass]
    public class ModelsTest
    {
        private static MovieDbContext db;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            db = new MovieDbContext(MovieDbContext.TestConnection);
            db.Database.Delete();
        }

        [TestMethod]
        public void TestMoviesAdd()
        {
            var releaseDate = DateTime.Now;
            db.Movies.Add(new Movie()
            {
                Duration = 90,
                Rating = 1.1M,
                Title = "Test title",
                ReleaseDate = releaseDate,
                Description = "Test Description"
            });
            db.SaveChanges();

            var addedMovies = db.Movies.AsQueryable().Single();

            Assert.AreEqual(1, addedMovies.Id);
            Assert.AreEqual(90, addedMovies.Duration);
            Assert.AreEqual(1.1M, addedMovies.Rating);
            Assert.AreEqual("Test title", addedMovies.Title);
            Assert.AreEqual(releaseDate, addedMovies.ReleaseDate);
            Assert.AreEqual("Test Description", addedMovies.Description);
        }

        [ClassCleanup]
        public static void CleanUp()
        {
            db.Database.Delete();
            db.Dispose();
        }
    }
}
