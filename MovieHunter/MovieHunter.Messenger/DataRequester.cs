namespace MovieHunter.Messenger
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using FLExtensions.Common;
    using Contracts;
    using Newtonsoft.Json;

    public class DataRequester : IDataRequester
    {
        public static readonly Func<HttpWebRequest, Exception, string> DefaultErrorHandling = (req, ex) => JsonConvert.SerializeObject(ex);
        public static readonly Action<HttpWebRequest> DefaultModification = request => request.Credentials = CredentialCache.DefaultCredentials;

        public string Request(string url, Action<HttpWebRequest> applyModifications, Func<HttpWebRequest, Exception, string> errorHandler = null)
        {
            var request = WebRequest.CreateHttp(url);

            (applyModifications ?? DefaultModification)(request);

            var response = (HttpWebResponse)request.GetResponse();

            try
            {
                using (var receiveStream = response.GetResponseStream())
                {
                    using (var rs = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        return rs.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                return (errorHandler ?? DefaultErrorHandling)(request, ex);
            }
        }

        public string Request(string url, params KeyValuePair<string, string>[] headers)
        {
            return this.Request(url, request =>
            {
                if (headers != null && headers.Length > 0)
                {
                    headers.ForEach(h => request.Headers.Add(h.Key, h.Value));
                }

                request.Credentials = CredentialCache.DefaultCredentials;
            });
        }

        public string Request(string url, string postData)
        {
            return this.Request(url, request =>
            {
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                using (var reqStream = request.GetRequestStream())
                {
                    var postArray = Encoding.ASCII.GetBytes(postData);

                    reqStream.Write(postArray, 0, postArray.Length);
                }
            });
        }

        public string RequestWithJsonBody(string url, string json, params KeyValuePair<string, string>[] headers)
        {
            return this.Request(url, request => 
            {
                request.ContentType = "application/json";
                request.Method = "POST";

                headers.ForEach(h => request.Headers.Add(h.Key, h.Value));

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }
            });
        }
    }
}