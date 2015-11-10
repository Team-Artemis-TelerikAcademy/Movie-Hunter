namespace MovieHunter.Importer
{
    public interface IMovieDataImporter
    {
        void ImportMoviesAndTrailers(string jsonMovies, string jsonTrailers);
    }
}