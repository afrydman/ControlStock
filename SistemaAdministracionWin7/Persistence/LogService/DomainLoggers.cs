using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DTO.BusinessEntities;

namespace Persistence.LogService
{
    // Domain-specific logging operations
    public static class LogOperations
    {
        // Database operations
        public const string DB_INSERT = "DB_INSERT";
        public const string DB_UPDATE = "DB_UPDATE";
        public const string DB_DELETE = "DB_DELETE";
        public const string DB_SELECT = "DB_SELECT";
        public const string DB_EXECUTE = "DB_EXECUTE";
        public const string DB_TRANSACTION = "DB_TRANSACTION";
        public const string DB_CONNECTION = "DB_CONNECTION";
        
        // Business operations
        public const string PRODUCT_CREATE = "PRODUCT_CREATE";
        public const string PRODUCT_UPDATE = "PRODUCT_UPDATE";
        public const string SALE_PROCESS = "SALE_PROCESS";
        public const string STOCK_UPDATE = "STOCK_UPDATE";
        public const string PRICE_UPDATE = "PRICE_UPDATE";
        public const string SUPPLIER_OPERATION = "SUPPLIER_OPERATION";
        
        // UI operations
        public const string FORM_LOAD = "FORM_LOAD";
        public const string USER_ACTION = "USER_ACTION";
        public const string VALIDATION = "VALIDATION";
        public const string REPORT_GENERATE = "REPORT_GENERATE";
        
        // System operations
        public const string APP_START = "APP_START";
        public const string APP_SHUTDOWN = "APP_SHUTDOWN";
        public const string CONFIG_LOAD = "CONFIG_LOAD";
        public const string SECURITY = "SECURITY";
    }

    // Database operations logger
    public class DatabaseLogger
    {
        private readonly IEnhancedLogger _logger;
        private readonly Dictionary<string, Stopwatch> _operationTimers;
        private readonly object _lockObject = new object();

        public DatabaseLogger(string category = "Database")
        {
            _logger = LogManager.GetLogger(category);
            _operationTimers = new Dictionary<string, Stopwatch>();
        }

        public void SetContext(string user, string sessionId)
        {
            _logger.SetContext(user, sessionId);
        }

        public string StartOperation(string operation, string tableName, object parameters = null)
        {
            var operationId = Guid.NewGuid().ToString("N").Substring(0, 8);
            
            lock (_lockObject)
            {
                var stopwatch = Stopwatch.StartNew();
                _operationTimers[operationId] = stopwatch;
            }

            var details = string.Format("Table: {0}", tableName);
            if (parameters != null)
            {
                details += string.Format(" | Params: {0}", parameters);
            }
            
            _logger.Debug(string.Format("Started {0} (ID: {1}) - {2}", operation, operationId, details), operation);
            return operationId;
        }

        public void CompleteOperation(string operationId, string operation, bool success, int? recordsAffected = null, string error = null)
        {
            Stopwatch stopwatch = null;
            
            lock (_lockObject)
            {
                if (_operationTimers.TryGetValue(operationId, out stopwatch))
                {
                    stopwatch.Stop();
                    _operationTimers.Remove(operationId);
                }
            }
            
            var duration = stopwatch?.ElapsedMilliseconds ?? 0;
            var message = string.Format("Completed {0} (ID: {1}) in {2}ms", operation, operationId, duration);
            
            if (recordsAffected.HasValue)
                message += string.Format(" | Records affected: {0}", recordsAffected.Value);
            
            if (!string.IsNullOrEmpty(error))
                message += string.Format(" | Error: {0}", error);

            if (success)
            {
                _logger.Info(message, operation);
                
                // Warn about slow queries
                if (duration > 5000)
                {
                    _logger.Warning(string.Format("Slow database operation: {0} took {1}ms", operation, duration), "DB_PERFORMANCE");
                }
            }
            else
            {
                _logger.Error(message, null, operation);
            }
        }

        public void LogConnectionEvent(string server, string database, bool success, Exception exception = null)
        {
            var message = string.Format("Database connection to {0}/{1}", server, database);
            
            if (success)
                _logger.Info(string.Format("{0} established", message), LogOperations.DB_CONNECTION);
            else
                _logger.Error(string.Format("{0} failed", message), exception, LogOperations.DB_CONNECTION);
        }

        public void LogTransaction(string transactionType, bool success, long durationMs, Exception exception = null)
        {
            var message = string.Format("Transaction {0} {1} in {2}ms", transactionType, (success ? "committed" : "rolled back"), durationMs);
            
            if (success)
                _logger.Info(message, LogOperations.DB_TRANSACTION);
            else
                _logger.Error(message, exception, LogOperations.DB_TRANSACTION);
        }

        // Direct logging methods for compatibility
        public void LogError(string message, Exception exception = null, string operation = null)
        {
            _logger.Error(message, exception, operation ?? LogOperations.DB_EXECUTE);
        }

        public void LogWarning(string message, string operation = null)
        {
            _logger.Warning(message, operation ?? LogOperations.DB_EXECUTE);
        }

        public void LogInfo(string message, string operation = null)
        {
            _logger.Info(message, operation ?? LogOperations.DB_EXECUTE);
        }

        public void LogDebug(string message, string operation = null)
        {
            _logger.Debug(message, operation ?? LogOperations.DB_EXECUTE);
        }
    }

    // Business operations logger
    public class BusinessLogger
    {
        private readonly IEnhancedLogger _logger;

        public BusinessLogger(string category = "Business")
        {
            _logger = LogManager.GetLogger(category);
        }

        public void SetContext(string user, string sessionId)
        {
            _logger.SetContext(user, sessionId);
        }

        public void LogProductOperation(string operation, string productCode, string description, bool success, string details = null)
        {
            var message = string.Format("Product {0}: {1} - {2}", operation, productCode, description);
            if (!string.IsNullOrEmpty(details))
                message += string.Format(" | {0}", details);

            if (success)
                _logger.Info(message, LogOperations.PRODUCT_CREATE);
            else
                _logger.Warning(message, LogOperations.PRODUCT_CREATE);
        }

        public void LogSaleTransaction(decimal amount, int itemCount, string paymentMethod, string customerInfo = null)
        {
            var message = string.Format("Sale completed: ${0:F2} | {1} items | Payment: {2}", amount, itemCount, paymentMethod);
            if (!string.IsNullOrEmpty(customerInfo))
                message += string.Format(" | Customer: {0}", customerInfo);

            _logger.Info(message, LogOperations.SALE_PROCESS);
        }

        public void LogStockMovement(string productCode, decimal oldStock, decimal newStock, string reason)
        {
            var change = newStock - oldStock;
            var changeType = change > 0 ? "increase" : "decrease";
            
            _logger.Info(string.Format("Stock {0}: {1} | {2} -> {3} ({4:+#;-#;0}) | Reason: {5}", changeType, productCode, oldStock, newStock, change, reason), 
                        LogOperations.STOCK_UPDATE);
        }

        public void LogPriceChange(string productCode, decimal oldPrice, decimal newPrice, string reason)
        {
            var changePercent = oldPrice > 0 ? ((newPrice - oldPrice) / oldPrice) * 100 : 0;
            
            _logger.Info(string.Format("Price update: {0} | ${1:F2} -> ${2:F2} ({3:+#.#;-#.#;0}%) | Reason: {4}", productCode, oldPrice, newPrice, changePercent, reason), 
                        LogOperations.PRICE_UPDATE);
        }

        public void LogBusinessRule(string rule, string entity, bool passed, string details = null)
        {
            var message = string.Format("Business rule '{0}' for {1}: {2}", rule, entity, (passed ? "PASSED" : "FAILED"));
            if (!string.IsNullOrEmpty(details))
                message += string.Format(" | {0}", details);

            if (passed)
                _logger.Debug(message, LogOperations.VALIDATION);
            else
                _logger.Warning(message, LogOperations.VALIDATION);
        }

        // Direct logging methods for compatibility
        public void LogError(string message, Exception exception = null, string operation = null)
        {
            _logger.Error(message, exception, operation ?? "BUSINESS");
        }

        public void LogWarning(string message, string operation = null)
        {
            _logger.Warning(message, operation ?? "BUSINESS");
        }

        public void LogInfo(string message, string operation = null)
        {
            _logger.Info(message, operation ?? "BUSINESS");
        }

        public void LogDebug(string message, string operation = null)
        {
            _logger.Debug(message, operation ?? "BUSINESS");
        }
    }

    // UI operations logger
    public class UILogger
    {
        private readonly IEnhancedLogger _logger;

        public UILogger(string category = "UI")
        {
            _logger = LogManager.GetLogger(category);
        }

        public void SetContext(string user, string sessionId)
        {
            _logger.SetContext(user, sessionId);
        }

        // Direct logging methods for compatibility
        public void LogError(string message, Exception exception = null, string operation = null)
        {
            _logger.Error(message, exception, operation ?? "BUSINESS");
        }

        public void LogWarning(string message, string operation = null)
        {
            _logger.Warning(message, operation ?? "BUSINESS");
        }

        public void LogInfo(string message, string operation = null)
        {
            _logger.Info(message, operation ?? "BUSINESS");
        }

        public void LogDebug(string message, string operation = null)
        {
            _logger.Debug(message, operation ?? "BUSINESS");
        }

        public void LogFormLoad(string formName, long loadTimeMs, bool success = true)
        {
            var message = string.Format("Form loaded: {0} in {1}ms", formName, loadTimeMs);
            
            if (success)
            {
                _logger.Info(message, LogOperations.FORM_LOAD);
                
                // Warn about slow form loads
                if (loadTimeMs > 3000)
                {
                    _logger.Warning(string.Format("Slow form load: {0} took {1}ms", formName, loadTimeMs), "UI_PERFORMANCE");
                }
            }
            else
            {
                _logger.Error(string.Format("Failed to load form: {0}", formName), null, LogOperations.FORM_LOAD);
            }
        }

        public void LogUserAction(string action, string formName, string controlName = null, string value = null)
        {
            var message = string.Format("User action: {0} on {1}", action, formName);
            if (!string.IsNullOrEmpty(controlName))
                message += string.Format(".{0}", controlName);
            if (!string.IsNullOrEmpty(value))
                message += string.Format(" | Value: {0}", value);

            _logger.Debug(message, LogOperations.USER_ACTION);
        }

        public void LogValidationError(string fieldName, string errorMessage, string formName)
        {
            _logger.Warning(string.Format("Validation error in {0}.{1}: {2}", formName, fieldName, errorMessage), LogOperations.VALIDATION);
        }

        public void LogReportGeneration(string reportName, int recordCount, long generationTimeMs, bool success)
        {
            var message = string.Format("Report '{0}': {1} records in {2}ms", reportName, recordCount, generationTimeMs);
            
            if (success)
                _logger.Info(message, LogOperations.REPORT_GENERATE);
            else
                _logger.Error(string.Format("Failed to generate report: {0}", reportName), null, LogOperations.REPORT_GENERATE);
        }
    }

    // System operations logger
    public class SystemLogger
    {
        private readonly IEnhancedLogger _logger;

        public SystemLogger(string category = "System")
        {
            _logger = LogManager.GetLogger(category);
        }

        public void SetContext(string user, string sessionId)
        {
            _logger.SetContext(user, sessionId);
        }

        public void LogApplicationStart(string appName, string version, string user)
        {
            _logger.Info(string.Format("{0} v{1} started by {2} on {3}", appName, version, user, Environment.MachineName), LogOperations.APP_START);
            
            // Log system information
            _logger.Debug(string.Format("System info: OS={0}, .NET={1}, Memory={2}MB", Environment.OSVersion, Environment.Version, GC.GetTotalMemory(false) / 1024 / 1024), LogOperations.APP_START);
        }

        public void LogApplicationShutdown(string appName, long uptimeMs)
        {
            var uptime = TimeSpan.FromMilliseconds(uptimeMs);
            _logger.Info(string.Format("{0} shutdown after {1:F1} hours uptime", appName, uptime.TotalHours), LogOperations.APP_SHUTDOWN);
        }

        public void LogConfigurationLoad(string configFile, bool success, Exception exception = null)
        {
            var message = string.Format("Configuration loaded from {0}", configFile);
            
            if (success)
                _logger.Info(message, LogOperations.CONFIG_LOAD);
            else
                _logger.Error(string.Format("Failed to load configuration: {0}", configFile), exception, LogOperations.CONFIG_LOAD);
        }

        public void LogSecurityEvent(string eventType, string details, bool isSuccessful = true)
        {
            var message = string.Format("Security event: {0} - {1}", eventType, details);
            
            if (isSuccessful)
                _logger.Info(message, LogOperations.SECURITY);
            else
                _logger.Warning(message, LogOperations.SECURITY);
        }

        public void LogPerformanceMetric(string metricName, double value, string unit = null)
        {
            var message = string.Format("Performance metric: {0} = {1}", metricName, value);
            if (!string.IsNullOrEmpty(unit))
                message += string.Format(" {0}", unit);

            _logger.Debug(message, "PERFORMANCE");
        }

        public void LogMemoryUsage(long workingSetMB, long gcMemoryMB)
        {
            _logger.Debug(string.Format("Memory usage: Working Set = {0}MB, GC Memory = {1}MB", workingSetMB, gcMemoryMB), "MEMORY");
        }
    }

    // Factory for getting appropriate loggers
    public static class LoggerFactory
    {
        private static readonly Dictionary<string, object> _loggers = new Dictionary<string, object>();
        private static readonly object _lock = new object();

        public static DatabaseLogger GetDatabaseLogger(string category = "Database")
        {
            return GetOrCreateLogger(category, () => new DatabaseLogger(category));
        }

        public static BusinessLogger GetBusinessLogger(string category = "Business")
        {
            return GetOrCreateLogger(category, () => new BusinessLogger(category));
        }

        public static UILogger GetUILogger(string category = "UI")
        {
            return GetOrCreateLogger(category, () => new UILogger(category));
        }

        public static SystemLogger GetSystemLogger(string category = "System")
        {
            return GetOrCreateLogger(category, () => new SystemLogger(category));
        }

        private static T GetOrCreateLogger<T>(string key, Func<T> factory) where T : class
        {
            lock (_lock)
            {
                if (_loggers.TryGetValue(key, out object existingLogger))
                {
                    return existingLogger as T;
                }

                var newLogger = factory();
                _loggers[key] = newLogger;
                return newLogger;
            }
        }
    }
}