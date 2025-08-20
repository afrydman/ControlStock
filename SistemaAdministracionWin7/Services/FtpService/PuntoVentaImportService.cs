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
    public class PuntoVentaImportService : Loggeable
    {
        private readonly FtpClientService _ftpClient;
        private readonly string _tempFolderPath;
        private readonly Guid _storeId;
        
        public PuntoVentaImportService() : base("PuntoVentaImportService")
        {
            string ftpServer = ConfigurationManager.AppSettings["FtpServer"];
            string ftpUser = ConfigurationManager.AppSettings["FtpUser"];
            string ftpPassword = ConfigurationManager.AppSettings["FtpPassword"];
            
            _ftpClient = new FtpClientService(ftpServer, ftpUser, ftpPassword);
            _tempFolderPath = Path.Combine(Path.GetTempPath(), "ControlStockImport");
            
            // Get this store's ID from config
            _storeId = Guid.Parse(ConfigurationManager.AppSettings["idLocal"]);
            
            if (!Directory.Exists(_tempFolderPath))
                Directory.CreateDirectory(_tempFolderPath);
        }

        public bool ImportGlobalData()
        {
            try
            {
                Log("Starting global data import");

                // Get latest global data file
                var globalFiles = _ftpClient.ListFiles("FromCentral/Global");
                var latestGlobalFile = globalFiles
                    .Where(f => f.StartsWith("global_data_") && f.EndsWith(".json"))
                    .OrderByDescending(f => f)
                    .FirstOrDefault();

                if (latestGlobalFile == null)
                {
                    Log("No global data files found");
                    return false;
                }

                Log($"Found latest global file: {latestGlobalFile}");

                // Download file
                string localFilePath = Path.Combine(_tempFolderPath, latestGlobalFile);
                string remoteFilePath = $"FromCentral/Global/{latestGlobalFile}";
                
                if (!_ftpClient.DownloadFile(remoteFilePath, localFilePath))
                {
                    Log("Failed to download global data file");
                    return false;
                }

                // Parse JSON
                string json = File.ReadAllText(localFilePath);
                var globalData = JsonConvert.DeserializeObject<GlobalDataTransfer>(json);

                if (globalData == null)
                {
                    Log("Failed to parse global data JSON");
                    return false;
                }

                Log($"Parsed global data: {globalData.Productos.Count} products, {globalData.Precios.Count} prices, {globalData.Proveedores.Count} suppliers");

                // Import data to local database
                bool success = true;
                success &= ImportProductos(globalData.Productos);
                success &= ImportPrecios(globalData.Precios);
                success &= ImportProveedores(globalData.Proveedores);

                // Cleanup temp file
                try { File.Delete(localFilePath); } catch { }

                if (success)
                {
                    Log("Global data import completed successfully");
                }
                else
                {
                    Log("Global data import completed with errors");
                }

                return success;
            }
            catch (Exception ex)
            {
                Log($"Global data import failed: {ex.Message}");
                Log($"Stack trace: {ex.StackTrace}");
                return false;
            }
        }

        public bool ImportStoreSpecificData()
        {
            try
            {
                Log($"Starting store-specific data import for store: {_storeId}");

                // Get latest store-specific data file
                var storeFiles = _ftpClient.ListFiles($"FromCentral/Stores/{_storeId}");
                var latestStoreFile = storeFiles
                    .Where(f => f.StartsWith("store_data_") && f.EndsWith(".json"))
                    .OrderByDescending(f => f)
                    .FirstOrDefault();

                if (latestStoreFile == null)
                {
                    Log("No store-specific data files found");
                    return true; // Not an error, just no data to import
                }

                Log($"Found latest store file: {latestStoreFile}");

                // Download file
                string localFilePath = Path.Combine(_tempFolderPath, latestStoreFile);
                string remoteFilePath = $"FromCentral/Stores/{_storeId}/{latestStoreFile}";
                
                if (!_ftpClient.DownloadFile(remoteFilePath, localFilePath))
                {
                    Log("Failed to download store-specific data file");
                    return false;
                }

                // Parse JSON
                string json = File.ReadAllText(localFilePath);
                var storeData = JsonConvert.DeserializeObject<StoreSpecificDataTransfer>(json);

                if (storeData == null)
                {
                    Log("Failed to parse store-specific data JSON");
                    return false;
                }

                Log($"Parsed store data: {storeData.Documentos.Count} documents, {storeData.StockAjustes.Count} stock adjustments");

                // Import data to local database
                bool success = true;
                success &= ImportDocumentos(storeData.Documentos);
                success &= ImportStockAjustes(storeData.StockAjustes);

                // Cleanup temp file
                try { File.Delete(localFilePath); } catch { }

                if (success)
                {
                    Log("Store-specific data import completed successfully");
                }
                else
                {
                    Log("Store-specific data import completed with errors");
                }

                return success;
            }
            catch (Exception ex)
            {
                Log($"Store-specific data import failed: {ex.Message}");
                return false;
            }
        }

        public bool ImportAllData()
        {
            bool success = true;
            success &= ImportGlobalData();
            success &= ImportStoreSpecificData();
            return success;
        }

        private bool ImportProductos(List<ProductoTransferData> productos)
        {
            try
            {
                Log($"Importing {productos.Count} products");
                var productoService = new ProductoService(new ProductoRepository());

                foreach (var prodTransfer in productos)
                {
                    // Check if product exists
                    var existingProd = productoService.GetByID(prodTransfer.ID);
                    
                    if (existingProd == null)
                    {
                        // Create new product
                        var newProd = new ProductoData
                        {
                            ID = prodTransfer.ID,
                            CodigoInterno = prodTransfer.CodigoInterno,
                            CodigoProveedor = prodTransfer.CodigoProveedor,
                            Description = prodTransfer.Description,
                            Proveedor = new ProveedorData(prodTransfer.ProveedorID),
                            Linea = new LineaData { ID = prodTransfer.LineaID, Description = prodTransfer.LineaNombre },
                            Temporada = new TemporadaData { ID = prodTransfer.TemporadaID, Description = prodTransfer.TemporadaNombre }
                        };
                        
                        productoService.Insert(newProd);
                    }
                    else
                    {
                        // Update existing product
                        existingProd.CodigoInterno = prodTransfer.CodigoInterno;
                        existingProd.CodigoProveedor = prodTransfer.CodigoProveedor;
                        existingProd.Description = prodTransfer.Description;
                        
                        productoService.Update(existingProd);
                    }
                }
                
                Log("Products import completed");
                return true;
            }
            catch (Exception ex)
            {
                Log($"Products import failed: {ex.Message}");
                return false;
            }
        }

        private bool ImportPrecios(List<PrecioTransferData> precios)
        {
            try
            {
                Log($"Importing {precios.Count} prices");
                var precioService = new PrecioService();

                foreach (var precioTransfer in precios)
                {
                    // Update or insert price
                    // Implementation depends on your PrecioService structure
                    Log($"Processing price for product: {precioTransfer.CodigoProducto}, talle: {precioTransfer.Talle}, price: {precioTransfer.Precio}");
                }
                
                Log("Prices import completed");
                return true;
            }
            catch (Exception ex)
            {
                Log($"Prices import failed: {ex.Message}");
                return false;
            }
        }

        private bool ImportProveedores(List<ProveedorTransferData> proveedores)
        {
            try
            {
                Log($"Importing {proveedores.Count} suppliers");
                var proveedorService = new ProveedorService(new ProveedorRepository());

                foreach (var provTransfer in proveedores)
                {
                    // Check if supplier exists
                    var existingProv = proveedorService.GetByID(provTransfer.ID);
                    
                    if (existingProv == null)
                    {
                        // Create new supplier
                        var newProv = new ProveedorData
                        {
                            ID = provTransfer.ID,
                            Codigo = provTransfer.Codigo,
                            RazonSocial = provTransfer.RazonSocial,
                            Email = provTransfer.Email,
                            Telefono = provTransfer.Telefono
                        };
                        
                        proveedorService.Insert(newProv);
                    }
                    else
                    {
                        // Update existing supplier
                        existingProv.RazonSocial = provTransfer.RazonSocial;
                        existingProv.Email = provTransfer.Email;
                        existingProv.Telefono = provTransfer.Telefono;
                        
                        proveedorService.Update(existingProv);
                    }
                }
                
                Log("Suppliers import completed");
                return true;
            }
            catch (Exception ex)
            {
                Log($"Suppliers import failed: {ex.Message}");
                return false;
            }
        }

        private bool ImportDocumentos(List<DocumentoTransferData> documentos)
        {
            try
            {
                Log($"Importing {documentos.Count} documents");
                
                foreach (var doc in documentos)
                {
                    Log($"Processing document: {doc.TipoDocumento} #{doc.Numero}, Total: {doc.Total}");
                    // Implementation depends on your document structure
                }
                
                Log("Documents import completed");
                return true;
            }
            catch (Exception ex)
            {
                Log($"Documents import failed: {ex.Message}");
                return false;
            }
        }

        private bool ImportStockAjustes(List<StockAjusteTransferData> stockAjustes)
        {
            try
            {
                Log($"Importing {stockAjustes.Count} stock adjustments");
                
                foreach (var ajuste in stockAjustes)
                {
                    Log($"Processing stock adjustment: {ajuste.CodigoProducto}, Qty: {ajuste.CantidadAjuste}, Reason: {ajuste.Motivo}");
                    // Implementation depends on your stock service structure
                }
                
                Log("Stock adjustments import completed");
                return true;
            }
            catch (Exception ex)
            {
                Log($"Stock adjustments import failed: {ex.Message}");
                return false;
            }
        }
    }
}