namespace MovieHunter.Messenger.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    public interface IDataRequester
    {
        string Request(string url, Action<HttpWebRequest> applyModifications = null, Func<HttpWebRequest, Exception, string> errorHandler = null);
        string RequestWithJsonBody(string url, string json, params KeyValuePair<string, string>[] headers);
    }
}