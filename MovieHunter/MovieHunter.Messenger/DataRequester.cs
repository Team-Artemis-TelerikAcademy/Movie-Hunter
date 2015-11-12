namespace ChatTest
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using FLExtensions.Common;

    public class DataRequester
    {
        //public string Get(string url, ICollection<string> headers = null)
        //{
        //    if(headers == null)
        //    {

        //    }
        //}

        public string Request(string url, ICollection<string> headers = null)
        {
            var request = WebRequest.CreateHttp(url);

            if (headers != null && headers.Count > 0)
            {
                headers.ForEach(h => request.Headers.Add(h));
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
    }
}