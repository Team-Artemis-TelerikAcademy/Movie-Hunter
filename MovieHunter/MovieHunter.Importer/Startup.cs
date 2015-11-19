namespace MovieHunter.Importer
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Globalization;
    using System.Data.Entity.Validation;
    using System.Data.Entity;

    using Data;
    using ImdbDownloader;
    using Models;

    public static class Startup
    {
        public static void Main()
        {
            // decimal separator problem
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            //Database.SetInitializer(new DropCreateDatabaseAlways<MovieDbContext>());

            // uncomment to download sample data from imdb
            //Downloader.Download(1, 5);
            //Downloader.SaveToFolder("../../SampleData");

            using (var db = new MovieDbContext())
            {
                try
                {
                    var moviesJson = File.ReadAllText("../../SampleData/movies.json");
                    var trailersJson = File.ReadAllText("../../SampleData/trailers.json");
                    new Importer(db).ImportMoviesAndTrailers(moviesJson, trailersJson);
                }
                catch (DbEntityValidationException ex)
                {
                    Console.WriteLine("Validation error:");
                    foreach (var item in ex.EntityValidationErrors)
                    {
                        foreach (var e in item.ValidationErrors)
                        {
                            Console.WriteLine(e.ErrorMessage + " " + e.PropertyName);
                        }
                    }
                }
            }
        }
    }
}
