using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;

namespace Persistence.LogService
{
    public enum LogLevel
    {
        Debug = 0,
        Info = 1,
        Warning = 2,
        Error = 3,
        Critical = 4
    }

    public class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public LogLevel Level { get; set; }
        public string Category { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
        public string SessionId { get; set; }
        public string Operation { get; set; }
        public Exception Exception { get; set; }
        public string ThreadId { get; set; }
        public string MachineName { get; set; }

        public LogEntry()
        {
            Timestamp = DateTime.Now;
            ThreadId = Thread.CurrentThread.ManagedThreadId.ToString();
            MachineName = Environment.MachineName;
        }
    }

    public interface IEnhancedLogger : IDisposable
    {
        void Debug(string message, string operation = null);
        void Info(string message, string operation = null);
        void Warning(string message, string operation = null);
        void Error(string message, Exception exception = null, string operation = null);
        void Critical(string message, Exception exception = null, string operation = null);
        void Log(LogEntry entry);
        void SetContext(string user, string sessionId);
        void SetMinimumLevel(LogLevel level);
    }

    public class EnhancedLogger : IEnhancedLogger
    {
        private readonly string _category;
        private readonly string _logDirectory;
        private readonly object _lockObject = new object();
        private LogLevel _minimumLevel = LogLevel.Debug;
        private string _currentUser = "";
        private string _currentSession = "";
        private Timer _flushTimer;
        private readonly ConcurrentQueue<LogEntry> _logQueue;
        private readonly StringBuilder _buffer;
        private bool _disposed = false;

        public EnhancedLogger(string category)
        {
            _category = category;
            _logDirectory = GetLogDirectory();
            _logQueue = new ConcurrentQueue<LogEntry>();
            _buffer = new StringBuilder();
            
            // Ensure log directory exists
            if (!Directory.Exists(_logDirectory))
                Directory.CreateDirectory(_logDirectory);

            // Start flush timer - flush every 5 seconds
            _flushTimer = new Timer(FlushLogs, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
            
            Info($"Logger initialized for category: {category}");
        }

        public void SetContext(string user, string sessionId)
        {
            _currentUser = user ?? "";
            _currentSession = sessionId ?? "";
        }

        public void SetMinimumLevel(LogLevel level)
        {
            _minimumLevel = level;
        }

        public void Debug(string message, string operation = null)
        {
            Log(new LogEntry
            {
                Level = LogLevel.Debug,
                Category = _category,
                Message = message,
                User = _currentUser,
                SessionId = _currentSession,
                Operation = operation
            });
        }

        public void Info(string message, string operation = null)
        {
            Log(new LogEntry
            {
                Level = LogLevel.Info,
                Category = _category,
                Message = message,
                User = _currentUser,
                SessionId = _currentSession,
                Operation = operation
            });
        }

        public void Warning(string message, string operation = null)
        {
            Log(new LogEntry
            {
                Level = LogLevel.Warning,
                Category = _category,
                Message = message,
                User = _currentUser,
                SessionId = _currentSession,
                Operation = operation
            });
        }

        public void Error(string message, Exception exception = null, string operation = null)
        {
            Log(new LogEntry
            {
                Level = LogLevel.Error,
                Category = _category,
                Message = message,
                User = _currentUser,
                SessionId = _currentSession,
                Operation = operation,
                Exception = exception
            });
        }

        public void Critical(string message, Exception exception = null, string operation = null)
        {
            Log(new LogEntry
            {
                Level = LogLevel.Critical,
                Category = _category,
                Message = message,
                User = _currentUser,
                SessionId = _currentSession,
                Operation = operation,
                Exception = exception
            });
        }

        public void Log(LogEntry entry)
        {
            if (entry.Level < _minimumLevel)
                return;

            // Add to queue for async processing
            _logQueue.Enqueue(entry);
        }

        private void FlushLogs(object state)
        {
            if (_disposed) return;

            try
            {
                var entries = new System.Collections.Generic.List<LogEntry>();
                
                // Dequeue all pending entries
                while (_logQueue.TryDequeue(out LogEntry entry))
                {
                    entries.Add(entry);
                }

                if (entries.Count == 0) return;

                // Group entries by date for daily log files
                var groupedEntries = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<LogEntry>>();
                
                foreach (var entry in entries)
                {
                    string dateKey = entry.Timestamp.ToString("yyyy-MM-dd");
                    if (!groupedEntries.ContainsKey(dateKey))
                        groupedEntries[dateKey] = new System.Collections.Generic.List<LogEntry>();
                    
                    groupedEntries[dateKey].Add(entry);
                }

                // Write to appropriate files
                foreach (var group in groupedEntries)
                {
                    WriteToFile(group.Key, group.Value);
                }
            }
            catch (Exception ex)
            {
                // Fallback logging - write to Windows Event Log or console
                try
                {
                    var fallbackMessage = $"EnhancedLogger flush failed: {ex.Message}";
                    System.Diagnostics.EventLog.WriteEntry("ControlStock", fallbackMessage, System.Diagnostics.EventLogEntryType.Error);
                }
                catch
                {
                    // Last resort - write to console
                    Console.WriteLine($"CRITICAL: Logger flush failed: {ex.Message}");
                }
            }
        }

        private void WriteToFile(string date, System.Collections.Generic.List<LogEntry> entries)
        {
            var logFilePath = Path.Combine(_logDirectory, $"{date}_{_category}.log");
            
            lock (_lockObject)
            {
                try
                {
                    using (var writer = new StreamWriter(logFilePath, true, Encoding.UTF8))
                    {
                        foreach (var entry in entries)
                        {
                            var logLine = FormatLogEntry(entry);
                            writer.WriteLine(logLine);
                        }
                        writer.Flush();
                    }
                }
                catch (Exception ex)
                {
                    // Try alternative location if primary fails
                    var fallbackPath = Path.Combine(Path.GetTempPath(), $"ControlStock_{date}_{_category}.log");
                    try
                    {
                        using (var writer = new StreamWriter(fallbackPath, true, Encoding.UTF8))
                        {
                            writer.WriteLine($"FALLBACK LOG - Original path failed: {logFilePath}");
                            writer.WriteLine($"Error: {ex.Message}");
                            
                            foreach (var entry in entries)
                            {
                                var logLine = FormatLogEntry(entry);
                                writer.WriteLine(logLine);
                            }
                        }
                    }
                    catch
                    {
                        // Give up - can't write anywhere
                    }
                }
            }
        }

        private string FormatLogEntry(LogEntry entry)
        {
            var sb = new StringBuilder();
            
            // Timestamp
            sb.Append(entry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            sb.Append(" | ");
            
            // Level
            sb.Append($"{entry.Level.ToString().ToUpper().PadRight(8)}");
            sb.Append(" | ");
            
            // Category
            sb.Append($"{entry.Category.PadRight(20)}");
            sb.Append(" | ");
            
            // Thread
            sb.Append($"T{entry.ThreadId.PadLeft(3)}");
            sb.Append(" | ");
            
            // User and Session (if available)
            if (!string.IsNullOrEmpty(entry.User))
            {
                sb.Append($"U:{entry.User} ");
            }
            if (!string.IsNullOrEmpty(entry.SessionId))
            {
                sb.Append($"S:{entry.SessionId.Substring(0, Math.Min(8, entry.SessionId.Length))} ");
            }
            
            // Operation (if available)
            if (!string.IsNullOrEmpty(entry.Operation))
            {
                sb.Append($"[{entry.Operation}] ");
            }
            
            // Message
            sb.Append(entry.Message);
            
            // Exception (if available)
            if (entry.Exception != null)
            {
                sb.AppendLine();
                sb.Append("    EXCEPTION: ");
                sb.Append(entry.Exception.Message);
                sb.AppendLine();
                sb.Append("    STACKTRACE: ");
                sb.Append(entry.Exception.StackTrace?.Replace("\r\n", "\r\n    "));
            }
            
            return sb.ToString();
        }

        private string GetLogDirectory()
        {
            try
            {
                var configPath = ConfigurationManager.AppSettings["CarpetaLogs"];
                if (!string.IsNullOrEmpty(configPath) && Directory.Exists(Path.GetDirectoryName(configPath)))
                {
                    return configPath;
                }
            }
            catch
            {
                // Ignore config errors
            }
            
            // Fallback to temp directory
            return Path.Combine(Path.GetTempPath(), "ControlStock", "Logs");
        }

        public void Dispose()
        {
            if (_disposed) return;
            
            _disposed = true;
            
            // Stop the timer
            _flushTimer?.Dispose();
            
            // Final flush
            FlushLogs(null);
            
            Info($"Logger disposed for category: {_category}");
        }
    }

    // Static factory for easy access
    public static class LogManager
    {
        private static readonly ConcurrentDictionary<string, IEnhancedLogger> _loggers = 
            new ConcurrentDictionary<string, IEnhancedLogger>();

        public static IEnhancedLogger GetLogger(string category)
        {
            return _loggers.GetOrAdd(category, cat => new EnhancedLogger(cat));
        }

        public static void SetGlobalContext(string user, string sessionId)
        {
            foreach (var logger in _loggers.Values)
            {
                logger.SetContext(user, sessionId);
            }
        }

        public static void SetGlobalMinimumLevel(LogLevel level)
        {
            foreach (var logger in _loggers.Values)
            {
                logger.SetMinimumLevel(level);
            }
        }

        public static void Shutdown()
        {
            foreach (var logger in _loggers.Values)
            {
                logger.Dispose();
            }
            _loggers.Clear();
        }
    }
}