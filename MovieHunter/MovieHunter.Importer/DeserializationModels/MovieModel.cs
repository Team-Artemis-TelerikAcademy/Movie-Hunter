namespace MovieHunter.Importer.DeserializationModels
{
    public class MovieModel
    {
        public Actor[] actors { get; set; }
        public Award[] awards { get; set; }
        public string[] filmingLocations { get; set; }
        public string[] genres { get; set; }
        public string metascore { get; set; }
        public string plot { get; set; }
        public string rated { get; set; }
        public string rating { get; set; }
        public string releaseDate { get; set; }
        public string[] runtime { get; set; }
        //public string simplePlot { get; set; }
        public string title { get; set; }
        public string urlIMDB { get; set; }
        public string urlPoster { get; set; }
        public string votes { get; set; }
        //public string year { get; set; }
    }

    public class Actor
    {
        public string actorName { get; set; }
        public string urlPhoto { get; set; }
        public string urlProfile { get; set; }
    }

    public class Award
    {
        public string award { get; set; }
    }

}