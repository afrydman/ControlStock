using System;
using System.Windows.Forms;
using Persistence.LogService;

namespace Central
{
    // Example of how to integrate enhanced logging into your application
    public class LoggingExample
    {
        // Example 1: Application startup logging initialization
        public static void InitializeApplicationLogging()
        {
            try
            {
                // Initialize logging system with current user
                var currentUser = Environment.UserName;
                LogConfiguration.InitializeLogging("Central", currentUser);

                // Set development vs production log levels
                #if DEBUG
                LogManager.SetGlobalMinimumLevel(LogLevel.Debug);
                #else
                LogManager.SetGlobalMinimumLevel(LogLevel.Info);
                #endif

                // Log application start
                var logger = LogManager.GetLogger("Application");
                logger.Info($"Central application started by {currentUser}");
                logger.Info($"Running on {Environment.MachineName} with .NET {Environment.Version}");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize logging: {ex.Message}", "Logging Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Example 2: Service-level logging
        public static void ExampleServiceLogging()
        {
            var logger = LogManager.GetLogger("ExampleService");
            
            try
            {
                logger.Debug("Starting data processing operation");
                
                // Simulate some work
                logger.Info("Processing 150 records");
                
                // Simulate warning
                logger.Warning("Record ID 45 has invalid data, skipping");
                
                logger.Info("Data processing completed successfully");
            }
            catch (Exception ex)
            {
                logger.Error("Data processing failed", ex);
            }
        }

        // Example 3: FTP operation logging with context
        public static void ExampleFtpLogging()
        {
            var ftpLogger = new FtpService.FtpLogger("ExampleFtp");
            
            // Set user context for this operation
            ftpLogger.SetContext("admin", "session_abc123");
            
            // Log FTP export operation
            ftpLogger.LogExportStart(DTO.BusinessEntities.TransferFileType.Global, null, 500);
            ftpLogger.LogExportDataCollection("Products", 200, 150);
            ftpLogger.LogExportDataCollection("Prices", 300, 200);
            ftpLogger.LogExportComplete("global_data_20250119.json", 5000, true);
        }

        // Example 4: Error handling with structured logging
        public static void ExampleErrorHandling()
        {
            var logger = LogManager.GetLogger("ErrorExample");
            
            try
            {
                // Some operation that might fail
                throw new InvalidOperationException("Sample error for logging demo");
            }
            catch (InvalidOperationException ex)
            {
                // Log with full context
                logger.Error("Business operation failed due to invalid state", ex, "BUSINESS_OPERATION");
            }
            catch (Exception ex)
            {
                // Log unexpected errors as critical
                logger.Critical("Unexpected error occurred", ex, "BUSINESS_OPERATION");
            }
        }

        // Example 5: Performance logging
        public static void ExamplePerformanceLogging()
        {
            var logger = LogManager.GetLogger("Performance");
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            
            try
            {
                // Simulate database operation
                System.Threading.Thread.Sleep(250);
                
                stopwatch.Stop();
                logger.Info($"Database query completed in {stopwatch.ElapsedMilliseconds}ms", "DB_QUERY");
                
                // Log performance metrics
                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    logger.Warning($"Slow query detected: {stopwatch.ElapsedMilliseconds}ms", "PERFORMANCE");
                }
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                logger.Error($"Database query failed after {stopwatch.ElapsedMilliseconds}ms", ex, "DB_QUERY");
            }
        }

        // Example 6: Application shutdown logging
        public static void ShutdownApplicationLogging()
        {
            try
            {
                var logger = LogManager.GetLogger("Application");
                logger.Info("Central application shutting down");
                
                // Properly shutdown logging system
                LogConfiguration.ShutdownLogging();
            }
            catch (Exception ex)
            {
                // Last resort error handling
                try
                {
                    System.Diagnostics.EventLog.WriteEntry("ControlStock", 
                        $"Error during logging shutdown: {ex.Message}", 
                        System.Diagnostics.EventLogEntryType.Error);
                }
                catch { }
            }
        }

        // Example 7: Conditional logging (performance-conscious)
        public static void ExampleConditionalLogging()
        {
            var logger = LogManager.GetLogger("ConditionalExample");
            
            // Only do expensive logging operations if debug level is enabled
            var debugLogger = logger as EnhancedLogger;
            
            // For expensive debug operations
            logger.Debug("Starting detailed analysis");
            
            for (int i = 0; i < 1000; i++)
            {
                // Only log every 100th iteration to avoid spam
                if (i % 100 == 0)
                {
                    logger.Debug($"Processing item {i}/1000");
                }
            }
            
            logger.Debug("Detailed analysis completed");
        }
    }
}