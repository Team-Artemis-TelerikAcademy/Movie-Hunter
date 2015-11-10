namespace MovieHunter.Importer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Data;
    using DeserializationModels;
    using Models;
    using Extensions;
    using Newtonsoft.Json;

    public class Importer : IMovieDataImporter
    {
        private static IDictionary<string, Restrictions> RestrictionMapping = new Dictionary<string, Restrictions>()
        {
            { "pg", Restrictions.Twelve },
            { "pg-13", Restrictions.Twelve },
            { "nc-17", Restrictions.Eighteen },
            {  "r", Restrictions.NeStavaZahoraSasSlabiSarca }
        };

        private const string EmergencyTrailer = "http://www.youtube.com/watch?v=mMFqjVnCpCo";

        private IMovieDbContext db;

        public Importer(IMovieDbContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// Imports movies and trailers records into the code-first database, using two json files as source.
        /// </summary>
        /// <param name="jsonMovies"></param>
        /// <param name="jsonTrailers"></param>
        public void ImportMoviesAndTrailers(string jsonMovies, string jsonTrailers)
        {
            this.db.Configuration.ValidateOnSaveEnabled = false;

            var movies = GetMovies(jsonMovies);
            var trailers = GetTrailers(jsonTrailers);

            var actors = new Dictionary<string, Models.Actor>();
            var genres = new Dictionary<string, Genre>();

            var counta = 0;

            foreach (var currentMovie in movies)
            {
                var first = trailers.FirstOrDefault(x => x.title == currentMovie.title);
                var trailerUrl = first == null ? EmergencyTrailer : first.videoURL;

                var trailer = new Trailer()
                {
                    Url = trailerUrl
                };

                this.db.Trailers.Add(trailer);

                decimal result = 0M;
                decimal.TryParse(currentMovie.rating, out result);

                var movieToAdd = new Movie()
                {
                    Description = currentMovie.plot,
                    Title = currentMovie.title,
                    ImageUrl = currentMovie.urlPoster,
                    Rating = result,
                    ReleaseDate = currentMovie.releaseDate.ToDateTime(),
                    Restriction = RestrictionMapping.ContainsKey(currentMovie.rated.ToLower()) ? RestrictionMapping[currentMovie.rated.ToLower()] : Restrictions.NotRestricted,
                    Duration = currentMovie.runtime == null || currentMovie.runtime.Length == 0 ? 90 : int.Parse(currentMovie.runtime[0].Split(' ')[0])
                };

                this.db.Movies.Add(movieToAdd);

                if (currentMovie.actors != null)
                {
                    foreach (var currentActor in currentMovie.actors)
                    {
                        var currentActorNames = currentActor.actorName.Split(' ');

                        var firstname = currentActorNames[0];
                        var lastname = currentActorNames[currentActorNames.Length == 1 ? 0 : 1];

                        if (!actors.ContainsKey(firstname + " " + lastname))
                        {
                            var actorToAdd = new Models.Actor()
                            {
                                FirstName = firstname,
                                LastName = lastname
                            };

                            actors.Add(actorToAdd.FirstName + " " + actorToAdd.LastName, actorToAdd);
                            this.db.Actors.Add(actorToAdd);
                        }

                        movieToAdd.Actors.Add(actors[firstname + " " + lastname]);
                    }
                }

                foreach (var currentGenre in currentMovie.genres)
                {
                    if (!genres.ContainsKey(currentGenre))
                    {
                        var newGenre = new Genre()
                        {
                            Name = currentGenre
                        };

                        genres.Add(currentGenre, newGenre);
                    }

                    movieToAdd.Genres.Add(genres[currentGenre]);
                    genres[currentGenre].Movies.Add(movieToAdd);
                    this.db.Genres.Add(genres[currentGenre]);
                }

                movieToAdd.Trailers.Add(trailer);
                this.db.Movies.Add(movieToAdd);

                if (counta++ % 5 == 4)
                {
                    this.db.SaveChanges();

                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine(counta + " items added");
                }
            }

            Console.WriteLine("data from from json imported.");
        }

        private static MovieModel[] GetMovies(string json)
        {
            var movies = JsonConvert.DeserializeObject<MovieModel[]>(json);
            return movies;
        }

        private static TrailerModel[] GetTrailers(string json)
        {
            var trailers = JsonConvert.DeserializeObject<TrailerModel[]>(json);
            return trailers;
        }
    }
}
