using System;
using System.Collections.Generic;

namespace DTO.BusinessEntities
{
    public class ComprasProveedoresData : DocumentoMonetrario<ComprasProveedoresdetalleData>
    {

        public ComprasProveedoresData() {


            Vendedor =new PersonalData();
            Proveedor = new ProveedorData();
            Local =  new LocalData();;
            Enable = false;
            Numero = 1;
            Prefix = 1;
            _TipoDocumento = BusinessEntities.TipoDocumento.Compra;
            Date = HelperDTO.BEGINNING_OF_TIME_DATE;
            Tributos = new List<TributoNexoData>();
        }
        public ComprasProveedoresData(Guid _id)
        {

           ID = _id;
            Vendedor = new PersonalData();
            Proveedor = new ProveedorData();
            Local = new LocalData(); ;
            Enable = false;
            Numero = 1;
            Prefix = 0;
            _TipoDocumento = BusinessEntities.TipoDocumento.Compra;
            Date = HelperDTO.BEGINNING_OF_TIME_DATE;
            Tributos = new List<TributoNexoData>();
        }

        public ProveedorData Proveedor { get; set; }
        public DateTime FechaFactura  { get; set; }


        public List<TributoNexoData> Tributos { get; set; }
    }
}
