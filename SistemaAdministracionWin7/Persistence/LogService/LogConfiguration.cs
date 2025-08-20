using System;
using System.Configuration;
using System.IO;

namespace Persistence.LogService
{
    public static class LogConfiguration
    {
        public static LogLevel GetMinimumLogLevel()
        {
            var levelString = ConfigurationManager.AppSettings["LogLevel"];
            if (Enum.TryParse<LogLevel>(levelString, true, out LogLevel level))
                return level;
            
            return LogLevel.Info; // Default level
        }

        public static string GetLogDirectory()
        {
            try
            {
                var configPath = ConfigurationManager.AppSettings["CarpetaLogs"];
                if (!string.IsNullOrEmpty(configPath))
                {
                    var expandedPath = Environment.ExpandEnvironmentVariables(configPath);
                    
                    // Ensure directory exists
                    if (!Directory.Exists(expandedPath))
                        Directory.CreateDirectory(expandedPath);
                    
                    return expandedPath;
                }
            }
            catch (Exception ex)
            {
                // Log to event log if possible
                try
                {
                    System.Diagnostics.EventLog.WriteEntry("ControlStock", 
                        $"Failed to create log directory: {ex.Message}", 
                        System.Diagnostics.EventLogEntryType.Warning);
                }
                catch { }
            }
            
            // Fallback to temp directory
            var fallbackPath = Path.Combine(Path.GetTempPath(), "ControlStock", "Logs");
            try
            {
                if (!Directory.Exists(fallbackPath))
                    Directory.CreateDirectory(fallbackPath);
            }
            catch { }
            
            return fallbackPath;
        }

        public static bool IsStructuredLoggingEnabled()
        {
            var enabledString = ConfigurationManager.AppSettings["StructuredLogging"];
            return bool.TryParse(enabledString, out bool enabled) && enabled;
        }

        public static int GetLogFlushIntervalSeconds()
        {
            var intervalString = ConfigurationManager.AppSettings["LogFlushIntervalSeconds"];
            if (int.TryParse(intervalString, out int interval) && interval > 0)
                return interval;
            
            return 5; // Default 5 seconds
        }

        public static int GetLogRetentionDays()
        {
            var retentionString = ConfigurationManager.AppSettings["LogRetentionDays"];
            if (int.TryParse(retentionString, out int days) && days > 0)
                return days;
            
            return 30; // Default 30 days
        }

        public static void InitializeLogging(string applicationName, string userId = null)
        {
            try
            {
                // Set global minimum level
                var minLevel = GetMinimumLogLevel();
                LogManager.SetGlobalMinimumLevel(minLevel);
                
                // Set context if provided
                if (!string.IsNullOrEmpty(userId))
                {
                    var sessionId = Guid.NewGuid().ToString("N").Substring(0, 8);
                    LogManager.SetGlobalContext(userId, sessionId);
                }
                
                // Log initialization
                var logger = LogManager.GetLogger("System");
                logger.Info($"{applicationName} logging initialized - Level: {minLevel}, Directory: {GetLogDirectory()}");
                
                // Schedule cleanup task
                ScheduleLogCleanup();
            }
            catch (Exception ex)
            {
                // Fallback logging
                try
                {
                    var fallbackPath = Path.Combine(Path.GetTempPath(), "ControlStock_InitError.log");
                    File.AppendAllText(fallbackPath, 
                        $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Logging initialization failed: {ex.Message}\r\n");
                }
                catch { }
            }
        }

        private static void ScheduleLogCleanup()
        {
            try
            {
                var timer = new System.Threading.Timer(CleanupOldLogs, null, 
                    TimeSpan.FromHours(1), // First cleanup after 1 hour
                    TimeSpan.FromDays(1)); // Then daily
                
                // Keep timer reference to prevent GC
                _cleanupTimer = timer;
            }
            catch (Exception ex)
            {
                var logger = LogManager.GetLogger("System");
                logger.Warning($"Failed to schedule log cleanup: {ex.Message}");
            }
        }

        private static System.Threading.Timer _cleanupTimer;

        private static void CleanupOldLogs(object state)
        {
            try
            {
                var logDirectory = GetLogDirectory();
                var retentionDays = GetLogRetentionDays();
                var cutoffDate = DateTime.Now.AddDays(-retentionDays);
                
                var logFiles = Directory.GetFiles(logDirectory, "*.log");
                int cleanedCount = 0;
                
                foreach (var file in logFiles)
                {
                    try
                    {
                        var fileInfo = new FileInfo(file);
                        if (fileInfo.LastWriteTime < cutoffDate)
                        {
                            File.Delete(file);
                            cleanedCount++;
                        }
                    }
                    catch
                    {
                        // Skip files that can't be deleted
                    }
                }
                
                if (cleanedCount > 0)
                {
                    var logger = LogManager.GetLogger("System");
                    logger.Info($"Log cleanup completed: {cleanedCount} old files removed");
                }
            }
            catch (Exception ex)
            {
                try
                {
                    var logger = LogManager.GetLogger("System");
                    logger.Error("Log cleanup failed", ex);
                }
                catch { }
            }
        }

        public static void ShutdownLogging()
        {
            try
            {
                var logger = LogManager.GetLogger("System");
                logger.Info("Shutting down logging system");
                
                _cleanupTimer?.Dispose();
                LogManager.Shutdown();
            }
            catch { }
        }
    }
}