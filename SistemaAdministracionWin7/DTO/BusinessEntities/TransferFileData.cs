using System;

namespace DTO.BusinessEntities
{
    public class TransferFileData : GenericObject
    {
        public TransferFileData()
        {
            ID = Guid.NewGuid();
            Status = TransferStatus.Pending;
            CreatedDate = DateTime.Now;
            RetryCount = 0;
            IsActive = true;
        }

        public string FileName { get; set; }
        public string FilePath { get; set; }
        public TransferFileType FileType { get; set; }
        public Guid GeneratedBy { get; set; }
        public Guid? DestinationStore { get; set; } // NULL = for all stores
        public TransferStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UploadedDate { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public long? FileSize { get; set; }
        public string ErrorMessage { get; set; }
        public int RetryCount { get; set; }
        public string LocalPath { get; set; }
        public string RemotePath { get; set; }
        public bool IsActive { get; set; }

        // Helper properties
        public bool IsForAllStores => !DestinationStore.HasValue;
        public bool IsForSpecificStore => DestinationStore.HasValue;
        public bool CanRetry => RetryCount < 3 && Status == TransferStatus.Failed;
        public bool IsCompleted => Status == TransferStatus.Uploaded || Status == TransferStatus.Processed;
        public string StatusDisplay => Status.ToString();
        public string DestinationDisplay => IsForAllStores ? "All Stores" : $"Store: {DestinationStore}";
    }

    public enum TransferStatus
    {
        Pending = 1,
        Uploading = 2,
        Uploaded = 3,
        Failed = 4,
        Processed = 5,
        Expired = 6
    }

    public enum TransferFileType
    {
        Global = 1,
        Store = 2,
        Sales = 3
    }

    // Helper class for transfer statistics
    public class TransferStatsData
    {
        public int TotalFiles { get; set; }
        public int PendingFiles { get; set; }
        public int UploadingFiles { get; set; }
        public int UploadedFiles { get; set; }
        public int FailedFiles { get; set; }
        public int ProcessedFiles { get; set; }
        public DateTime LastTransfer { get; set; }
        public long TotalSize { get; set; }
        
        public double SuccessRate 
        { 
            get 
            { 
                if (TotalFiles == 0) return 0;
                return ((double)(UploadedFiles + ProcessedFiles) / TotalFiles) * 100;
            } 
        }
    }
}