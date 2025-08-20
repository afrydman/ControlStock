using System;
using System.Collections.Generic;
using System.Linq;

namespace Helper.LogService
{
    public static class Log
    {
        private static readonly Dictionary<string, ILogger> _loggers = new Dictionary<string, ILogger>();
        private static readonly object _lockObject = new object();
        private static LogLevel _defaultMinimumLevel = LogLevel.Info;

        public static LogLevel DefaultMinimumLevel
        {
            get { return _defaultMinimumLevel; }
            set
            {
                _defaultMinimumLevel = value;
                lock (_lockObject)
                {
                    foreach (var logger in _loggers.Values)
                    {
                        logger.MinimumLevel = value;
                    }
                }
            }
        }

        public static void New(string loggerId, string file, LogLevel? minimumLevel = null)
        {
            lock (_lockObject)
            {
                // Check for conflicts
                var existingLogger = _loggers.FirstOrDefault(kvp => kvp.Value.FilePath == file && kvp.Key != loggerId);
                if (existingLogger.Value != null)
                {
                    throw new Exception($"Ya hay un logger escribiendo en ese archivo, pero con un id diferente: {existingLogger.Key}");
                }

                // If logger already exists with same ID, dispose old one
                if (_loggers.ContainsKey(loggerId))
                {
                    if (_loggers[loggerId].FilePath != file)
                    {
                        throw new Exception("Ya hay un logger escribiendo con ese id, pero en un distinto archivo");
                    }
                    return; // Already exists with same file, reuse it
                }

                var logger = new Logger(file);
                logger.MinimumLevel = minimumLevel ?? _defaultMinimumLevel;
                _loggers.Add(loggerId, logger);
            }
        }

        public static ILogger Get(string loggerId)
        {
            lock (_lockObject)
            {
                if (!_loggers.ContainsKey(loggerId))
                {
                    throw new Exception($"Para obtener el Logger debe crearlo primero haciendo Log.New(). Logger ID: {loggerId}");
                }
                return _loggers[loggerId];
            }
        }

        public static void Write(string loggerId, string message)
        {
            Write(loggerId, LogLevel.Info, message);
        }

        public static void Write(string loggerId, LogLevel level, string message)
        {
            var logger = Get(loggerId);
            logger.Log(level, message);
        }

        public static void WriteDebug(string loggerId, string message)
        {
            Write(loggerId, LogLevel.Debug, message);
        }

        public static void WriteInfo(string loggerId, string message)
        {
            Write(loggerId, LogLevel.Info, message);
        }

        public static void WriteWarning(string loggerId, string message)
        {
            Write(loggerId, LogLevel.Warning, message);
        }

        public static void WriteError(string loggerId, string message)
        {
            Write(loggerId, LogLevel.Error, message);
        }

        public static void WriteError(string loggerId, string message, Exception ex)
        {
            var logger = Get(loggerId);
            logger.LogError(message, ex);
        }

        public static void WriteFatal(string loggerId, string message)
        {
            Write(loggerId, LogLevel.Fatal, message);
        }

        public static void WriteFatal(string loggerId, string message, Exception ex)
        {
            var logger = Get(loggerId);
            logger.LogFatal(message, ex);
        }

        public static void Flush(string loggerId)
        {
            var logger = Get(loggerId);
            logger.Flush();
        }

        public static void FlushAll()
        {
            lock (_lockObject)
            {
                foreach (var logger in _loggers.Values)
                {
                    logger.Flush();
                }
            }
        }

        public static void Close(string loggerId)
        {
            lock (_lockObject)
            {
                if (_loggers.ContainsKey(loggerId))
                {
                    _loggers[loggerId].Dispose();
                    _loggers.Remove(loggerId);
                }
            }
        }

        public static void CloseAll()
        {
            lock (_lockObject)
            {
                foreach (var logger in _loggers.Values)
                {
                    logger.Dispose();
                }
                _loggers.Clear();
            }
        }

        public static bool Exists(string loggerId)
        {
            lock (_lockObject)
            {
                return _loggers.ContainsKey(loggerId);
            }
        }

        public static IEnumerable<string> GetLoggerIds()
        {
            lock (_lockObject)
            {
                return _loggers.Keys.ToList();
            }
        }
    }
}