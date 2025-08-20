using System;
using System.Collections.Generic;

namespace DTO.BusinessEntities
{
    // Simple DTOs for FTP transfer - optimized for JSON serialization
    
    public class ProductoTransferData
    {
        public Guid ID { get; set; }
        public string CodigoInterno { get; set; }
        public string CodigoProveedor { get; set; }
        public string Description { get; set; }
        public Guid ProveedorID { get; set; }
        public string ProveedorNombre { get; set; }
        public Guid LineaID { get; set; }
        public string LineaNombre { get; set; }
        public Guid TemporadaID { get; set; }
        public string TemporadaNombre { get; set; }
        public DateTime FechaModificacion { get; set; }
    }

    public class PrecioTransferData
    {
        public Guid ProductoTalleID { get; set; }
        public string CodigoProducto { get; set; }
        public int Talle { get; set; }
        public decimal Precio { get; set; }
        public Guid ListaPrecioID { get; set; }
        public DateTime FechaModificacion { get; set; }
    }

    public class ProveedorTransferData
    {
        public Guid ID { get; set; }
        public string Codigo { get; set; }
        public string RazonSocial { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaModificacion { get; set; }
    }

    // Container classes for JSON files
    public class GlobalDataTransfer
    {
        public DateTime FechaGeneracion { get; set; }
        public List<ProductoTransferData> Productos { get; set; }
        public List<PrecioTransferData> Precios { get; set; }
        public List<ProveedorTransferData> Proveedores { get; set; }

        public GlobalDataTransfer()
        {
            FechaGeneracion = DateTime.Now;
            Productos = new List<ProductoTransferData>();
            Precios = new List<PrecioTransferData>();
            Proveedores = new List<ProveedorTransferData>();
        }
    }

    public class StoreSpecificDataTransfer
    {
        public DateTime FechaGeneracion { get; set; }
        public Guid StoreID { get; set; }
        public List<DocumentoTransferData> Documentos { get; set; }
        public List<StockAjusteTransferData> StockAjustes { get; set; }

        public StoreSpecificDataTransfer()
        {
            FechaGeneracion = DateTime.Now;
            Documentos = new List<DocumentoTransferData>();
            StockAjustes = new List<StockAjusteTransferData>();
        }
    }

    public class DocumentoTransferData
    {
        public Guid ID { get; set; }
        public string TipoDocumento { get; set; }
        public int Numero { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string Observaciones { get; set; }
    }

    public class StockAjusteTransferData
    {
        public Guid ProductoID { get; set; }
        public string CodigoProducto { get; set; }
        public int Talle { get; set; }
        public string Color { get; set; }
        public decimal CantidadAjuste { get; set; }
        public string Motivo { get; set; }
        public DateTime Fecha { get; set; }
    }
}