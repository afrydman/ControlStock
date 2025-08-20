using System;
using System.Collections.Generic;
using System.IO;
using DTO.BusinessEntities;
using Repository.Repositories.TransferRepository;
using Persistence.LogService;

namespace Services.FtpService
{
    public class TransferFileService : Loggeable
    {
        private readonly ITransferFileRepository _repository;

        public TransferFileService() : base("TransferFileService")
        {
            _repository = new TransferFileRepository();
        }

        public TransferFileService(ITransferFileRepository repository) : base("TransferFileService")
        {
            _repository = repository;
        }

        // Create transfer record
        public Guid CreateTransfer(string fileName, string localPath, TransferFileType fileType, 
            Guid generatedBy, Guid? destinationStore = null)
        {
            try
            {
                var transfer = new TransferFileData
                {
                    ID = Guid.NewGuid(),
                    FileName = fileName,
                    FilePath = GetOrganizedFilePath(fileType, destinationStore, fileName),
                    FileType = fileType,
                    GeneratedBy = generatedBy,
                    DestinationStore = destinationStore,
                    LocalPath = localPath,
                    Status = TransferStatus.Pending
                };

                // Set file size if file exists
                if (File.Exists(localPath))
                {
                    var fileInfo = new FileInfo(localPath);
                    transfer.FileSize = fileInfo.Length;
                }

                bool success = _repository.Insert(transfer);
                if (success)
                {
                    Log($"Created transfer record: {transfer.ID} for file: {fileName}");
                    return transfer.ID;
                }
                else
                {
                    Log($"Failed to create transfer record for file: {fileName}");
                    return Guid.Empty;
                }
            }
            catch (Exception ex)
            {
                Log($"Error creating transfer record: {ex.Message}");
                return Guid.Empty;
            }
        }

        // Update transfer status
        public bool UpdateTransferStatus(Guid transferId, TransferStatus status, string errorMessage = null)
        {
            try
            {
                bool success = _repository.UpdateStatus(transferId, status, errorMessage);
                if (success)
                {
                    Log($"Updated transfer {transferId} status to: {status}");
                }
                return success;
            }
            catch (Exception ex)
            {
                Log($"Error updating transfer status: {ex.Message}");
                return false;
            }
        }

        // Mark transfer as uploaded
        public bool MarkAsUploaded(Guid transferId, string remotePath)
        {
            try
            {
                var transfer = _repository.GetByID(transferId);
                if (transfer == null) return false;

                long fileSize = 0;
                if (File.Exists(transfer.LocalPath))
                {
                    fileSize = new FileInfo(transfer.LocalPath).Length;
                }

                bool success = _repository.UpdateUploadInfo(transferId, remotePath, fileSize);
                if (success)
                {
                    success = _repository.UpdateStatus(transferId, TransferStatus.Uploaded);
                    Log($"Marked transfer {transferId} as uploaded to: {remotePath}");
                }
                return success;
            }
            catch (Exception ex)
            {
                Log($"Error marking transfer as uploaded: {ex.Message}");
                return false;
            }
        }

        // Handle failed transfer
        public bool MarkAsFailed(Guid transferId, string errorMessage)
        {
            try
            {
                _repository.IncrementRetryCount(transferId);
                bool success = _repository.UpdateStatus(transferId, TransferStatus.Failed, errorMessage);
                if (success)
                {
                    Log($"Marked transfer {transferId} as failed: {errorMessage}");
                }
                return success;
            }
            catch (Exception ex)
            {
                Log($"Error marking transfer as failed: {ex.Message}");
                return false;
            }
        }

        // Get organized file path based on type and destination
        private string GetOrganizedFilePath(TransferFileType fileType, Guid? destinationStore, string fileName)
        {
            switch (fileType)
            {
                case TransferFileType.Global:
                    return $"Global/{fileName}";
                
                case TransferFileType.Store:
                    if (destinationStore.HasValue)
                        return $"Stores/{destinationStore.Value}/{fileName}";
                    else
                        return $"Stores/Unknown/{fileName}";
                
                case TransferFileType.Sales:
                    if (destinationStore.HasValue)
                        return $"Sales/{destinationStore.Value}/{fileName}";
                    else
                        return $"Sales/Unknown/{fileName}";
                
                default:
                    return $"Other/{fileName}";
            }
        }

        // Query methods
        public List<TransferFileData> GetPendingTransfers()
        {
            return _repository.GetPendingTransfers();
        }

        public List<TransferFileData> GetFailedTransfers()
        {
            return _repository.GetFailedTransfers();
        }

        public List<TransferFileData> GetRecentTransfers(int days = 7)
        {
            return _repository.GetRecentTransfers(days);
        }

        public List<TransferFileData> GetTransfersForStore(Guid storeId)
        {
            return _repository.GetTransfersForStore(storeId);
        }

        public TransferStatsData GetStatistics(DateTime? fromDate = null, DateTime? toDate = null)
        {
            return _repository.GetTransferStatistics(fromDate, toDate);
        }

        // Retry failed transfers
        public List<TransferFileData> GetRetryableTransfers()
        {
            var failedTransfers = GetFailedTransfers();
            return failedTransfers.FindAll(t => t.CanRetry);
        }

        public bool RetryTransfer(Guid transferId)
        {
            try
            {
                var transfer = _repository.GetByID(transferId);
                if (transfer == null || !transfer.CanRetry)
                {
                    return false;
                }

                // Reset status to pending for retry
                bool success = _repository.UpdateStatus(transferId, TransferStatus.Pending, null);
                if (success)
                {
                    Log($"Reset transfer {transferId} for retry");
                }
                return success;
            }
            catch (Exception ex)
            {
                Log($"Error retrying transfer: {ex.Message}");
                return false;
            }
        }

        // Cleanup old transfers
        public int CleanupOldTransfers(int daysToKeep = 30)
        {
            try
            {
                int cleanedCount = _repository.CleanupExpiredTransfers(daysToKeep);
                Log($"Cleaned up {cleanedCount} old transfers");
                return cleanedCount;
            }
            catch (Exception ex)
            {
                Log($"Error cleaning up transfers: {ex.Message}");
                return 0;
            }
        }

        // File organization helpers
        public string GetLocalStoragePath(TransferFileType fileType, Guid? destinationStore = null)
        {
            string baseDir = Path.Combine(Path.GetTempPath(), "ControlStockFiles");
            
            switch (fileType)
            {
                case TransferFileType.Global:
                    return Path.Combine(baseDir, "Global");
                
                case TransferFileType.Store:
                    if (destinationStore.HasValue)
                        return Path.Combine(baseDir, "Stores", destinationStore.ToString());
                    else
                        return Path.Combine(baseDir, "Stores", "Unknown");
                
                case TransferFileType.Sales:
                    if (destinationStore.HasValue)
                        return Path.Combine(baseDir, "Sales", destinationStore.ToString());
                    else
                        return Path.Combine(baseDir, "Sales", "Unknown");
                
                default:
                    return Path.Combine(baseDir, "Other");
            }
        }

        public void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Log($"Created directory: {path}");
            }
        }
    }
}