using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace TradeUnionCommittee.Web.GUI.Logger
{
    public class FileLogger : ILogger
    {
        private readonly string _filePath;
        private readonly object _lock = new object();
        public FileLogger(string path)
        {
            _filePath = path;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            //return logLevel == LogLevel.Trace;
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter == null) return;
            lock (_lock)
            {
                File.AppendAllText(_filePath, DateTime.Now + " -> " + formatter(state, exception) + Environment.NewLine);
            }
        }
    }
}