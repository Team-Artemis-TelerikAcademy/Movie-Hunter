namespace MovieHunter.Importer.DeserializationModels
{
    public class TrailerModel
    {
        public string duration { get; set; }
        public string title { get; set; }
        public string videoURL { get; set; }
    }

    public class TrailerCollection
    {
        public TrailerModel[] trailers { get; set; }
    }
}