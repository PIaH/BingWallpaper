using BingWallpaper.Lib.Data;
using BingWallpaper.Lib.Exceptions;
using BingWallpaper.Lib.Logging;
using Newtonsoft.Json;
using System.Net;

namespace BingWallpaper.Lib.Core
{
    public class JsonImageDownloader
    {
        private static Logger Log = Logger.Instance;

        public JsonImage DownloadJsonImage()
        {
            using (var client = new WebClient())
            {
                Log.Debug("Getting todays wallpaper info");
                var body = client.DownloadString(@"http://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=de-De");
                Log.Debug("Received todays wallpaper info");
                var data = JsonConvert.DeserializeObject<RootObject>(body);
                if (data == null)
                {
                    throw new WallpaperException("The received data is not valid");
                }
                return new JsonImage(data);
            }
        }
    }
}