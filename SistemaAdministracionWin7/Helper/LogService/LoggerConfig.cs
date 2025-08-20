using System;
using System.Configuration;
using System.IO;

namespace Helper.LogService
{
    public static class LoggerConfig
    {
        private static string _logFolder;
        private static readonly object _lockObject = new object();
        
        /// <summary>
        /// Gets the configured log folder from app.config, or a default folder if not configured
        /// </summary>
        public static string LogFolder
        {
            get
            {
                if (_logFolder == null)
                {
                    lock (_lockObject)
                    {
                        if (_logFolder == null)
                        {
                            // Try to get from app.config
                            _logFolder = ConfigurationManager.AppSettings["CarpetaLogs"];
                            
                            // If not configured, use a default folder
                            if (string.IsNullOrWhiteSpace(_logFolder))
                            {
                                // Default to CommonApplicationData\ControlStock\Logs
                                string defaultPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                                _logFolder = Path.Combine(defaultPath, "ControlStock", "Logs");
                            }
                            
                            // Ensure the folder exists
                            try
                            {
                                if (!Directory.Exists(_logFolder))
                                {
                                    Directory.CreateDirectory(_logFolder);
                                }
                            }
                            catch (Exception ex)
                            {
                                // Fall back to temp folder if we can't create the directory
                                _logFolder = Path.GetTempPath();
                                Console.WriteLine($"Could not create log folder, using temp: {ex.Message}");
                            }
                        }
                    }
                }
                return _logFolder;
            }
        }
        
        /// <summary>
        /// Creates a logger with the standard configuration
        /// </summary>
        /// <param name="loggerId">Unique identifier for the logger</param>
        /// <param name="fileName">Log file name (without path)</param>
        /// <param name="minimumLevel">Minimum log level (optional)</param>
        public static void InitializeLogger(string loggerId, string fileName, LogLevel? minimumLevel = null)
        {
            string fullPath = Path.Combine(LogFolder, fileName);
            Log.New(loggerId, fullPath, minimumLevel);
        }
        
        /// <summary>
        /// Creates a logger for a specific module
        /// </summary>
        /// <param name="moduleName">Name of the module (e.g., "Central", "PuntoVenta")</param>
        /// <param name="minimumLevel">Minimum log level (optional)</param>
        public static void InitializeModuleLogger(string moduleName, LogLevel? minimumLevel = null)
        {
            string fileName = $"{moduleName}.log";
            InitializeLogger(moduleName, fileName, minimumLevel);
        }
        
        /// <summary>
        /// Gets the full path for a log file
        /// </summary>
        /// <param name="fileName">Log file name</param>
        /// <returns>Full path to the log file</returns>
        public static string GetLogFilePath(string fileName)
        {
            return Path.Combine(LogFolder, fileName);
        }
        
        /// <summary>
        /// Initializes the default app logger (should be called with module-specific name)
        /// </summary>
        public static void InitializeCommonLoggers()
        {
            // This method is kept for backward compatibility with HelperService
            // but the actual logger initialization should use InitializeModuleLogger
            // which creates module-specific log files (e.g., Central.log, PuntoVenta.log)
        }
        
        /// <summary>
        /// Sets or updates the log folder path
        /// </summary>
        /// <param name="path">New log folder path</param>
        public static void SetLogFolder(string path)
        {
            lock (_lockObject)
            {
                _logFolder = path;
                
                // Ensure the folder exists
                try
                {
                    if (!Directory.Exists(_logFolder))
                    {
                        Directory.CreateDirectory(_logFolder);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not create log folder at {path}: {ex.Message}");
                    throw;
                }
            }
        }
    }
}