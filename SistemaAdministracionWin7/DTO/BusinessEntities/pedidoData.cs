using System;
using System.Collections.Generic;

namespace DTO.BusinessEntities
{
    public class pedidoData : DocumentoMonetrario<pedidoDetalleData> 
    {
        public pedidoData()
        {

            Local = new LocalData();
            Vendedor = new PersonalData();
            Cliente = new ClienteData();
            _TipoDocumento = BusinessEntities.TipoDocumento.Pedido;
        }
        public pedidoData(Guid _id)
        {
            ID = _id;
            Local = new LocalData();
            Vendedor = new PersonalData();
            Cliente = new ClienteData();
            _TipoDocumento = BusinessEntities.TipoDocumento.Pedido;
        }
        public ClienteData Cliente { get; set; }

        public ClaseDocumento tipoVenta { get; set; }
        public FormaPagoData formaPago { get; set; }
        public bool cambio { get; set; }
  
        public bool coniva { get; set; }



        public List<FormaPagoCuotaData> formasdepago { get; set; }

             public bool completo { get; set; }


    }
}
