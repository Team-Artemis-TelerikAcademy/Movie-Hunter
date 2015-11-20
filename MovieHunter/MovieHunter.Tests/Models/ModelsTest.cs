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
            var title = "Test title TestMoviesAdd";
            db.Movies.Add(new Movie()
            {
                Duration = 90,
                Rating = 1.1M,
                Title = title,
                ReleaseDate = releaseDate,
                Description = "Test Description"
            });

            db.SaveChanges();

            var addedMovies = db.Movies.AsQueryable().Single(m => m.Title == title);

            Assert.AreEqual(90, addedMovies.Duration);
            Assert.AreEqual(1.1M, addedMovies.Rating);
            Assert.AreEqual(title, addedMovies.Title);
            Assert.AreEqual(releaseDate, addedMovies.ReleaseDate);
            Assert.AreEqual("Test Description", addedMovies.Description);
        }

        [TestMethod]
        public void TestActorsAdd()
        {
            string fullName = "Pesho Goshov";
            db.Actors.Add(new Actor()
            {
                FirstName = "Pesho",
                LastName = "Goshov",
                FullName = fullName
            });

            db.SaveChanges();

            var addedActor = db.Actors.AsQueryable().Single(a => a.FullName == fullName);

            Assert.AreEqual("Pesho", addedActor.FirstName);
            Assert.AreEqual("Goshov", addedActor.LastName);
            Assert.AreEqual(fullName, addedActor.FullName);
        }

        [TestMethod]
        public void TestGenresAdd()
        {
            string name = "Test name";
            db.Genres.Add(new Genre()
            {
                Name = name
            });

            db.SaveChanges();

            var addedGenre = db.Genres.AsQueryable().Single(g => g.Name == name);

            Assert.AreEqual(name, addedGenre.Name);
        }

        [TestMethod]
        public void TestMessagesAdd()
        {
            var timeSent = DateTime.Now;
            db.Messages.Add(new Message()
            {
                Content = "Test content",
                TimeSent = timeSent
            });

            db.SaveChanges();

            var addedMessage = db.Messages.AsQueryable().Single(m => m.Content == "Test content");

            Assert.AreEqual("Test content", addedMessage.Content);
            Assert.AreEqual(timeSent, addedMessage.TimeSent);

        }

        [TestMethod]
        public void TestTrailersAdd()
        {
            var releaseDate = DateTime.Now;
            string url = "https://www.google.bg";
            db.Movies.Add(new Movie()
            {
                Duration = 90,
                Rating = 1.1M,
                Title = "T title",
                ReleaseDate = releaseDate,
                Description = "Test Description"
            });

            db.Trailers.Add(new Trailer()
            {
                Url = url,
                ReleaseDate = releaseDate,
                MovieId = 1
            });

            db.SaveChanges();

            var addedTrailer = db.Trailers.AsQueryable().Single(t => t.Url == url);

            Assert.AreEqual(url, addedTrailer.Url);
            Assert.AreEqual(releaseDate, addedTrailer.ReleaseDate);
            Assert.AreEqual(1, addedTrailer.MovieId);

        }

        [TestMethod]
        public void TestUsersAdd()
        {
            string userName = "Test name";
            db.Users.Add(new User()
            {
                UserName = userName
            });

            db.SaveChanges();

            var addedUser = db.Users.AsQueryable().Single(u=> u.UserName == userName);

            Assert.AreEqual(userName, addedUser.UserName);
        }

        [ClassCleanup]
        public static void CleanUp()
        {
            db.Database.Delete();
            db.Dispose();
        }
    }
}
