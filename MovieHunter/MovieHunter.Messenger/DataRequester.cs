namespace MovieHunter.Messenger
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using FLExtensions.Common;

    public class DataRequester
    {
        public string Request(string url, ICollection<KeyValuePair<string, string>> headers = null)
        {
            var request = WebRequest.CreateHttp(url);

            if (headers != null && headers.Count > 0)
            {
                headers.ForEach(h => request.Headers.Add(h.Key, h.Value));
            }      

            request.Credentials = CredentialCache.DefaultCredentials;

            var response = (HttpWebResponse)request.GetResponse();

            var receiveStream = response.GetResponseStream();

            var readStream = new StreamReader(receiveStream, Encoding.UTF8);
            var json = readStream.ReadToEnd();

            receiveStream.Close();
            readStream.Close();

            return json;
        }

        public string Request(string url, string postData)
        {
            var webRequest = WebRequest.Create(url);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";

            var reqStream = webRequest.GetRequestStream();

            var postArray = Encoding.ASCII.GetBytes(postData);

            reqStream.Write(postArray, 0, postArray.Length);
            reqStream.Close();
            string result;
            try
            {
                var sr = new StreamReader(webRequest.GetResponse().GetResponseStream());
                result = sr.ReadToEnd();
                sr.Close();
            }
            catch (WebException wex)
            {
                result = "{ \"access_token\": \"\" }";
            }

            return result;
        }

        public string RequestWithJsonBody(string url, string json, ICollection<KeyValuePair<string, string>> headers = null)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            headers.ForEach(h => httpWebRequest.Headers.Add(h.Key, h.Value));

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }
    }
}