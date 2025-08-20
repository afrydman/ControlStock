using System;
using System.IO;

namespace Persistence.LogService
{
    /// <summary>
    /// Simple logging base class for backward compatibility
    /// </summary>
    public class Loggeable
    {
        private static Logger _logger;
        private string _category;

        public Loggeable()
        {
            _category = "General";
        }

        public Loggeable(string category)
        {
            _category = category ?? "General";
        }

        public static void create(string category)
        {
            if (_logger == null)
            {
                try
                {
                    var logPath = Path.Combine(Environment.CurrentDirectory, "Logs", (category ?? "General") + ".log");
                    var directory = Path.GetDirectoryName(logPath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    _logger = new Logger(logPath);
                }
                catch
                {
                    // If logger creation fails, create a basic one
                    try
                    {
                        _logger = new Logger("fallback.log");
                    }
                    catch
                    {
                        // Logger creation completely failed, _logger stays null
                    }
                }
            }
        }

        public static void Log(string message)
        {
            try
            {
                if (_logger == null)
                {
                    create("General");
                }
                
                if (_logger != null)
                {
                    _logger.Log(message);
                }
                else
                {
                    // Fallback to direct file logging if logger fails
                    FallbackLog(message);
                }
            }
            catch (Exception)
            {
                // Fallback to file logging if logger fails
                try
                {
                    FallbackLog(message);
                }
                catch
                {
                    // If all logging fails, ignore silently to prevent cascading errors
                }
            }
        }

        private static void FallbackLog(string message)
        {
            var logPath = Path.Combine(Environment.CurrentDirectory, "Logs", "fallback.log");
            var directory = Path.GetDirectoryName(logPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            
            File.AppendAllText(logPath, 
                string.Format("[{0}] {1}{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message, Environment.NewLine));
        }

        public void LogInstance(string message, bool includeTimestamp)
        {
            if (includeTimestamp)
            {
                Log(string.Format("[{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message));
            }
            else
            {
                Log(message);
            }
        }

        public static void Close()
        {
            try
            {
                if (_logger != null)
                {
                    _logger.Close();
                    _logger = null;
                }
            }
            catch
            {
                // Ignore errors during cleanup
            }
        }
    }
}