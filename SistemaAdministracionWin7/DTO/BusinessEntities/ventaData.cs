using System.Collections.Generic;

namespace DTO.BusinessEntities
{
    public class VentaData : DocumentoMonetrario<VentaDetalleData>
    {

        
        public VentaData()
        {
            Local = new LocalData();
            Vendedor = new PersonalData();
            Cliente = new ClienteData();
            _TipoDocumento = TipoDocumento.Venta;
            Pagos = new List<PagoData>();
            Children = new List<VentaDetalleData>();
            Tributos = new List<TributoNexoData>();
            Numero = 1;
            Prefix = 1;
            CAE = "";
            CAEVto = HelperDTO.BEGINNING_OF_TIME_DATE;
            Date = HelperDTO.BEGINNING_OF_TIME_DATE;
        }
        public ClienteData Cliente { get; set; }
        public bool Cambio { get; set; }
        public List<PagoData> Pagos { get; set; }
        public List<TributoNexoData> Tributos { get; set; }
    }
}
