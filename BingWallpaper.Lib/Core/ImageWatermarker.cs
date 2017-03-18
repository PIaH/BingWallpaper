using BingWallpaper.Lib.Exceptions;
using BingWallpaper.Lib.Logging;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace BingWallpaper.Lib.Core
{
    public class ImageWatermarker
    {
        private static Logger Log = Logger.Instance;

        public string CreateWaterMarkedImage(Stream image, DateTime timestamp)
        {
            var losungenFile = GetLosungenFilename();
            if (!File.Exists(losungenFile))
            {
                var sb = new StringBuilder();
                sb.AppendLine("Skip enhanching the wallpaper with text, because there is no text data for that.")
                    .AppendLine("Please install an up-to-date version of the Losungen.")
                    .AppendLine("Please get the file " + losungenFile + " and put it into the proper directory so that it can be loaded.")
                    .AppendLine("Please see: http://www.losungen.de/download/");
                throw new WallpaperException(sb.ToString());
            }

            Log.Debug("Enhancing wallpaper with text");
            var filename = @"Wallpapers\wallpaper_" + timestamp.ToString("yyyy_MM_dd") + ".jpg";
            Wallpaper.CreateWallpaperWithText(image, ImageFormat.Bmp, Losungen.ReadFromXml(losungenFile, timestamp), filename);
            return filename;
        }

        private string GetLosungenFilename()
        {
            return @"Losungen\Losungen Free " + DateTime.Now.Year + ".xml";
        }

    }
}
