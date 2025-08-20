using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using DTO.BusinessEntities;

namespace Services.JsonSerializationService
{
    public interface IJsonSerializationService
    {
        string SerializeObject<T>(T obj);
        T DeserializeObject<T>(string json);
        string SerializeRemito(RemitoData remito);
        RemitoData DeserializeRemito(string json);
        string SerializeList<T>(List<T> list);
        List<T> DeserializeList<T>(string json);
        void JsonToFile<T>(T obj, string fileName);
        void JsonToFile(RemitoData remito, string fileName);
        T ReadJsonFromFile<T>(string fileName);
        RemitoData ReadJsonFromFile(string fileName);
    }

    public class JsonSerializationService : IJsonSerializationService
    {
        private readonly JsonSerializerSettings _settings;

        public JsonSerializationService()
        {
            _settings = new JsonSerializerSettings
            {
                // Handle circular references
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                
                // Include type information for inheritance
                TypeNameHandling = TypeNameHandling.Auto,
                
                // Format for readability
                Formatting = Formatting.Indented,
                
                // Handle null values
                NullValueHandling = NullValueHandling.Include,
                
                // Use ISO date format
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                
                // Contract resolver for handling properties
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new DefaultNamingStrategy()
                }
            };
        }

        /// <summary>
        /// Serializes any object to JSON string
        /// </summary>
        public string SerializeObject<T>(T obj)
        {
            try
            {
                if (obj == null)
                    return null;

                return JsonConvert.SerializeObject(obj, _settings);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error serializing object of type {typeof(T).Name}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Deserializes JSON string to specified type
        /// </summary>
        public T DeserializeObject<T>(string json)
        {
            try
            {
                if (string.IsNullOrEmpty(json))
                    return default(T);

                return JsonConvert.DeserializeObject<T>(json, _settings);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error deserializing JSON to type {typeof(T).Name}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Serializes a Remito object with all its details
        /// </summary>
        public string SerializeRemito(RemitoData remito)
        {
            try
            {
                if (remito == null)
                    return null;

                // Create a custom object to avoid circular references and simplify structure
                var remitoJson = new
                {
                    // Base properties from GenericObject
                    remito.ID,
                    remito.Description,
                    remito.Enable,
                    
                    // DocumentoGeneralData properties
                    remito.Date,
                    Vendedor = remito.Vendedor != null ? new
                    {
                        remito.Vendedor.ID
                       
                    } : null,
                    Local = remito.Local != null ? new
                    {
                        remito.Local.ID,
                        remito.Local.Nombre,
                        remito.Local.Codigo
                    } : null,
                    remito.Numero,
                    remito.Prefix,
                    
                    // DocumentoMonetrario properties
                    remito.Monto,
                    remito.ClaseDocumento,
                    remito.IVA,
                    remito.Descuento,
                    remito.CAE,
                    remito.CAEVto,
                    
                    // RemitoData specific properties
                    remito.CantidadTotal,
                    remito.FechaRecibo,
                    LocalDestino = remito.LocalDestino != null ? new
                    {
                        remito.LocalDestino.ID,
                        remito.LocalDestino.Nombre,
                        remito.LocalDestino.Codigo
                    } : null,
                    Estado = remito.estado.ToString(),
                    
                    // Children (details)
                    Detalles = remito.Children != null ? remito.Children.ConvertAll(d => (object)new
                    {
                      
                        d.FatherID,
                        d.Codigo,
                        d.Cantidad,
                       
                    }) : new List<object>()
                };

                return JsonConvert.SerializeObject(remitoJson, _settings);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error serializing Remito: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Deserializes JSON string to RemitoData object
        /// </summary>
        public RemitoData DeserializeRemito(string json)
        {
            try
            {
                if (string.IsNullOrEmpty(json))
                    return null;

                // Use dynamic to handle the custom structure
                dynamic remitoJson = JsonConvert.DeserializeObject(json);
                
                var remito = new RemitoData
                {
                    // Base properties
                    ID = remitoJson.ID != null ? Guid.Parse(remitoJson.ID.ToString()) : Guid.Empty,
                    Description = remitoJson.Description,
                    Enable = remitoJson.Enable,
                    
                    // DocumentoGeneralData properties
                    Date = remitoJson.Date,
                    Numero = remitoJson.Numero,
                    Prefix = remitoJson.Prefix,
                    
                    // DocumentoMonetrario properties
                    Monto = remitoJson.Monto,
                    ClaseDocumento = (ClaseDocumento)Enum.Parse(typeof(ClaseDocumento), remitoJson.ClaseDocumento.ToString()),
                    IVA = remitoJson.IVA ?? 0,
                    Descuento = remitoJson.Descuento ?? 0,
                    CAE = remitoJson.CAE,
                    CAEVto = remitoJson.CAEVto ?? DateTime.MinValue,
                    
                    // RemitoData specific
                    CantidadTotal = remitoJson.CantidadTotal,
                    FechaRecibo = remitoJson.FechaRecibo ?? DTO.HelperDTO.BEGINNING_OF_TIME_DATE
                };

                // Reconstruct Vendedor
                if (remitoJson.Vendedor != null)
                {
                    remito.Vendedor = new DTO.PersonalData
                    {
                        ID = Guid.Parse(remitoJson.Vendedor.ID.ToString()),
                       
                    };
                }

                // Reconstruct Local
                if (remitoJson.Local != null)
                {
                    remito.Local = new LocalData
                    {
                        ID = Guid.Parse(remitoJson.Local.ID.ToString()),
                     
                    };
                }

                // Reconstruct LocalDestino
                if (remitoJson.LocalDestino != null)
                {
                    remito.LocalDestino = new LocalData
                    {
                        ID = Guid.Parse(remitoJson.LocalDestino.ID.ToString()),
                       
                    };
                }

                // Reconstruct Children (detalles)
                remito.Children = new List<remitoDetalleData>();
                if (remitoJson.Detalles != null)
                {
                    foreach (var detalle in remitoJson.Detalles)
                    {
                        remito.Children.Add(new remitoDetalleData
                        {
                           
                            FatherID = Guid.Parse(detalle.FatherID.ToString()),
                            Codigo = detalle.Codigo,
                            Cantidad = detalle.Cantidad,
                           
                        });
                    }
                }

                return remito;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error deserializing JSON to Remito: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Serializes a list of objects to JSON
        /// </summary>
        public string SerializeList<T>(List<T> list)
        {
            try
            {
                if (list == null)
                    return "[]";

                return JsonConvert.SerializeObject(list, _settings);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error serializing list of type {typeof(T).Name}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Deserializes JSON string to a list of specified type
        /// </summary>
        public List<T> DeserializeList<T>(string json)
        {
            try
            {
                if (string.IsNullOrEmpty(json))
                    return new List<T>();

                return JsonConvert.DeserializeObject<List<T>>(json, _settings);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error deserializing JSON to list of type {typeof(T).Name}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Saves any object as JSON to a file in the configured SaveJSON path
        /// </summary>
        public void JsonToFile<T>(T obj, string fileName)
        {
            try
            {
                string basePath = GetSaveJsonPath();
                string fullPath = Path.Combine(basePath, fileName);
                
                // Ensure directory exists
                string directory = Path.GetDirectoryName(fullPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string json = SerializeObject(obj);
                File.WriteAllText(fullPath, json);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error saving JSON to file '{fileName}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Saves a Remito object as JSON to a file in the configured SaveJSON path
        /// </summary>
        public void JsonToFile(RemitoData remito, string fileName)
        {
            try
            {
                string basePath = GetSaveJsonPath();
                string fullPath = Path.Combine(basePath, fileName);
                
                // Ensure directory exists
                string directory = Path.GetDirectoryName(fullPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string json = SerializeRemito(remito);
                File.WriteAllText(fullPath, json);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error saving Remito JSON to file '{fileName}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets the SaveJSON path from app.config
        /// </summary>
        private string GetSaveJsonPath()
        {
            try
            {
                string configPath = ConfigurationManager.AppSettings["SaveJSON"];
                
                if (string.IsNullOrEmpty(configPath))
                {
                    throw new ConfigurationErrorsException("SaveJSON configuration key not found in app.config");
                }

                // Handle relative paths
                if (!Path.IsPathRooted(configPath))
                {
                    configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configPath);
                }

                return configPath;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error reading SaveJSON configuration: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Reads and deserializes a JSON file from the configured SaveJSON path
        /// </summary>
        public T ReadJsonFromFile<T>(string fileName)
        {
            try
            {
                string basePath = GetSaveJsonPath();
                string fullPath = Path.Combine(basePath, fileName);
                
                if (!File.Exists(fullPath))
                {
                    throw new FileNotFoundException($"JSON file not found: {fullPath}");
                }

                string json = File.ReadAllText(fullPath);
                return DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error reading JSON from file '{fileName}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Reads and deserializes a Remito JSON file from the configured SaveJSON path
        /// </summary>
        public RemitoData ReadJsonFromFile(string fileName)
        {
            try
            {
                string basePath = GetSaveJsonPath();
                string fullPath = Path.Combine(basePath, fileName);
                
                if (!File.Exists(fullPath))
                {
                    throw new FileNotFoundException($"Remito JSON file not found: {fullPath}");
                }

                string json = File.ReadAllText(fullPath);
                return DeserializeRemito(json);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error reading Remito JSON from file '{fileName}': {ex.Message}", ex);
            }
        }
    }

    /// <summary>
    /// Alternative simplified service using built-in DataContractJsonSerializer
    /// Use this if you don't want to add Newtonsoft.Json dependency
    /// </summary>
    public class SimpleJsonSerializationService : IJsonSerializationService
    {
        public string SerializeObject<T>(T obj)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
                serializer.WriteObject(ms, obj);
                return System.Text.Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public T DeserializeObject<T>(string json)
        {
            using (var ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)))
            {
                var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }
        }

        public string SerializeRemito(RemitoData remito)
        {
            return SerializeObject(remito);
        }

        public RemitoData DeserializeRemito(string json)
        {
            return DeserializeObject<RemitoData>(json);
        }

        public string SerializeList<T>(List<T> list)
        {
            return SerializeObject(list);
        }

        public List<T> DeserializeList<T>(string json)
        {
            return DeserializeObject<List<T>>(json);
        }

        public void JsonToFile<T>(T obj, string fileName)
        {
            try
            {
                string basePath = GetSaveJsonPath();
                string fullPath = Path.Combine(basePath, fileName);
                
                string directory = Path.GetDirectoryName(fullPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string json = SerializeObject(obj);
                File.WriteAllText(fullPath, json);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error saving JSON to file '{fileName}': {ex.Message}", ex);
            }
        }

        public void JsonToFile(RemitoData remito, string fileName)
        {
            try
            {
                string basePath = GetSaveJsonPath();
                string fullPath = Path.Combine(basePath, fileName);
                
                string directory = Path.GetDirectoryName(fullPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string json = SerializeRemito(remito);
                File.WriteAllText(fullPath, json);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error saving Remito JSON to file '{fileName}': {ex.Message}", ex);
            }
        }

        public T ReadJsonFromFile<T>(string fileName)
        {
            try
            {
                string basePath = GetSaveJsonPath();
                string fullPath = Path.Combine(basePath, fileName);
                
                if (!File.Exists(fullPath))
                {
                    throw new FileNotFoundException($"JSON file not found: {fullPath}");
                }

                string json = File.ReadAllText(fullPath);
                return DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error reading JSON from file '{fileName}': {ex.Message}", ex);
            }
        }

        public RemitoData ReadJsonFromFile(string fileName)
        {
            try
            {
                string basePath = GetSaveJsonPath();
                string fullPath = Path.Combine(basePath, fileName);
                
                if (!File.Exists(fullPath))
                {
                    throw new FileNotFoundException($"Remito JSON file not found: {fullPath}");
                }

                string json = File.ReadAllText(fullPath);
                return DeserializeRemito(json);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error reading Remito JSON from file '{fileName}': {ex.Message}", ex);
            }
        }

        private string GetSaveJsonPath()
        {
            try
            {
                string configPath = ConfigurationManager.AppSettings["SaveJSON"];
                
                if (string.IsNullOrEmpty(configPath))
                {
                    throw new ConfigurationErrorsException("SaveJSON configuration key not found in app.config");
                }

                if (!Path.IsPathRooted(configPath))
                {
                    configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configPath);
                }

                return configPath;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error reading SaveJSON configuration: {ex.Message}", ex);
            }
        }
    }
}