using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using DTO.BusinessEntities;
using Newtonsoft.Json;
using Persistence.LogService;
using Services.ProductoService;
using Services.PrecioService;
using Services.ProveedorService;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.ProveedorRepository;

namespace Services.FtpService
{
    public class CentralExportService : Loggeable
    {
        private readonly FtpClientService _ftpClient;
        private readonly TransferFileService _transferService;
        private readonly string _tempFolderPath;
        private readonly Guid _currentUserId;
        
        public CentralExportService(Guid? userId = null) : base("CentralExportService")
        {
            string ftpServer = ConfigurationManager.AppSettings["FtpServer"];
            string ftpUser = ConfigurationManager.AppSettings["FtpUser"];
            string ftpPassword = ConfigurationManager.AppSettings["FtpPassword"];
            
            _ftpClient = new FtpClientService(ftpServer, ftpUser, ftpPassword);
            _transferService = new TransferFileService();
            _tempFolderPath = Path.Combine(Path.GetTempPath(), "ControlStockExport");
            _currentUserId = userId ?? Guid.NewGuid(); // Default user if not provided
            
            if (!Directory.Exists(_tempFolderPath))
                Directory.CreateDirectory(_tempFolderPath);
        }

        public bool ExportGlobalData()
        {
            Guid transferId = Guid.Empty;
            string localFilePath = "";
            
            try
            {
                Log("Starting global data export");

                // Prepare organized storage
                string globalStoragePath = _transferService.GetLocalStoragePath(TransferFileType.Global);
                _transferService.EnsureDirectoryExists(globalStoragePath);

                var globalData = new GlobalDataTransfer();
                
                // Get data from services
                var productoService = new ProductoService(new ProductoRepository());
                var precioService = new PrecioService();
                var proveedorService = new ProveedorService(new ProveedorRepository());

                // Export productos
                Log("Exporting products");
                var productos = productoService.GetAll();
                globalData.Productos = productos.Select(p => new ProductoTransferData
                {
                    ID = p.ID,
                    CodigoInterno = p.CodigoInterno,
                    CodigoProveedor = p.CodigoProveedor,
                    Description = p.Description,
                    ProveedorID = p.Proveedor?.ID ?? Guid.Empty,
                    ProveedorNombre = p.Proveedor?.RazonSocial ?? "",
                    LineaID = p.Linea?.ID ?? Guid.Empty,
                    LineaNombre = p.Linea?.Description ?? "",
                    TemporadaID = p.Temporada?.ID ?? Guid.Empty,
                    TemporadaNombre = p.Temporada?.Description ?? "",
                    FechaModificacion = DateTime.Now
                }).ToList();

                // Export precios
                Log("Exporting prices");
                var precios = precioService.GetAllPrecios();
                globalData.Precios = precios.Select(precio => new PrecioTransferData
                {
                    ProductoTalleID = precio.ProductoTalle?.ID ?? Guid.Empty,
                    CodigoProducto = precio.ProductoTalle?.Producto?.CodigoInterno ?? "",
                    Talle = precio.ProductoTalle?.Talle ?? 0,
                    Precio = precio.Precio,
                    ListaPrecioID = precio.ListaPrecio?.ID ?? Guid.Empty,
                    FechaModificacion = DateTime.Now
                }).ToList();

                // Export proveedores
                Log("Exporting suppliers");
                var proveedores = proveedorService.GetAll();
                globalData.Proveedores = proveedores.Select(p => new ProveedorTransferData
                {
                    ID = p.ID,
                    Codigo = p.Codigo,
                    RazonSocial = p.RazonSocial,
                    Email = p.Email ?? "",
                    Telefono = p.Telefono ?? "",
                    FechaModificacion = DateTime.Now
                }).ToList();

                // Create file
                string fileName = $"global_data_{DateTime.Now:yyyyMMdd_HHmmss}.json";
                localFilePath = Path.Combine(globalStoragePath, fileName);
                
                // Create transfer record
                transferId = _transferService.CreateTransfer(fileName, localFilePath, 
                    TransferFileType.Global, _currentUserId, null);
                
                if (transferId == Guid.Empty)
                {
                    Log("Failed to create transfer record");
                    return false;
                }

                // Serialize to JSON
                string json = JsonConvert.SerializeObject(globalData, Formatting.Indented);
                File.WriteAllText(localFilePath, json);
                Log($"Created local file: {localFilePath}");

                // Update status to uploading
                _transferService.UpdateTransferStatus(transferId, TransferStatus.Uploading);

                // Upload to FTP with organized path
                string remoteFilePath = $"Global/{fileName}";
                bool uploadSuccess = _ftpClient.UploadFile(localFilePath, remoteFilePath);

                if (uploadSuccess)
                {
                    // Mark as uploaded
                    _transferService.MarkAsUploaded(transferId, remoteFilePath);
                    Log("Global data export completed successfully");
                    
                    // Clean up old files (keep last 7 days)
                    CleanupOldFiles("Global", "global_data_", 7);
                    
                    return true;
                }
                else
                {
                    // Mark as failed
                    _transferService.MarkAsFailed(transferId, "FTP upload failed");
                    Log("Failed to upload global data file");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log($"Global data export failed: {ex.Message}");
                Log($"Stack trace: {ex.StackTrace}");
                
                // Mark transfer as failed if we created one
                if (transferId != Guid.Empty)
                {
                    _transferService.MarkAsFailed(transferId, ex.Message);
                }
                
                return false;
            }
            finally
            {
                // Cleanup local temp file after a delay to allow for debugging
                try 
                { 
                    if (File.Exists(localFilePath))
                    {
                        // Keep the file for now - cleanup can be done separately
                        Log($"Local file kept for reference: {localFilePath}");
                    }
                } 
                catch { }
            }
        }

        public bool ExportStoreSpecificData(Guid storeId, List<DocumentoTransferData> documentos = null, List<StockAjusteTransferData> stockAjustes = null)
        {
            Guid transferId = Guid.Empty;
            string localFilePath = "";
            
            try
            {
                Log($"Starting store-specific data export for store: {storeId}");

                // Prepare organized storage
                string storeStoragePath = _transferService.GetLocalStoragePath(TransferFileType.Store, storeId);
                _transferService.EnsureDirectoryExists(storeStoragePath);

                var storeData = new StoreSpecificDataTransfer
                {
                    StoreID = storeId,
                    Documentos = documentos ?? new List<DocumentoTransferData>(),
                    StockAjustes = stockAjustes ?? new List<StockAjusteTransferData>()
                };

                // Create file
                string fileName = $"store_data_{DateTime.Now:yyyyMMdd_HHmmss}.json";
                localFilePath = Path.Combine(storeStoragePath, fileName);
                
                // Create transfer record
                transferId = _transferService.CreateTransfer(fileName, localFilePath, 
                    TransferFileType.Store, _currentUserId, storeId);
                
                if (transferId == Guid.Empty)
                {
                    Log("Failed to create transfer record");
                    return false;
                }

                // Serialize to JSON
                string json = JsonConvert.SerializeObject(storeData, Formatting.Indented);
                File.WriteAllText(localFilePath, json);
                Log($"Created local file: {localFilePath}");

                // Update status to uploading
                _transferService.UpdateTransferStatus(transferId, TransferStatus.Uploading);

                // Upload to FTP with organized path
                string remoteFilePath = $"Stores/{storeId}/{fileName}";
                
                // Ensure store directory exists
                _ftpClient.CreateDirectory($"Stores/{storeId}");
                
                bool uploadSuccess = _ftpClient.UploadFile(localFilePath, remoteFilePath);

                if (uploadSuccess)
                {
                    // Mark as uploaded
                    _transferService.MarkAsUploaded(transferId, remoteFilePath);
                    Log($"Store-specific data export completed for store: {storeId}");
                    
                    // Clean up old files for this store
                    CleanupOldFiles($"Stores/{storeId}", "store_data_", 7);
                    
                    return true;
                }
                else
                {
                    // Mark as failed
                    _transferService.MarkAsFailed(transferId, "FTP upload failed");
                    Log($"Failed to upload store-specific data for store: {storeId}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log($"Store-specific data export failed for store {storeId}: {ex.Message}");
                
                // Mark transfer as failed if we created one
                if (transferId != Guid.Empty)
                {
                    _transferService.MarkAsFailed(transferId, ex.Message);
                }
                
                return false;
            }
            finally
            {
                // Keep local file for reference
                try 
                { 
                    if (File.Exists(localFilePath))
                    {
                        Log($"Local file kept for reference: {localFilePath}");
                    }
                } 
                catch { }
            }
        }

        public bool ExportAllStores()
        {
            // Get all stores from configuration or database
            // For now, this would need to be implemented based on your LocalData structure
            Log("Export all stores not yet implemented - need store list");
            return true;
        }

        // Transfer management methods
        public List<TransferFileData> GetRecentTransfers(int days = 7)
        {
            return _transferService.GetRecentTransfers(days);
        }

        public List<TransferFileData> GetFailedTransfers()
        {
            return _transferService.GetFailedTransfers();
        }

        public List<TransferFileData> GetPendingTransfers()
        {
            return _transferService.GetPendingTransfers();
        }

        public TransferStatsData GetTransferStatistics(DateTime? fromDate = null, DateTime? toDate = null)
        {
            return _transferService.GetStatistics(fromDate, toDate);
        }

        public bool RetryFailedTransfer(Guid transferId)
        {
            try
            {
                var transfer = _transferService.GetRecentTransfers(30).FirstOrDefault(t => t.ID == transferId);
                if (transfer == null || !transfer.CanRetry)
                {
                    return false;
                }

                // Retry based on transfer type
                if (transfer.FileType == TransferFileType.Global)
                {
                    return ExportGlobalData();
                }
                else if (transfer.FileType == TransferFileType.Store && transfer.DestinationStore.HasValue)
                {
                    return ExportStoreSpecificData(transfer.DestinationStore.Value);
                }

                return false;
            }
            catch (Exception ex)
            {
                Log($"Error retrying transfer: {ex.Message}");
                return false;
            }
        }

        public int CleanupOldTransfers(int daysToKeep = 30)
        {
            return _transferService.CleanupOldTransfers(daysToKeep);
        }

        private void CleanupOldFiles(string remotePath, string filePrefix, int daysToKeep)
        {
            try
            {
                var files = _ftpClient.ListFiles(remotePath);
                var cutoffDate = DateTime.Now.AddDays(-daysToKeep);

                foreach (var file in files)
                {
                    if (file.StartsWith(filePrefix))
                    {
                        // Try to extract date from filename (assuming format: prefix_yyyyMMdd_HHmmss.json)
                        var parts = file.Replace(filePrefix, "").Replace(".json", "").Split('_');
                        if (parts.Length >= 1 && DateTime.TryParseExact(parts[0], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime fileDate))
                        {
                            if (fileDate < cutoffDate)
                            {
                                _ftpClient.DeleteFile($"{remotePath}/{file}");
                                Log($"Deleted old file: {file}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Cleanup failed: {ex.Message}");
            }
        }
    }
}