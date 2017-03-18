using System;

namespace BingWallpaper.Lib.Exceptions
{
    class WallpaperException : Exception
    {
        public WallpaperException()
        {

        }

        public WallpaperException(string message)
            : base(message)
        {

        }

        public WallpaperException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
