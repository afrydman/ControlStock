using System;
using System.Collections.Generic;

namespace Helper.LogService
{
    public interface ILogger : IDisposable
    {
        void Close();
        void Log(string message);
        void Log(LogLevel level, string message);
        void LogDebug(string message);
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogError(string message, Exception ex);
        void LogFatal(string message);
        void LogFatal(string message, Exception ex);
        void LogStrings(string section, IList<string> strings);
        void Flush();
        string FilePath { get; }
        LogLevel MinimumLevel { get; set; }
    }
}