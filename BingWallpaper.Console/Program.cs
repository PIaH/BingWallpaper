using BingWallpaper.Lib.Core;
using System;

namespace BingWallpaper.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Header.Print();
            try
            {
                var jsonImageDownloader = new JsonImageDownloader();
                var jsonImage = jsonImageDownloader.DownloadJsonImage();
                using (var stream = new BinaryImageDownloader().DownloadImage(jsonImage))
                {
                    var imagefile = new ImageWatermarker().CreateWaterMarkedImage(stream, DateTime.Now);
                    Wallpaper.Set(imagefile, Wallpaper.Style.Stretched);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("There was an error: " + ex.Message);
            }

#if DEBUG
            System.Console.WriteLine("Press any key to exit");
            System.Console.ReadKey();
#endif
        }
    }
}
