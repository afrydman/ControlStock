using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using DTO.BusinessEntities;
using Persistence.LogService;
using Repository.Repositories.ProductoRepository;
using Services.ProductoService;
using SharedForms;

namespace Examples
{
    // Example 1: Enhanced Repository Implementation
    public class LoggedProductoRepository : LoggedRepository<ProductoData>
    {
        public LoggedProductoRepository() : base("Producto")
        {
        }

        protected override bool ExecuteInsert(ProductoData entity)
        {
            // Your existing insert logic here
            return Persistence.Conexion.ExecuteNonQuery("INSERT INTO Productos...", null);
        }

        protected override bool ExecuteUpdate(ProductoData entity)
        {
            // Your existing update logic here
            return Persistence.Conexion.ExecuteNonQuery("UPDATE Productos...", null);
        }

        protected override bool ExecuteDelete(Guid id)
        {
            // Your existing delete logic here
            return Persistence.Conexion.ExecuteNonQuery("DELETE FROM Productos...", null);
        }

        protected override ProductoData ExecuteGetByID(Guid id)
        {
            // Your existing get by ID logic here
            return null; // Replace with actual implementation
        }

        protected override List<ProductoData> ExecuteGetAll()
        {
            // Your existing get all logic here
            return new List<ProductoData>(); // Replace with actual implementation
        }

        // Custom query methods with logging
        public List<ProductoData> GetByProveedor(Guid proveedorId)
        {
            return LoggedQuery(() =>
            {
                // Your existing query logic
                return new List<ProductoData>();
            }, $"GetByProveedor_{proveedorId}");
        }

        public List<ProductoData> GetLowStockProducts(int threshold)
        {
            return LoggedQuery(() =>
            {
                // Your existing query logic
                return new List<ProductoData>();
            }, $"GetLowStockProducts_{threshold}");
        }
    }

    // Example 2: Enhanced Service Implementation
    public class LoggedProductoService : LoggedService<ProductoData>
    {
        public LoggedProductoService(IProductoRepository repository) 
            : base("Producto", repository)
        {
        }

        // Override validation methods
        protected override void ValidateForInsert(ProductoData entity)
        {
            base.ValidateForInsert(entity);
            
            // Custom validation with logging
            if (!ValidateBusinessRule("ProductCodeUnique", 
                () => IsProductCodeUnique(entity.CodigoInterno),
                $"Product code: {entity.CodigoInterno}"))
            {
                throw new InvalidOperationException($"Product code {entity.CodigoInterno} already exists");
            }

            if (!ValidateBusinessRule("ProductDescriptionRequired",
                () => !string.IsNullOrEmpty(entity.Description),
                $"Product ID: {entity.ID}"))
            {
                throw new ArgumentException("Product description is required");
            }
        }

        protected override void OnEntityInserted(ProductoData entity)
        {
            base.OnEntityInserted(entity);
            
            // Log business event
            _businessLogger.LogProductOperation("CREATE", entity.CodigoInterno, entity.Description, true, 
                $"New product created by supplier {entity.Proveedor?.RazonSocial}");
            
            // Trigger related operations
            InitializeDefaultPricing(entity);
        }

        protected override void OnEntityUpdated(ProductoData newEntity, ProductoData oldEntity)
        {
            base.OnEntityUpdated(newEntity, oldEntity);
            
            // Log specific changes
            if (oldEntity.Description != newEntity.Description)
            {
                _businessLogger.LogInfo($"Product description changed: {oldEntity.Description} -> {newEntity.Description}", "PRODUCT_UPDATE");
            }
        }

        // Custom business methods
        public bool UpdateProductPrices(List<ProductoData> products, decimal percentageIncrease)
        {
            return LoggedBusinessOperation(() =>
            {
                var successful = 0;
                var failed = 0;

                foreach (var product in products)
                {
                    try
                    {
                        // Log price change
                        _businessLogger.LogPriceChange(product.CodigoInterno, 0, 0, $"{percentageIncrease}% increase");
                        successful++;
                    }
                    catch (Exception ex)
                    {
                        _businessLogger.LogError($"Price update failed for product {product.CodigoInterno}", ex, "PRICE_UPDATE");
                        failed++;
                    }
                }

                _businessLogger.LogInfo($"Bulk price update completed: {successful} successful, {failed} failed", "BULK_PRICE_UPDATE");
                return failed == 0;

            }, "BULK_PRICE_UPDATE");
        }

        private bool IsProductCodeUnique(string productCode)
        {
            // Implementation to check uniqueness
            return true; // Placeholder
        }

        private void InitializeDefaultPricing(ProductoData product)
        {
            _businessLogger.LogInfo($"Initializing default pricing for product {product.CodigoInterno}", "PRICE_INIT");
        }
    }

    // Example 3: Enhanced Form Implementation
    public partial class LoggedProductForm : LoggedForm
    {
        private LoggedProductoService _productoService;

        public LoggedProductForm() : base("ProductForm")
        {
            InitializeComponent();
            _productoService = new LoggedProductoService(new LoggedProductoRepository());
        }

        protected override void OnFormLoading()
        {
            base.OnFormLoading();
            
            // Load data with logging
            LoadProducts();
            LoadSuppliers();
        }

        private void LoadProducts()
        {
            LoggedDataOperation(() =>
            {
                var products = _productoService.GetAll();
                
                // Bind to UI
                dataGridView1.DataSource = products;
                
                LogDataLoad("Products", products.Count, 0);
                
            }, "LOAD_PRODUCTS");
        }

        private void LoadSuppliers()
        {
            LoggedDataOperation(() =>
            {
                // Load suppliers
                LogDataLoad("Suppliers", 10, 150); // Example
                
            }, "LOAD_SUPPLIERS");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            LogButtonClick("Save", "Product data");
            
            if (!ValidateForm())
                return;
            
            var product = CreateProductFromForm();
            
            LoggedDataOperation(() =>
            {
                var success = _productoService.Insert(product);
                LogDataSave("Product", success);
                
                if (success)
                {
                    ShowInfoMessage("Success", "Product saved successfully");
                    ClearForm();
                }
                
            }, "SAVE_PRODUCT");
        }

        protected override bool OnValidateForm()
        {
            var isValid = true;
            
            // Validate product code
            isValid &= ValidateField(txtProductCode, 
                code => !string.IsNullOrWhiteSpace(code) && code.Length >= 3,
                "Product code must be at least 3 characters long");
            
            // Validate description
            isValid &= ValidateField(txtDescription,
                desc => !string.IsNullOrWhiteSpace(desc),
                "Product description is required");
            
            return isValid;
        }

        private ProductoData CreateProductFromForm()
        {
            return new ProductoData
            {
                CodigoInterno = txtProductCode.Text,
                Description = txtDescription.Text,
                // ... other properties
            };
        }

        private void ClearForm()
        {
            txtProductCode.Text = "";
            txtDescription.Text = "";
            LogUserAction("Form Cleared");
        }

        private void InitializeComponent()
        {
            // Designer-generated code would go here
            this.txtProductCode = new TextBox();
            this.txtDescription = new TextBox();
            this.dataGridView1 = new DataGridView();
        }

        private TextBox txtProductCode;
        private TextBox txtDescription;
        private DataGridView dataGridView1;
    }

    // Example 4: Application-wide logging initialization
    public static class ApplicationLoggingSetup
    {
        public static void InitializeApplicationLogging()
        {
            try
            {
                // Get current user context
                var currentUser = Environment.UserName;
                var sessionId = Guid.NewGuid().ToString("N")[..8];
                
                // Initialize logging system
                LogConfiguration.InitializeApplicationLogging("ControlStock", currentUser);
                
                // Set global context for all loggers
                LogManager.SetGlobalContext(currentUser, sessionId);
                
                // Set appropriate log levels
                #if DEBUG
                LogManager.SetGlobalMinimumLevel(LogLevel.Debug);
                #else
                LogManager.SetGlobalMinimumLevel(LogLevel.Info);
                #endif

                // Log application startup
                var systemLogger = LoggerFactory.GetSystemLogger();
                systemLogger.LogApplicationStart("ControlStock", "1.0", currentUser);
                
                // Log system information
                systemLogger.LogPerformanceMetric("StartupMemoryMB", GC.GetTotalMemory(false) / 1024 / 1024, "MB");
                
            }
            catch (Exception ex)
            {
                // Fallback error handling
                MessageBox.Show($"Failed to initialize logging system: {ex.Message}", 
                    "Logging Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static void ShutdownApplicationLogging()
        {
            try
            {
                var systemLogger = LoggerFactory.GetSystemLogger();
                systemLogger.LogApplicationShutdown("ControlStock", Environment.TickCount);
                
                // Shutdown logging system
                LogConfiguration.ShutdownLogging();
            }
            catch
            {
                // Ignore shutdown errors
            }
        }
    }

    // Example 5: Error handling and logging throughout the application
    public static class GlobalErrorHandler
    {
        public static void HandleUnhandledException(Exception ex)
        {
            try
            {
                var systemLogger = LoggerFactory.GetSystemLogger();
                systemLogger.LogCritical("Unhandled application exception", ex, "CRITICAL_ERROR");
                
                // Show user-friendly error message
                MessageBox.Show($"An unexpected error occurred. The error has been logged for review.\n\nError: {ex.Message}",
                    "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                // Last resort - basic error display
                MessageBox.Show("A critical error occurred and could not be logged.", 
                    "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void SetupGlobalExceptionHandling()
        {
            Application.ThreadException += (sender, e) => HandleUnhandledException(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => 
                HandleUnhandledException(e.ExceptionObject as Exception);
        }
    }

    // Example 6: Performance monitoring throughout the application
    public static class PerformanceMonitor
    {
        private static readonly SystemLogger _logger = LoggerFactory.GetSystemLogger();
        private static readonly Timer _performanceTimer;

        static PerformanceMonitor()
        {
            _performanceTimer = new System.Threading.Timer(LogPerformanceMetrics, null, 
                TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5));
        }

        private static void LogPerformanceMetrics(object state)
        {
            try
            {
                var process = Process.GetCurrentProcess();
                
                _logger.LogPerformanceMetric("WorkingSetMB", process.WorkingSet64 / 1024 / 1024, "MB");
                _logger.LogPerformanceMetric("PrivateMemoryMB", process.PrivateMemorySize64 / 1024 / 1024, "MB");
                _logger.LogPerformanceMetric("GCMemoryMB", GC.GetTotalMemory(false) / 1024 / 1024, "MB");
                _logger.LogPerformanceMetric("ThreadCount", process.Threads.Count);
                _logger.LogPerformanceMetric("HandleCount", process.HandleCount);
            }
            catch (Exception ex)
            {
                _logger.LogError("Performance monitoring failed", ex, "PERFORMANCE");
            }
        }
    }
}

// Example usage in Program.cs
/*
static void Main()
{
    // Initialize logging first
    Examples.ApplicationLoggingSetup.InitializeApplicationLogging();
    
    // Set up global exception handling
    Examples.GlobalErrorHandler.SetupGlobalExceptionHandling();
    
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
    
    // Run your application
    Application.Run(new MainForm());
    
    // Cleanup logging on shutdown
    Examples.ApplicationLoggingSetup.ShutdownApplicationLogging();
}
*/