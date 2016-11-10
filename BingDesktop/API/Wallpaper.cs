using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;

namespace BingDesktop.API
{
    public sealed class Wallpaper
    {
        Wallpaper() { }

        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public enum Style : int
        {
            Tiled,
            Centered,
            Stretched
        }

        public static void Set(string path, Style style)
        {
            Image img = Image.FromFile(path);
            string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
            img.Save(tempPath, System.Drawing.Imaging.ImageFormat.Bmp);

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            if (style == Style.Stretched)
            {
                key.SetValue(@"WallpaperStyle", 2.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            }

            if (style == Style.Centered)
            {
                key.SetValue(@"WallpaperStyle", 1.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            }

            if (style == Style.Tiled)
            {
                key.SetValue(@"WallpaperStyle", 1.ToString());
                key.SetValue(@"TileWallpaper", 1.ToString());
            }

            SystemParametersInfo(SPI_SETDESKWALLPAPER,
                0,
                tempPath,
                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }

        private static Font FindFont(Graphics g, string longString, Size Room, Font PreferedFont)
        {
            SizeF RealSize = g.MeasureString(longString, PreferedFont);
            float HeightScaleRatio = Room.Height / RealSize.Height;
            float WidthScaleRatio = Room.Width / RealSize.Width;
            float ScaleRatio = (HeightScaleRatio < WidthScaleRatio) ? ScaleRatio = HeightScaleRatio : ScaleRatio = WidthScaleRatio;
            float ScaleFontSize = PreferedFont.Size * ScaleRatio;
            return new Font(PreferedFont.FontFamily, ScaleFontSize);
        }

        public static string CreateWallpaperWithText(string imagePath, string text)
        {
            var nl = Environment.NewLine;
            int margin = 60;

            var fi = new FileInfo(imagePath);
            var dir = fi.Directory.Name;
            var ext = fi.Extension;
            var filename = fi.Name.Substring(0, fi.Name.Length - ext.Length);

            var newFilename = Path.Combine(dir, filename + "_losungen" + ext).ToString();

            using (var bmp = new Bitmap(imagePath))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    var roomForText = new Size(Convert.ToInt32(bmp.Width * 0.6) - margin - margin, bmp.Height - margin - margin);
                    using (var prefferedFont = new Font("Segoe WP", 10, FontStyle.Regular, GraphicsUnit.Pixel))
                    {
                        using (var font = FindFont(g, text, roomForText, prefferedFont))
                        {
                            using (var backgroundBrush = new SolidBrush(Color.FromArgb(100, 0xff, 0xff, 0xff)))
                            {
                                var textSize = g.MeasureString(text, font);
                                int x = margin + margin, y = Convert.ToInt32((bmp.Height - textSize.Height) / 2.0);
                                g.FillRectangle(backgroundBrush, x - margin, y - margin, textSize.Width + margin + margin, textSize.Height + margin + margin);
                                g.SmoothingMode = SmoothingMode.AntiAlias;
                                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                StringFormat stringFormat = new StringFormat();
                                //stringFormat.Alignment = StringAlignment.Center;
                                //stringFormat.LineAlignment = StringAlignment.Center;
                                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                                g.DrawString(text, font, Brushes.Black, x, y, stringFormat);

                                g.Flush();

                                bmp.Save(newFilename);
                            }
                        }
                    }
                }
            }
            // delete old image
            File.Delete(imagePath);

            return newFilename;
        }
    }
}
