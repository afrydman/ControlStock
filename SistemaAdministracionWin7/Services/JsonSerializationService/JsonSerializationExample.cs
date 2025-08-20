using System;
using System.Collections.Generic;
using DTO;
using DTO.BusinessEntities;
using Services.JsonSerializationService;

namespace Services.JsonSerializationService
{
    /// <summary>
    /// Example usage of JsonSerializationService for Remito objects
    /// </summary>
    public class JsonSerializationExample
    {
        private readonly IJsonSerializationService _jsonService;

        public JsonSerializationExample()
        {
            // Use the full-featured service with Newtonsoft.Json
            _jsonService = new JsonSerializationService();
            
            // Alternative: Use the simple built-in serializer (no Newtonsoft dependency)
            // _jsonService = new SimpleJsonSerializationService();
        }

        /// <summary>
        /// Example: Serialize a Remito to JSON
        /// </summary>
        public string ExampleSerializeRemito()
        {

            Guid remitoID = Guid.NewGuid();
            // Create a sample remito
            var remito = new RemitoData
            {
                ID = remitoID,
                Description = "Remito de transferencia entre locales",
                Enable = true,
                Date = DateTime.Now,
                Numero = 1234,
                Prefix = 1,
                Monto = 5000.50m,
                ClaseDocumento = ClaseDocumento.B,
                IVA = 1050.11m,
                Descuento = 100.00m,
                CantidadTotal = 25,
                FechaRecibo = DateTime.Now.AddDays(1),
                
                // Set origin location
                Local = new LocalData
                {
                    ID = Guid.NewGuid(),
             
                },
                
                // Set destination location
                LocalDestino = new LocalData
                {
                    ID = Guid.NewGuid(),
           
                },
                
                // Set vendor/employee
                Vendedor = new PersonalData
                {
                    ID = Guid.NewGuid(),
                    
                },
                
                // Add detail items
                Children = new List<remitoDetalleData>
                {
                    new remitoDetalleData
                    {
                       
                        FatherID =remitoID,
                        Codigo = "PROD001",
                        Cantidad = 10,
                       
                    },
                    new remitoDetalleData
                    {
                        FatherID =remitoID,
                        Codigo = "PROD002",
                        Cantidad = 20,
                    }
                }
            };

            // Serialize to JSON
            string json = _jsonService.SerializeRemito(remito);
            
            // The JSON can now be:
            // - Saved to a file
            // - Sent via API
            // - Stored in database
            // - Transmitted between systems
            
            return json;
        }

        /// <summary>
        /// Example: Deserialize JSON back to Remito object
        /// </summary>
        public RemitoData ExampleDeserializeRemito(string json)
        {
            // Deserialize from JSON
            RemitoData remito = _jsonService.DeserializeRemito(json);
            
            // Now you can access all properties
            Console.WriteLine($"Remito ID: {remito.ID}");
            Console.WriteLine($"Numero: {remito.Show}");
            Console.WriteLine($"From: {remito.Local.Nombre}");
            Console.WriteLine($"To: {remito.LocalDestino.Nombre}");
            Console.WriteLine($"Total Items: {remito.Children.Count}");
            Console.WriteLine($"Estado: {remito.estado}");
            
          
            return remito;
        }

        /// <summary>
        /// Example: Save Remito to JSON file
        /// </summary>
        public void SaveRemitoToFile(RemitoData remito, string filePath)
        {
            string json = _jsonService.SerializeRemito(remito);
            System.IO.File.WriteAllText(filePath, json);
            Console.WriteLine($"Remito saved to: {filePath}");
        }

        /// <summary>
        /// Example: Load Remito from JSON file
        /// </summary>
        public RemitoData LoadRemitoFromFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                throw new System.IO.FileNotFoundException($"File not found: {filePath}");
            }
            
            string json = System.IO.File.ReadAllText(filePath);
            RemitoData remito = _jsonService.DeserializeRemito(json);
            Console.WriteLine($"Remito loaded from: {filePath}");
            
            return remito;
        }

        /// <summary>
        /// Example: Serialize any business object
        /// </summary>
        public void ExampleSerializeOtherObjects()
        {
            // Serialize a product
            var producto = new ProductoData
            {
                ID = Guid.NewGuid(),
                Description = "Zapatilla Running",
                CodigoProveedor = "ZR-001"
            };
            string productoJson = _jsonService.SerializeObject(producto);
            
            // Serialize a list of products
            var productos = new List<ProductoData>
            {
                producto,
                new ProductoData { ID = Guid.NewGuid(), Description = "Bota Trekking" }
            };
            string productosJson = _jsonService.SerializeList(productos);
            
            // Deserialize back
            ProductoData productoRestored = _jsonService.DeserializeObject<ProductoData>(productoJson);
            List<ProductoData> productosRestored = _jsonService.DeserializeList<ProductoData>(productosJson);
        }

        /// <summary>
        /// Example: Complete workflow
        /// </summary>
        public void CompleteWorkflowExample()
        {
            Console.WriteLine("=== JSON Serialization Example ===\n");
            
            // 1. Create and serialize
            Console.WriteLine("1. Creating and serializing Remito...");
            string json = ExampleSerializeRemito();
            Console.WriteLine($"JSON Generated ({json.Length} characters)");
            Console.WriteLine(json.Substring(0, Math.Min(500, json.Length)) + "...\n");
            
            // 2. Deserialize
            Console.WriteLine("2. Deserializing JSON back to Remito object...");
            RemitoData remito = ExampleDeserializeRemito(json);
            Console.WriteLine($"Successfully restored Remito: {remito.Show}\n");
            
            // 3. Save to file
            string filePath = @"C:\temp\remito_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".json";
            Console.WriteLine($"3. Saving to file: {filePath}");
            SaveRemitoToFile(remito, filePath);
            
            // 4. Load from file
            Console.WriteLine("4. Loading from file...");
            RemitoData loadedRemito = LoadRemitoFromFile(filePath);
            Console.WriteLine($"Loaded Remito: {loadedRemito.Show}");
            
            Console.WriteLine("\n=== Example Complete ===");
        }
    }
}