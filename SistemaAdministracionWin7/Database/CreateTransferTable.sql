-- Create table for tracking FTP file transfers
CREATE TABLE [dbo].[TransferFiles](
    [ID] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [FileName] [nvarchar](255) NOT NULL,
    [FilePath] [nvarchar](500) NOT NULL,
    [FileType] [nvarchar](50) NOT NULL, -- 'Global', 'Store', 'Sales'
    [GeneratedBy] [uniqueidentifier] NOT NULL, -- User or System ID who generated
    [DestinationStore] [uniqueidentifier] NULL, -- NULL = for all stores, GUID = specific store
    [Status] [nvarchar](20) NOT NULL DEFAULT 'Pending', -- 'Pending', 'Uploading', 'Uploaded', 'Failed', 'Processed'
    [CreatedDate] [datetime] NOT NULL DEFAULT GETDATE(),
    [UploadedDate] [datetime] NULL,
    [ProcessedDate] [datetime] NULL,
    [FileSize] [bigint] NULL, -- File size in bytes
    [ErrorMessage] [nvarchar](1000) NULL,
    [RetryCount] [int] NOT NULL DEFAULT 0,
    [LocalPath] [nvarchar](500) NULL, -- Local file path before upload
    [RemotePath] [nvarchar](500) NULL, -- Remote FTP path after upload
    [IsActive] [bit] NOT NULL DEFAULT 1
);

-- Create indexes for better performance
CREATE INDEX [IX_TransferFiles_Status] ON [dbo].[TransferFiles] ([Status]);
CREATE INDEX [IX_TransferFiles_DestinationStore] ON [dbo].[TransferFiles] ([DestinationStore]);
CREATE INDEX [IX_TransferFiles_CreatedDate] ON [dbo].[TransferFiles] ([CreatedDate]);
CREATE INDEX [IX_TransferFiles_FileType] ON [dbo].[TransferFiles] ([FileType]);

-- Add some sample status values documentation
/*
Status Values:
- 'Pending': File created locally, ready to upload
- 'Uploading': Currently being uploaded to FTP
- 'Uploaded': Successfully uploaded to FTP
- 'Failed': Upload failed
- 'Processed': File has been downloaded and processed by destination
- 'Expired': File is old and can be deleted

FileType Values:
- 'Global': Products, prices, suppliers for all stores
- 'Store': Store-specific documents, adjustments
- 'Sales': Sales data from store to central (future use)
*/