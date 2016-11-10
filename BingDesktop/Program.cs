using BingDesktop.API;
using BingDesktop.Data;
using BingDesktop.Misc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BingDesktop
{
    class Program
    {
        private const string ENDPOINT = @"/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=de-De";
        private const string HOST = @"http://www.bing.com";
        private const string EXT = @".jpg";
        private const string IMAGEDIR = @"Wallpapers/";

        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                await MainInternal(args);
            }).Wait();

#if DEBUG
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
#endif
        }

        static async Task MainInternal(string[] args)
        {
            Header.Print();
            try
            {
                using (var client = new HttpClient())
                {
                    Out("Getting todays wallpaper info");
                    var result = await client.GetAsync(HOST + ENDPOINT);
                    Out("Received todays wallpaper info");
                    var body = await result.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<RootObject>(body);
                    if (data == null)
                    {
                        Out("The received data is not valid");
                        return;
                    }
                    var image = data.images.FirstOrDefault();
                    if (image == null)
                    {
                        Out("The received data contains no image information");
                        return;
                    }
                    var downloadedImagePath = GetImagePath(image);
                    if (File.Exists(downloadedImagePath))
                    {
                        Out("The image file has already been downloaded");
                    }
                    else
                    {
                        if (!Directory.Exists(IMAGEDIR))
                        {
                            Out("Creating directory for wallpapers");
                            var directoryInfo = Directory.CreateDirectory(IMAGEDIR);
                            if (!directoryInfo.Exists)
                            {
                                Out("Could not create the directory for saving the image file");
                                return;
                            }
                        }
                        Out("Downloading image file");
                        using (WebClient webClient = new WebClient())
                        {
                            var imageurl = HOST + image.url;
                            await webClient.DownloadFileTaskAsync(imageurl, downloadedImagePath);
                        }
                    }
                    var wallpaperFilename = downloadedImagePath;
                    var losungenFile = GetLosungenFilename();
                    if (File.Exists(losungenFile))
                    {
                        Out("Enhancing wallpaper with text");
                        wallpaperFilename = Wallpaper.CreateWallpaperWithText(downloadedImagePath, Losungen.ReadFromXml(losungenFile, DateTime.Now));
                    }
                    else
                    {
                        Out("Skip enhanching the wallpaper with text, because there is no text data for that.");
                        Out("Please install an up-to-date version of the Losungen.");
                        Out("Please get the file " + losungenFile + " and put it into the proper directory so that it can be loaded.");
                        Out("Please see: http://www.losungen.de/download/");
                    }
                    Out("Setting wallpaper");
                    Wallpaper.Set(wallpaperFilename, Wallpaper.Style.Stretched);
                    Out("Wallpaper successfully applied");
                }
            }
            catch (Exception ex)
            {
                Out("An error occurred: " + ex.Message);
            }
        }

        private static string GetLosungenFilename()
        {
            return @"Losungen\Losungen Free " + DateTime.Now.Year + ".xml";
        }

        private static string GetImagePath(Image image)
        {
            return IMAGEDIR + "/" + image.enddate + EXT;
        }

        private static void Out(string format, params string[] arg)
        {
            Console.WriteLine(format, arg);
        }
    }
}