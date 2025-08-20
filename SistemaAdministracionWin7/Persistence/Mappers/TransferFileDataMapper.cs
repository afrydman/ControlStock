using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO.BusinessEntities;

namespace Persistence.Mappers
{
    public static class TransferFileDataMapper
    {
        public static TransferFileData Map(SqlDataReader reader)
        {
            var transferFile = new TransferFileData
            {
                ID = reader.GetGuid("ID"),
                FileName = reader.GetString("FileName"),
                FilePath = reader.GetString("FilePath"),
                FileType = (TransferFileType)Enum.Parse(typeof(TransferFileType), reader.GetString("FileType")),
                GeneratedBy = reader.GetGuid("GeneratedBy"),
                Status = (TransferStatus)Enum.Parse(typeof(TransferStatus), reader.GetString("Status")),
                CreatedDate = reader.GetDateTime("CreatedDate"),
                RetryCount = reader.GetInt32("RetryCount"),
                IsActive = reader.GetBoolean("IsActive")
            };

            // Handle nullable fields
            if (!reader.IsDBNull("DestinationStore"))
                transferFile.DestinationStore = reader.GetGuid("DestinationStore");

            if (!reader.IsDBNull("UploadedDate"))
                transferFile.UploadedDate = reader.GetDateTime("UploadedDate");

            if (!reader.IsDBNull("ProcessedDate"))
                transferFile.ProcessedDate = reader.GetDateTime("ProcessedDate");

            if (!reader.IsDBNull("FileSize"))
                transferFile.FileSize = reader.GetInt64("FileSize");

            if (!reader.IsDBNull("ErrorMessage"))
                transferFile.ErrorMessage = reader.GetString("ErrorMessage");

            if (!reader.IsDBNull("LocalPath"))
                transferFile.LocalPath = reader.GetString("LocalPath");

            if (!reader.IsDBNull("RemotePath"))
                transferFile.RemotePath = reader.GetString("RemotePath");

            return transferFile;
        }

        public static List<SqlParameter> GetInsertParameters(TransferFileData transferFile)
        {
            return new List<SqlParameter>
            {
                new SqlParameter("@ID", transferFile.ID),
                new SqlParameter("@FileName", transferFile.FileName ?? (object)DBNull.Value),
                new SqlParameter("@FilePath", transferFile.FilePath ?? (object)DBNull.Value),
                new SqlParameter("@FileType", transferFile.FileType.ToString()),
                new SqlParameter("@GeneratedBy", transferFile.GeneratedBy),
                new SqlParameter("@DestinationStore", transferFile.DestinationStore ?? (object)DBNull.Value),
                new SqlParameter("@Status", transferFile.Status.ToString()),
                new SqlParameter("@CreatedDate", transferFile.CreatedDate),
                new SqlParameter("@UploadedDate", transferFile.UploadedDate ?? (object)DBNull.Value),
                new SqlParameter("@ProcessedDate", transferFile.ProcessedDate ?? (object)DBNull.Value),
                new SqlParameter("@FileSize", transferFile.FileSize ?? (object)DBNull.Value),
                new SqlParameter("@ErrorMessage", transferFile.ErrorMessage ?? (object)DBNull.Value),
                new SqlParameter("@RetryCount", transferFile.RetryCount),
                new SqlParameter("@LocalPath", transferFile.LocalPath ?? (object)DBNull.Value),
                new SqlParameter("@RemotePath", transferFile.RemotePath ?? (object)DBNull.Value),
                new SqlParameter("@IsActive", transferFile.IsActive)
            };
        }

        public static List<SqlParameter> GetUpdateParameters(TransferFileData transferFile)
        {
            var parameters = GetInsertParameters(transferFile);
            // ID is used in WHERE clause for updates, not in SET clause
            return parameters;
        }

        public static List<SqlParameter> GetStatusUpdateParameters(Guid id, TransferStatus status, string errorMessage = null)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ID", id),
                new SqlParameter("@Status", status.ToString()),
                new SqlParameter("@ErrorMessage", errorMessage ?? (object)DBNull.Value)
            };

            // Add timestamp based on status
            switch (status)
            {
                case TransferStatus.Uploaded:
                    parameters.Add(new SqlParameter("@UploadedDate", DateTime.Now));
                    break;
                case TransferStatus.Processed:
                    parameters.Add(new SqlParameter("@ProcessedDate", DateTime.Now));
                    break;
            }

            return parameters;
        }
    }
}