using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using DTO.BusinessEntities;
using Persistence.LogService;

namespace Services.FtpService
{
    public static class FtpOperations
    {
        public const string UPLOAD = "FTP_UPLOAD";
        public const string DOWNLOAD = "FTP_DOWNLOAD";
        public const string LIST_FILES = "FTP_LIST";
        public const string CREATE_DIR = "FTP_MKDIR";
        public const string DELETE_FILE = "FTP_DELETE";
        public const string TRANSFER_START = "TRANSFER_START";
        public const string TRANSFER_COMPLETE = "TRANSFER_COMPLETE";
        public const string TRANSFER_FAILED = "TRANSFER_FAILED";
        public const string EXPORT_START = "EXPORT_START";
        public const string EXPORT_COMPLETE = "EXPORT_COMPLETE";
        public const string IMPORT_START = "IMPORT_START";
        public const string IMPORT_COMPLETE = "IMPORT_COMPLETE";
    }

    public class FtpLogger
    {
        private readonly IEnhancedLogger _logger;
        private readonly Dictionary<string, Stopwatch> _operationTimers;
        private readonly object _lockObject = new object();

        public FtpLogger(string category = "FTP")
        {
            _logger = LogManager.GetLogger(category);
            _operationTimers = new Dictionary<string, Stopwatch>();
        }

        public void SetContext(string user, string sessionId)
        {
            _logger.SetContext(user, sessionId);
        }

        #region FTP Client Operations
        
        public string StartOperation(string operation, string details = null)
        {
            var operationId = Guid.NewGuid().ToString("N")[..8];
            
            lock (_lockObject)
            {
                var stopwatch = Stopwatch.StartNew();
                _operationTimers[operationId] = stopwatch;
            }
            
            _logger.Info($"Started {operation} (ID: {operationId})" + 
                        (string.IsNullOrEmpty(details) ? "" : $" - {details}"), 
                        operation);
            
            return operationId;
        }

        public void CompleteOperation(string operationId, string operation, string details = null, bool success = true)
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
            var message = $"Completed {operation} (ID: {operationId}) in {duration}ms" +
                         (string.IsNullOrEmpty(details) ? "" : $" - {details}");
            
            if (success)
                _logger.Info(message, operation);
            else
                _logger.Error(message, null, operation);
        }

        public void LogUploadStart(string localPath, string remotePath, long fileSize)
        {
            _logger.Info($"Starting upload: {Path.GetFileName(localPath)} " +
                        $"({FormatFileSize(fileSize)}) -> {remotePath}", 
                        FtpOperations.UPLOAD);
        }

        public void LogUploadComplete(string fileName, long fileSize, long durationMs, bool success = true)
        {
            var throughput = durationMs > 0 ? (fileSize / 1024.0) / (durationMs / 1000.0) : 0;
            var message = $"Upload {(success ? "completed" : "failed")}: {fileName} " +
                         $"({FormatFileSize(fileSize)}) in {durationMs}ms " +
                         $"(~{throughput:F1} KB/s)";
            
            if (success)
                _logger.Info(message, FtpOperations.UPLOAD);
            else
                _logger.Error(message, null, FtpOperations.UPLOAD);
        }

        public void LogDownloadStart(string remotePath, string localPath)
        {
            _logger.Info($"Starting download: {remotePath} -> {Path.GetFileName(localPath)}", 
                        FtpOperations.DOWNLOAD);
        }

        public void LogDownloadComplete(string fileName, long fileSize, long durationMs, bool success = true)
        {
            var throughput = durationMs > 0 ? (fileSize / 1024.0) / (durationMs / 1000.0) : 0;
            var message = $"Download {(success ? "completed" : "failed")}: {fileName} " +
                         $"({FormatFileSize(fileSize)}) in {durationMs}ms " +
                         $"(~{throughput:F1} KB/s)";
            
            if (success)
                _logger.Info(message, FtpOperations.DOWNLOAD);
            else
                _logger.Error(message, null, FtpOperations.DOWNLOAD);
        }

        public void LogDirectoryOperation(string remotePath, string operation, bool success = true, string error = null)
        {
            var message = $"Directory {operation}: {remotePath}";
            
            if (success)
                _logger.Info(message, FtpOperations.CREATE_DIR);
            else
                _logger.Error($"{message} failed: {error}", null, FtpOperations.CREATE_DIR);
        }

        public void LogFileList(string remotePath, int fileCount, long durationMs)
        {
            _logger.Debug($"Listed {fileCount} files in {remotePath} ({durationMs}ms)", 
                         FtpOperations.LIST_FILES);
        }

        #endregion

        #region Transfer Operations

        public void LogTransferStart(TransferFileData transfer)
        {
            _logger.Info($"Transfer started: {transfer.FileName} " +
                        $"(Type: {transfer.FileType}, Destination: {transfer.DestinationDisplay})", 
                        FtpOperations.TRANSFER_START);
        }

        public void LogTransferStatusChange(Guid transferId, TransferStatus fromStatus, TransferStatus toStatus, string reason = null)
        {
            var message = $"Transfer {transferId:N}[..8] status: {fromStatus} -> {toStatus}";
            if (!string.IsNullOrEmpty(reason))
                message += $" ({reason})";
            
            if (toStatus == TransferStatus.Failed)
                _logger.Warning(message, "TRANSFER_STATUS");
            else
                _logger.Info(message, "TRANSFER_STATUS");
        }

        public void LogTransferComplete(TransferFileData transfer, long durationMs)
        {
            var message = $"Transfer completed: {transfer.FileName} " +
                         $"({FormatFileSize(transfer.FileSize ?? 0)}) in {durationMs}ms";
            
            _logger.Info(message, FtpOperations.TRANSFER_COMPLETE);
        }

        public void LogTransferFailed(TransferFileData transfer, string error, int retryCount)
        {
            var message = $"Transfer failed: {transfer.FileName} " +
                         $"(Retry: {retryCount}/3) - {error}";
            
            _logger.Error(message, null, FtpOperations.TRANSFER_FAILED);
        }

        public void LogTransferRetry(Guid transferId, string fileName, int retryCount)
        {
            _logger.Warning($"Retrying transfer: {fileName} (Attempt: {retryCount + 1}/3)", 
                           "TRANSFER_RETRY");
        }

        #endregion

        #region Export Operations

        public void LogExportStart(TransferFileType fileType, Guid? storeId, int recordCount)
        {
            var destination = storeId.HasValue ? $"Store {storeId}" : "All Stores";
            _logger.Info($"Starting export: {fileType} to {destination} ({recordCount} records)", 
                        FtpOperations.EXPORT_START);
        }

        public void LogExportDataCollection(string dataType, int count, long durationMs)
        {
            _logger.Debug($"Collected {dataType}: {count} records in {durationMs}ms", 
                         FtpOperations.EXPORT_START);
        }

        public void LogExportSerialization(string fileName, long fileSize, long durationMs)
        {
            _logger.Debug($"Serialized {fileName}: {FormatFileSize(fileSize)} in {durationMs}ms", 
                         FtpOperations.EXPORT_START);
        }

        public void LogExportComplete(string fileName, long totalDurationMs, bool success = true)
        {
            var message = $"Export {(success ? "completed" : "failed")}: {fileName} in {totalDurationMs}ms";
            
            if (success)
                _logger.Info(message, FtpOperations.EXPORT_COMPLETE);
            else
                _logger.Error(message, null, FtpOperations.EXPORT_COMPLETE);
        }

        #endregion

        #region Import Operations

        public void LogImportStart(string fileName, Guid storeId)
        {
            _logger.Info($"Starting import: {fileName} for Store {storeId}", 
                        FtpOperations.IMPORT_START);
        }

        public void LogImportDataProcessing(string dataType, int count, int successCount, long durationMs)
        {
            var message = $"Processed {dataType}: {successCount}/{count} in {durationMs}ms";
            
            if (successCount == count)
                _logger.Info(message, FtpOperations.IMPORT_START);
            else
                _logger.Warning(message, FtpOperations.IMPORT_START);
        }

        public void LogImportComplete(string fileName, int totalRecords, int successCount, long totalDurationMs)
        {
            var success = successCount == totalRecords;
            var message = $"Import {(success ? "completed" : "completed with errors")}: {fileName} " +
                         $"({successCount}/{totalRecords} records) in {totalDurationMs}ms";
            
            if (success)
                _logger.Info(message, FtpOperations.IMPORT_COMPLETE);
            else
                _logger.Warning(message, FtpOperations.IMPORT_COMPLETE);
        }

        #endregion

        #region Error and Exception Logging

        public void LogError(string operation, string message, Exception exception = null)
        {
            _logger.Error($"{operation}: {message}", exception, operation);
        }

        public void LogWarning(string operation, string message)
        {
            _logger.Warning($"{operation}: {message}", operation);
        }

        public void LogConnectionError(string server, string user, Exception exception)
        {
            _logger.Error($"FTP connection failed to {server} with user {user}", 
                         exception, "FTP_CONNECTION");
        }

        public void LogAuthenticationError(string server, string user)
        {
            _logger.Error($"FTP authentication failed for {user}@{server}", 
                         null, "FTP_AUTH");
        }

        public void LogConfigurationError(string setting, string message)
        {
            _logger.Error($"Configuration error for {setting}: {message}", 
                         null, "FTP_CONFIG");
        }

        #endregion

        #region Statistics and Performance

        public void LogStatistics(TransferStatsData stats)
        {
            _logger.Info($"Transfer Statistics - Total: {stats.TotalFiles}, " +
                        $"Success: {stats.UploadedFiles + stats.ProcessedFiles}, " +
                        $"Failed: {stats.FailedFiles}, " +
                        $"Success Rate: {stats.SuccessRate:F1}%", 
                        "TRANSFER_STATS");
        }

        public void LogPerformanceMetrics(string operation, Dictionary<string, object> metrics)
        {
            var metricsParts = new List<string>();
            foreach (var metric in metrics)
            {
                metricsParts.Add($"{metric.Key}: {metric.Value}");
            }
            
            _logger.Debug($"{operation} metrics: {string.Join(", ", metricsParts)}", 
                         "PERFORMANCE");
        }

        public void LogCleanupOperation(int filesProcessed, int filesDeleted, long durationMs)
        {
            _logger.Info($"Cleanup completed: {filesDeleted}/{filesProcessed} files removed in {durationMs}ms", 
                        "CLEANUP");
        }

        #endregion

        #region Utility Methods

        private string FormatFileSize(long bytes)
        {
            if (bytes < 1024) return $"{bytes} B";
            if (bytes < 1024 * 1024) return $"{bytes / 1024.0:F1} KB";
            if (bytes < 1024 * 1024 * 1024) return $"{bytes / (1024.0 * 1024):F1} MB";
            return $"{bytes / (1024.0 * 1024 * 1024):F1} GB";
        }

        public void Dispose()
        {
            _logger?.Dispose();
        }

        #endregion
    }
}