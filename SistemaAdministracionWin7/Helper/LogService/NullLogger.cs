using System;
using System.Collections.Generic;

namespace Helper.LogService
{
    public class NullLogger : ILogger
    {
        public LogLevel MinimumLevel { get; set; } = LogLevel.Debug;
        public string FilePath { get; private set; }

        public void Close()
        {
        }

        public void Dispose()
        {
        }

        public void Flush()
        {
        }

        public void Log(string message)
        {
        }

        public void Log(LogLevel level, string message)
        {
        }

        public void LogDebug(string message)
        {
        }

        public void LogInfo(string message)
        {
        }

        public void LogWarning(string message)
        {
        }

        public void LogError(string message)
        {
        }

        public void LogError(string message, Exception ex)
        {
        }

        public void LogFatal(string message)
        {
        }

        public void LogFatal(string message, Exception ex)
        {
        }

        public void LogStrings(string section, IList<string> strings)
        {
        }
    }
}