using BingDesktop.API;
using BingDesktop.Data;
using BingDesktop.Misc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BingDesktop
{
    class Program
    {
        private const string ENDPOINT = @"/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=de-De";
        private const string HOST = @"http://www.bing.com";
        private const string EXT = @".jpg";

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
                        Out("The received answer is not valid");
                        return;
                    }
                    var image = data.images.FirstOrDefault();
                    if (image == null)
                    {
                        Out("The received answer contains no image information");
                        return;
                    }
                    var imageurl = HOST + image.url;
                    if (File.Exists(image.enddate + EXT))
                    {
                        Out("The image file has already been downloaded");
                    }
                    else
                    {
                        Out("Downloading image file");
                        using (WebClient webClient = new WebClient())
                        {
                            await webClient.DownloadFileTaskAsync(imageurl, image.enddate + EXT);
                        }
                    }
                    Out("Enhancing wallpaper with text");
                    var wallpaperFilename = Wallpaper.CreateWallpaperWithText(image.enddate + EXT, Losungen.ReadFromXml(@"Losungen\Losungen Free 2016.xml", DateTime.Now));
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

        private static void Out(string format, params string[] arg)
        {
            Console.WriteLine(format, arg);
        }
    }
}