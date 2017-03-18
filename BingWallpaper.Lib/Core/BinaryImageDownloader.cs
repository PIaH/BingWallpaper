using BingWallpaper.Lib.Exceptions;
using BingWallpaper.Lib.Logging;
using System;
using System.IO;
using System.Net;

namespace BingWallpaper.Lib.Core
{
    public class BinaryImageDownloader
    {
        private static Logger Log = Logger.Instance;

        public Stream DownloadImage(JsonImage image)
        {
            Log.Debug("Downloading image stream");
            using (var webClient = new WebClient())
            {
                try
                {
                    var bytes = webClient.DownloadData(image.Url);
                    if (bytes == null || bytes.Length == 0)
                    {
                        throw new WallpaperException("The received image stream is empty");
                    }
                    Log.Debug("Received the image as stream ({0:0.##} MBytes)", (bytes.Length / 1024.0 / 1024.0));
                    return new MemoryStream(bytes);
                }
                catch (WebException exception)
                {
                    throw new WallpaperException("Image stream could not be received", exception);
                }
                catch (ArgumentNullException exception)
                {
                    throw new WallpaperException("Invalid image Url", exception);
                }
                catch (NotSupportedException exception)
                {
                    throw new WallpaperException("The web operation is not supported", exception);
                }
            }
        }
    }
}
