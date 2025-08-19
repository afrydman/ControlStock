using System;

namespace DTO.BusinessEntities
{




    public class ProductoData : GenericObject
    {

        public ProductoData()
        {

            Temporada = new TemporadaData();
            Linea = new LineaData();
            Proveedor = new ProveedorData();
            CodigoProveedor = "";
            Description = "";
        }
        public ProductoData(Guid _id)
        {
            ID = _id;
            Temporada = new TemporadaData();
            Linea = new LineaData();
            Proveedor = new ProveedorData();
            CodigoProveedor = "";
            Description = "";

        }


       
        public ProveedorData Proveedor { get; set; }
       
        public string CodigoProveedor { get; set; }
        public LineaData Linea { get; set; }
        public TemporadaData Temporada { get; set; }
        public string CodigoInterno { get; set; }
        public string Show
        {
            get { return  CodigoProveedor + " - " + Description; }
        }
       






        
    }
}
