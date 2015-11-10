namespace MovieHunter.Importer.ImdbDownloader
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using DeserializationModels;
    using Newtonsoft.Json;

    public class Downloader
    {
        private static ICollection<MovieModel> Movies = new List<MovieModel>();
        private static ICollection<TrailerModel> Trailers = new List<TrailerModel>();

        /// <summary>
        /// Releases any downloaded content.
        /// </summary>
        public static void Flush()
        {
            Movies.Clear();
            Trailers.Clear();
        }

        /// <summary>
        /// Downloads trailer info and info about their respective movies from the specified pages into the RAM memory. Uses the classes from DeserializationModels folder.
        /// </summary>
        /// <param name="startPage"></param>
        /// <param name="endPage"></param>
        public static void Download(int startPage = 1, int endPage = 5)
        {
            Console.WriteLine("Download started for pages " + startPage + " to " + endPage);
            for (int i = startPage; i <= endPage; i++)
            {
                Console.SetCursorPosition(0, 1);
                Console.WriteLine("Downloading page " + i);
                var trailers = GetTrailersArray(i);

                foreach (var t in trailers)
                {
                    Trailers.Add(t);
                }

                DownloadMoviesForTrailers(trailers, i);

                Console.Clear();
            }
            Console.WriteLine("\nFinished downloading");
        }

        /// <summary>
        /// Saves the downloaded info in the specified folder. The folder is created if it doesn't exist.
        /// </summary>
        /// <param name="folderPath"></param>
        public static void SaveToFolder(string folderPath = "../../SampleData")
        {
            Console.WriteLine("Saving...");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            File.AppendAllText(folderPath + "/movies.json", JsonConvert.SerializeObject(Movies));
            File.AppendAllText(folderPath + "/trailers.json", JsonConvert.SerializeObject(Trailers));

            Console.WriteLine("Data saved successfully");
        }

        private static void DownloadMoviesForTrailers(TrailerModel[] trailers, int part)
        {
            var counter = 0;

            foreach (var item in trailers)
            {
                DownloadMovieByTitle(item.title, part);
                if (counter % 5 == 0)
                {
                    Console.Write('.');
                }
            }
        }

        private static void DownloadMovieByTitle(string title, int part)
        {
            var request = (HttpWebRequest)WebRequest.Create(
                string.Format("http://www.myapifilms.com/imdb?title={0}&format=JSON&aka=0&business=0&seasons=0&seasonYear=0&technical=0&filter=M&exactFilter=0&limit=1&forceYear=0&lang=en-us&actors=F&biography=0&trailer=0&uniqueName=0&filmography=0&bornDied=0&starSign=0&actorActress=0&actorTrivia=0&movieTrivia=0&awards=1&moviePhotos=N&movieVideos=N&similarMovies=0&adultSearch=0", title));

            request.MaximumAutomaticRedirections = 4;
            request.MaximumResponseHeadersLength = 4;

            request.Credentials = CredentialCache.DefaultCredentials;
            var response = (HttpWebResponse)request.GetResponse();



            var receiveStream = response.GetResponseStream();

            var readStream = new StreamReader(receiveStream, Encoding.UTF8);
            var json = readStream.ReadToEnd();
            MovieModel[] result;
            try
            {
                result = JsonConvert.DeserializeObject<MovieModel[]>(json);

                foreach (var item in result)
                {
                    Movies.Add(item);
                }
            }
            catch (Exception)
            {
                return;
            }

            response.Close();
            readStream.Close();
        }

        private static TrailerModel[] GetTrailersArray(int page)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://www.myapifilms.com/imdb/trailers?format=JSON&trailers=PM&page=" + page);

            request.MaximumAutomaticRedirections = 4;
            request.MaximumResponseHeadersLength = 4;
            request.Credentials = CredentialCache.DefaultCredentials;
            var response = (HttpWebResponse)request.GetResponse();

            var receiveStream = response.GetResponseStream();

            var readStream = new StreamReader(receiveStream, Encoding.UTF8);

            var json = readStream.ReadToEnd(); ;

            var result = JsonConvert.DeserializeObject<TrailerRoot>(json).trailers;

            foreach (var item in result)
            {
                Trailers.Add(item);
            }

            response.Close();
            readStream.Close();

            return result;
        }
    }
}
