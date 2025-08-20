using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO.BusinessEntities;
using Persistence;
using Persistence.Mappers;

namespace Repository.Repositories.TransferRepository
{
    public class TransferFileRepository : ITransferFileRepository
    {
        public bool Insert(TransferFileData entity)
        {
            string sql = @"
                INSERT INTO TransferFiles 
                (ID, FileName, FilePath, FileType, GeneratedBy, DestinationStore, Status, 
                 CreatedDate, UploadedDate, ProcessedDate, FileSize, ErrorMessage, 
                 RetryCount, LocalPath, RemotePath, IsActive)
                VALUES 
                (@ID, @FileName, @FilePath, @FileType, @GeneratedBy, @DestinationStore, @Status,
                 @CreatedDate, @UploadedDate, @ProcessedDate, @FileSize, @ErrorMessage,
                 @RetryCount, @LocalPath, @RemotePath, @IsActive)";

            var parameters = TransferFileDataMapper.GetInsertParameters(entity);
            return Conexion.ExecuteNonQuery(sql, parameters, false);
        }

        public bool Update(TransferFileData entity)
        {
            string sql = @"
                UPDATE TransferFiles SET
                    FileName = @FileName,
                    FilePath = @FilePath,
                    FileType = @FileType,
                    GeneratedBy = @GeneratedBy,
                    DestinationStore = @DestinationStore,
                    Status = @Status,
                    UploadedDate = @UploadedDate,
                    ProcessedDate = @ProcessedDate,
                    FileSize = @FileSize,
                    ErrorMessage = @ErrorMessage,
                    RetryCount = @RetryCount,
                    LocalPath = @LocalPath,
                    RemotePath = @RemotePath,
                    IsActive = @IsActive
                WHERE ID = @ID";

            var parameters = TransferFileDataMapper.GetUpdateParameters(entity);
            return Conexion.ExecuteNonQuery(sql, parameters, false);
        }

        public bool Delete(Guid id)
        {
            string sql = "UPDATE TransferFiles SET IsActive = 0 WHERE ID = @ID";
            var parameters = new List<SqlParameter> { new SqlParameter("@ID", id) };
            return Conexion.ExecuteNonQuery(sql, parameters, false);
        }

        public TransferFileData GetByID(Guid id)
        {
            string sql = "SELECT * FROM TransferFiles WHERE ID = @ID AND IsActive = 1";
            var parameters = new List<SqlParameter> { new SqlParameter("@ID", id) };
            
            using (var reader = Conexion.ExcuteText(sql, parameters))
            {
                if (reader != null && reader.Read())
                {
                    return TransferFileDataMapper.Map(reader);
                }
            }
            return null;
        }

        public List<TransferFileData> GetAll()
        {
            string sql = "SELECT * FROM TransferFiles WHERE IsActive = 1 ORDER BY CreatedDate DESC";
            return ExecuteQuery(sql, null);
        }

        public bool UpdateStatus(Guid transferId, TransferStatus status, string errorMessage = null)
        {
            string sql = @"
                UPDATE TransferFiles SET
                    Status = @Status,
                    ErrorMessage = @ErrorMessage" +
                    (status == TransferStatus.Uploaded ? ", UploadedDate = @UploadedDate" : "") +
                    (status == TransferStatus.Processed ? ", ProcessedDate = @ProcessedDate" : "") +
                    " WHERE ID = @ID";

            var parameters = TransferFileDataMapper.GetStatusUpdateParameters(transferId, status, errorMessage);
            return Conexion.ExecuteNonQuery(sql, parameters, false);
        }

        public bool UpdateUploadInfo(Guid transferId, string remotePath, long fileSize)
        {
            string sql = @"
                UPDATE TransferFiles SET
                    RemotePath = @RemotePath,
                    FileSize = @FileSize,
                    UploadedDate = @UploadedDate
                WHERE ID = @ID";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ID", transferId),
                new SqlParameter("@RemotePath", remotePath),
                new SqlParameter("@FileSize", fileSize),
                new SqlParameter("@UploadedDate", DateTime.Now)
            };

            return Conexion.ExecuteNonQuery(sql, parameters, false);
        }

        public bool IncrementRetryCount(Guid transferId)
        {
            string sql = "UPDATE TransferFiles SET RetryCount = RetryCount + 1 WHERE ID = @ID";
            var parameters = new List<SqlParameter> { new SqlParameter("@ID", transferId) };
            return Conexion.ExecuteNonQuery(sql, parameters, false);
        }

        public List<TransferFileData> GetByStatus(TransferStatus status)
        {
            string sql = "SELECT * FROM TransferFiles WHERE Status = @Status AND IsActive = 1 ORDER BY CreatedDate DESC";
            var parameters = new List<SqlParameter> { new SqlParameter("@Status", status.ToString()) };
            return ExecuteQuery(sql, parameters);
        }

        public List<TransferFileData> GetPendingTransfers()
        {
            return GetByStatus(TransferStatus.Pending);
        }

        public List<TransferFileData> GetFailedTransfers()
        {
            return GetByStatus(TransferStatus.Failed);
        }

        public List<TransferFileData> GetRecentTransfers(int days = 7)
        {
            string sql = @"
                SELECT * FROM TransferFiles 
                WHERE CreatedDate >= @FromDate AND IsActive = 1 
                ORDER BY CreatedDate DESC";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@FromDate", DateTime.Now.AddDays(-days))
            };

            return ExecuteQuery(sql, parameters);
        }

        public List<TransferFileData> GetGlobalTransfers()
        {
            string sql = "SELECT * FROM TransferFiles WHERE DestinationStore IS NULL AND IsActive = 1 ORDER BY CreatedDate DESC";
            return ExecuteQuery(sql, null);
        }

        public List<TransferFileData> GetTransfersForStore(Guid storeId)
        {
            string sql = "SELECT * FROM TransferFiles WHERE DestinationStore = @StoreId AND IsActive = 1 ORDER BY CreatedDate DESC";
            var parameters = new List<SqlParameter> { new SqlParameter("@StoreId", storeId) };
            return ExecuteQuery(sql, parameters);
        }

        public List<TransferFileData> GetTransfersByFileType(TransferFileType fileType)
        {
            string sql = "SELECT * FROM TransferFiles WHERE FileType = @FileType AND IsActive = 1 ORDER BY CreatedDate DESC";
            var parameters = new List<SqlParameter> { new SqlParameter("@FileType", fileType.ToString()) };
            return ExecuteQuery(sql, parameters);
        }

        public TransferStatsData GetTransferStatistics(DateTime? fromDate = null, DateTime? toDate = null)
        {
            string sql = @"
                SELECT 
                    COUNT(*) as TotalFiles,
                    SUM(CASE WHEN Status = 'Pending' THEN 1 ELSE 0 END) as PendingFiles,
                    SUM(CASE WHEN Status = 'Uploading' THEN 1 ELSE 0 END) as UploadingFiles,
                    SUM(CASE WHEN Status = 'Uploaded' THEN 1 ELSE 0 END) as UploadedFiles,
                    SUM(CASE WHEN Status = 'Failed' THEN 1 ELSE 0 END) as FailedFiles,
                    SUM(CASE WHEN Status = 'Processed' THEN 1 ELSE 0 END) as ProcessedFiles,
                    COALESCE(MAX(CreatedDate), '1900-01-01') as LastTransfer,
                    COALESCE(SUM(FileSize), 0) as TotalSize
                FROM TransferFiles 
                WHERE IsActive = 1" +
                (fromDate.HasValue ? " AND CreatedDate >= @FromDate" : "") +
                (toDate.HasValue ? " AND CreatedDate <= @ToDate" : "");

            var parameters = new List<SqlParameter>();
            if (fromDate.HasValue) parameters.Add(new SqlParameter("@FromDate", fromDate.Value));
            if (toDate.HasValue) parameters.Add(new SqlParameter("@ToDate", toDate.Value));

            using (var reader = Conexion.ExcuteText(sql, parameters))
            {
                if (reader != null && reader.Read())
                {
                    return new TransferStatsData
                    {
                        TotalFiles = reader.GetInt32("TotalFiles"),
                        PendingFiles = reader.GetInt32("PendingFiles"),
                        UploadingFiles = reader.GetInt32("UploadingFiles"),
                        UploadedFiles = reader.GetInt32("UploadedFiles"),
                        FailedFiles = reader.GetInt32("FailedFiles"),
                        ProcessedFiles = reader.GetInt32("ProcessedFiles"),
                        LastTransfer = reader.GetDateTime("LastTransfer"),
                        TotalSize = reader.GetInt64("TotalSize")
                    };
                }
            }

            return new TransferStatsData();
        }

        public int CleanupExpiredTransfers(int daysToKeep = 30)
        {
            string sql = @"
                UPDATE TransferFiles SET IsActive = 0 
                WHERE CreatedDate < @CutoffDate 
                AND (Status = 'Processed' OR Status = 'Expired')
                AND IsActive = 1";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@CutoffDate", DateTime.Now.AddDays(-daysToKeep))
            };

            // This would need to return affected rows count - modify ExecuteNonQuery if needed
            bool success = Conexion.ExecuteNonQuery(sql, parameters, false);
            return success ? 1 : 0; // Simplified - you might want to modify ExecuteNonQuery to return affected rows
        }

        public List<TransferFileData> GetTransfersForCleanup(int daysToKeep = 30)
        {
            string sql = @"
                SELECT * FROM TransferFiles 
                WHERE CreatedDate < @CutoffDate 
                AND (Status = 'Processed' OR Status = 'Expired')
                AND IsActive = 1
                ORDER BY CreatedDate";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@CutoffDate", DateTime.Now.AddDays(-daysToKeep))
            };

            return ExecuteQuery(sql, parameters);
        }

        public List<TransferFileData> GetTransfersByDateRange(DateTime fromDate, DateTime toDate)
        {
            string sql = @"
                SELECT * FROM TransferFiles 
                WHERE CreatedDate >= @FromDate AND CreatedDate <= @ToDate AND IsActive = 1
                ORDER BY CreatedDate DESC";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate)
            };

            return ExecuteQuery(sql, parameters);
        }

        public List<TransferFileData> GetTransfersByUser(Guid userId)
        {
            string sql = "SELECT * FROM TransferFiles WHERE GeneratedBy = @UserId AND IsActive = 1 ORDER BY CreatedDate DESC";
            var parameters = new List<SqlParameter> { new SqlParameter("@UserId", userId) };
            return ExecuteQuery(sql, parameters);
        }

        public bool HasPendingTransfersForStore(Guid? storeId)
        {
            string sql = @"
                SELECT COUNT(*) FROM TransferFiles 
                WHERE Status = 'Pending' AND IsActive = 1" +
                (storeId.HasValue ? " AND DestinationStore = @StoreId" : " AND DestinationStore IS NULL");

            var parameters = new List<SqlParameter>();
            if (storeId.HasValue) parameters.Add(new SqlParameter("@StoreId", storeId.Value));

            using (var reader = Conexion.ExcuteText(sql, parameters))
            {
                if (reader != null && reader.Read())
                {
                    return reader.GetInt32(0) > 0;
                }
            }

            return false;
        }

        private List<TransferFileData> ExecuteQuery(string sql, List<SqlParameter> parameters)
        {
            var results = new List<TransferFileData>();

            using (var reader = Conexion.ExcuteText(sql, parameters))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        results.Add(TransferFileDataMapper.Map(reader));
                    }
                }
            }

            return results;
        }
    }
}