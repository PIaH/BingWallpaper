using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingWallpaper.Lib.Logging
{
    class Logger
    {
        private static Logger instance;

        private Logger()
        {

        }

        public static Logger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Logger();
                }
                return instance;
            }
        }

        public void Info(string format, params object[] args)
        {
            Log(LogLevel.INFO, format, args);
        }

        public void Debug(string format, params object[] args)
        {
            Log(LogLevel.DEBUG, format, args);
        }

        private void Log(LogLevel level, string format, params object[] args)
        {
            Console.WriteLine(level + ": " + format, args);
        }
    }
}
