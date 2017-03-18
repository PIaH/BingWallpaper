using BingWallpaper.Lib.Data;
using BingWallpaper.Lib.Exceptions;
using System.Linq;

namespace BingWallpaper.Lib.Core
{
    public class JsonImage
    {
        private Image json;

        public JsonImage(RootObject json)
        {
            if (json == null
                || json.images == null
                || json.images.Count == 0
                || string.IsNullOrEmpty(json.images.First().url)
                || string.IsNullOrEmpty(json.images.First().copyright))
            {
                throw new WallpaperException("Invalid image json data");
            }

            this.json = json.images.First();
        }

        public string Url
        {
            get
            {
                return string.Format("http://www.bing.com{0}", json.url);
            }
        }

        public string Copyright
        {
            get
            {
                return json.copyright;
            }
        }
    }
}