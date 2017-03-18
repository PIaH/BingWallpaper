using BingWallpaper.Lib.Core;
using System;
using System.Windows;
using System.Windows.Threading;

namespace BingWallpaper.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
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
                MessageBox.Show("There was an error: " + Environment.NewLine + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                this.Close();
            };
            timer.Start();
        }

    }
}
