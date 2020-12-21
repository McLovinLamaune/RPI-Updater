using System;
using System.Collections.Generic;
using System.Text;

namespace RPI_Updater.Managers
{
    public enum LogTag
    {
        INFO,
        WARNING,
        DEBUG,
        ERROR,
        CRITICAL
    }
    public class LoggerManager
    {
        private static readonly Lazy<LoggerManager> lazy = new Lazy<LoggerManager>(() => new LoggerManager());

        public static LoggerManager Instance { get { return lazy.Value; } }

        private LoggerManager()
        {
        }

        public void Log(LogTag tag, string message)
        {
            string log = "[" + DateTime.Now.ToString("HH:mm:ss") + "]" + "[" + tag.ToString() + "] - " + message;
            Console.WriteLine(log);
        }
    }
}

