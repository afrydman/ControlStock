using System;
using DTO.BusinessEntities;

namespace DTO
{

    public class CantidadCodigoDescriptionData : CantidadCodigoData
    {
        public string Description { get { return _description; } }

        private string _description;
        public void SetDescription(string d)
        {
            _description = d;
        }
        public string Codigo { get; set; }

        public CantidadCodigoDescriptionData(string _codigo, decimal _cantidad)
        {
            Cantidad = _cantidad;
            Codigo = _codigo;
        }

        public CantidadCodigoDescriptionData()
        {
            
        }
        public string GetCodigo()
        {
            return Codigo;
        }

        public string GetProveedor()
        {
            if (string.IsNullOrEmpty(Codigo)||Codigo.Length<10) return "";
            return Codigo.Substring(0, 4);
        }
        public string GetProveedorArticulo()
        {
            if (string.IsNullOrEmpty(Codigo) || Codigo.Length < 10) return "";
            return Codigo.Substring(0, 7);
        }
        public string GetArticulo()
        {
            if (string.IsNullOrEmpty(Codigo) || Codigo.Length < 10) return "";
            return Codigo.Substring(3, 3);
        }
        public string GetColor()
        {
            if (string.IsNullOrEmpty(Codigo) || Codigo.Length < 10) return "";
            return Codigo.Substring(7, 3);
        }
        public string GetTalle()
        {
            if (string.IsNullOrEmpty(Codigo) || Codigo.Length < 12) return "";
            return Codigo.Substring(10, 2);
        }





    }

    public class CantidadCodigoData : ChildData
    {
        public decimal Alicuota { get; set; }
        public decimal Bonificacion { get; set; }
        public decimal PrecioUnidad { get; set; }
        public decimal Cantidad { get; set; }
     
     
        
        //se usa para la impresion.
     
        public CantidadCodigoData()
        {
        }

        public decimal GetCantidad()
        {
            return Cantidad;
        }

        public void SetSubtotal(decimal d)
        {
            _Subtotal = d;
        }

        public void SetSubtotalConIva(decimal d)
        {
            _SubtotalConIva = d;
        }

        private decimal _Subtotal;
        public decimal Subtotal
        { get { return _Subtotal; } }

        private decimal _SubtotalConIva;
        public decimal SubtotalConIva
        { get { return _SubtotalConIva; } }

     
    }
}
