using System;
using System.Configuration;

namespace Persistence.LogService
{
    /// <summary>
    /// Enhanced base class for objects that need logging capabilities.
    /// Provides both backward compatibility and new enhanced features.
    /// </summary>
    public class EnhancedLoggeable : IDisposable
    {
        protected readonly IEnhancedLogger _logger;
        protected readonly string _category;
        private bool _disposed = false;

        public EnhancedLoggeable() : this("General")
        {
        }

        public EnhancedLoggeable(string category)
        {
            _category = category;
            _logger = LogManager.GetLogger(category);
        }

        // Enhanced logging methods
        protected void LogDebug(string message, string operation = null)
        {
            _logger.Debug(message, operation);
        }

        protected void LogInfo(string message, string operation = null)
        {
            _logger.Info(message, operation);
        }

        protected void LogWarning(string message, string operation = null)
        {
            _logger.Warning(message, operation);
        }

        protected void LogError(string message, Exception exception = null, string operation = null)
        {
            _logger.Error(message, exception, operation);
        }

        protected void LogCritical(string message, Exception exception = null, string operation = null)
        {
            _logger.Critical(message, exception, operation);
        }

        // Backward compatibility method - maps to Info level
        protected void Log(string message)
        {
            _logger.Info(message);
        }

        // Context management
        protected void SetLogContext(string user, string sessionId)
        {
            _logger.SetContext(user, sessionId);
        }

        protected void SetLogLevel(LogLevel level)
        {
            _logger.SetMinimumLevel(level);
        }

        // Performance logging helpers
        protected void LogPerformance(string operation, long durationMs)
        {
            if (durationMs > 1000)
            {
                LogWarning(string.Format("Slow operation: {0} took {1}ms", operation, durationMs), "PERFORMANCE");
            }
            else
            {
                LogDebug(string.Format("Operation: {0} completed in {1}ms", operation, durationMs), "PERFORMANCE");
            }
        }

        protected T LoggedExecute<T>(Func<T> operation, string operationName, T defaultValue = default(T))
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            
            try
            {
                LogDebug(string.Format("Starting {0}", operationName), operationName);
                var result = operation();
                
                stopwatch.Stop();
                LogInfo(string.Format("Completed {0} in {1}ms", operationName, stopwatch.ElapsedMilliseconds), operationName);
                
                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                LogError(string.Format("Failed {0} after {1}ms", operationName, stopwatch.ElapsedMilliseconds), ex, operationName);
                return defaultValue;
            }
        }

        protected void LoggedExecute(Action operation, string operationName)
        {
            LoggedExecute(() => { operation(); return true; }, operationName);
        }

        public virtual void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                _logger?.Dispose();
            }
        }
    }

}