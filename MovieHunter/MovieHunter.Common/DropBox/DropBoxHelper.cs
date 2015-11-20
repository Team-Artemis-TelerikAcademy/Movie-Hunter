namespace MovieHunter.Common.Dropbox
{
    using System;
    using System.IO;
    using System.Diagnostics;

    using Spring.Social.OAuth1;
    using Spring.Social.Dropbox.Api;
    using Spring.Social.Dropbox.Connect;
    using Spring.IO;
    using System.Net;

    public class DropboxService
    {
        private const string DropboxAppKey = "29ocgdy2n22wvqw";
        private const string DropboxAppSecret = "za013l7n2750jwu";

        private const string appToken = "oramtoz0q2qr3kc1";
        private const string appTokenSecret = "va0jkyz16vzjtm4";

        private IDropbox currentDropbox;

        public DropboxService()
        {
            DropboxServiceProvider dropboxServiceProvider =
                new DropboxServiceProvider(DropboxAppKey, DropboxAppSecret, AccessLevel.AppFolder);

            IDropbox dropbox = dropboxServiceProvider.GetApi(appToken, appTokenSecret);

            DropboxProfile profile = dropbox.GetUserProfileAsync().Result;

            this.currentDropbox = dropbox;
        }

        public string GetRedirectionUrl(string url, string wallpaperName)
        {
            byte[] imageBytes;

            using (var wc = new WebClient())
            {
                imageBytes = wc.DownloadData(url);
            }

            IResource res = new ByteArrayResource(imageBytes);

            Entry uploadFileEntry = currentDropbox.UploadFileAsync(res, string.Format("/movie-images/{0}-wallpaper.jpeg", wallpaperName)).Result;

            DropboxLink sharedUrl = currentDropbox.GetShareableLinkAsync(uploadFileEntry.Path).Result;

            var link = sharedUrl.Url;

            return link;
        }
    }
}
