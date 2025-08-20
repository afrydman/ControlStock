using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Helper.LogService
{
    public class Logger : ILogger
    {
        private StreamWriter _log;
        private readonly object _lockObject = new object();
        private readonly string _baseFilePath;
        private DateTime _currentLogDate;
        private bool _disposed = false;

        public LogLevel MinimumLevel { get; set; } = LogLevel.Info;
        public string FilePath { get; private set; }

        public Logger(string file)
        {
            _baseFilePath = file;
            _currentLogDate = DateTime.Today;
            InitializeLogFile();
        }

        private void InitializeLogFile()
        {
            string directory = Path.GetDirectoryName(_baseFilePath);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(_baseFilePath);
            string extension = Path.GetExtension(_baseFilePath);
            
            // Create filename with date: filename_yyyy-MM-dd.ext
            string dateString = _currentLogDate.ToString("yyyy-MM-dd");
            FilePath = Path.Combine(directory, $"{fileNameWithoutExt}_{dateString}{extension}");
            
            // Ensure directory exists
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            
            try
            {
                _log = new StreamWriter(FilePath, true);
                _log.AutoFlush = true; // Auto-flush for safety
            }
            catch (Exception ex)
            {
                // Fallback to a temp file if cannot write to specified location
                FilePath = Path.GetTempFileName();
                _log = new StreamWriter(FilePath, true);
                _log.AutoFlush = true;
                _log.WriteLine($"Error creating log file at {_baseFilePath}: {ex.Message}. Using temp file: {FilePath}");
            }
        }

        private void CheckAndRotateLog()
        {
            if (DateTime.Today > _currentLogDate)
            {
                lock (_lockObject)
                {
                    if (DateTime.Today > _currentLogDate)
                    {
                        // Close current log file
                        _log?.Close();
                        _log?.Dispose();
                        
                        // Initialize new log file with new date
                        _currentLogDate = DateTime.Today;
                        InitializeLogFile();
                    }
                }
            }
        }

        public virtual void Log(string message)
        {
            Log(LogLevel.Info, message);
        }

        public virtual void Log(LogLevel level, string message)
        {
            if (level < MinimumLevel)
                return;

            CheckAndRotateLog();
            
            lock (_lockObject)
            {
                try
                {
                    string levelString = level.ToString().ToUpper().PadRight(7);
                    string threadId = Thread.CurrentThread.ManagedThreadId.ToString().PadLeft(3);
                    _log.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{levelString}] [Thread:{threadId}] {message}");
                }
                catch (Exception ex)
                {
                    // Silently fail or could write to Windows Event Log
                    Console.WriteLine($"Logging failed: {ex.Message}");
                }
            }
        }

        public void LogDebug(string message)
        {
            Log(LogLevel.Debug, message);
        }

        public void LogInfo(string message)
        {
            Log(LogLevel.Info, message);
        }

        public void LogWarning(string message)
        {
            Log(LogLevel.Warning, message);
        }

        public void LogError(string message)
        {
            Log(LogLevel.Error, message);
        }

        public void LogError(string message, Exception ex)
        {
            Log(LogLevel.Error, $"{message} - Exception: {ex?.GetType().Name}: {ex?.Message}");
            if (ex?.StackTrace != null)
            {
                Log(LogLevel.Error, $"StackTrace: {ex.StackTrace}");
            }
            if (ex?.InnerException != null)
            {
                LogError("Inner Exception", ex.InnerException);
            }
        }

        public void LogFatal(string message)
        {
            Log(LogLevel.Fatal, message);
        }

        public void LogFatal(string message, Exception ex)
        {
            Log(LogLevel.Fatal, $"{message} - Exception: {ex?.GetType().Name}: {ex?.Message}");
            if (ex?.StackTrace != null)
            {
                Log(LogLevel.Fatal, $"StackTrace: {ex.StackTrace}");
            }
            if (ex?.InnerException != null)
            {
                LogFatal("Inner Exception", ex.InnerException);
            }
        }

        public virtual void LogStrings(string section, IList<string> strings)
        {
            foreach (var str in strings)
            {
                Log(LogLevel.Info, $"{section} >>> {str}");
            }
        }

        public void Flush()
        {
            lock (_lockObject)
            {
                try
                {
                    _log?.Flush();
                }
                catch
                {
                    // Silently fail
                }
            }
        }

        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    lock (_lockObject)
                    {
                        _log?.Flush();
                        _log?.Close();
                        _log?.Dispose();
                        _log = null;
                    }
                }
                _disposed = true;
            }
        }

        ~Logger()
        {
            Dispose(false);
        }
    }
}