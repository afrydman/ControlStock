using System;
using System.Collections.Generic;
using DTO.BusinessEntities;

namespace Repository.Repositories.TransferRepository
{
    public interface ITransferFileRepository : IGenericRepository<TransferFileData>
    {
        // Status management
        bool UpdateStatus(Guid transferId, TransferStatus status, string errorMessage = null);
        bool UpdateUploadInfo(Guid transferId, string remotePath, long fileSize);
        bool IncrementRetryCount(Guid transferId);

        // Queries by status
        List<TransferFileData> GetByStatus(TransferStatus status);
        List<TransferFileData> GetPendingTransfers();
        List<TransferFileData> GetFailedTransfers();
        List<TransferFileData> GetRecentTransfers(int days = 7);

        // Queries by destination
        List<TransferFileData> GetGlobalTransfers();
        List<TransferFileData> GetTransfersForStore(Guid storeId);
        List<TransferFileData> GetTransfersByFileType(TransferFileType fileType);

        // Statistics and cleanup
        TransferStatsData GetTransferStatistics(DateTime? fromDate = null, DateTime? toDate = null);
        int CleanupExpiredTransfers(int daysToKeep = 30);
        List<TransferFileData> GetTransfersForCleanup(int daysToKeep = 30);

        // Advanced queries
        List<TransferFileData> GetTransfersByDateRange(DateTime fromDate, DateTime toDate);
        List<TransferFileData> GetTransfersByUser(Guid userId);
        bool HasPendingTransfersForStore(Guid? storeId);
    }
}