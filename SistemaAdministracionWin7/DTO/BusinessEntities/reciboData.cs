using System.Collections.Generic;

namespace DTO.BusinessEntities
{
    public class ReciboData : DocumentoMonetrario<ReciboOrdenPagoDetalleData>
    {
       public ReciboData() {

            Vendedor = new PersonalData();
            tercero = new ClienteData();
            Local = new LocalData(); ;
            Enable = false;
            Numero = 1;
            Prefix = 1;
           Children = new List<ReciboOrdenPagoDetalleData>();
        }
       public ClienteData tercero { get; set; }
    }

    public class OrdenPagoData : DocumentoMonetrario<ReciboOrdenPagoDetalleData>
    {
        public OrdenPagoData()
        {

            Vendedor = new PersonalData();
            Tercero = new ProveedorData();
            Local = new LocalData(); ;
            Enable = false;
            Numero = 1;
            Prefix = 1;
            Children = new List<ReciboOrdenPagoDetalleData>();
        }
        public ProveedorData Tercero { get; set; }
    }

    public class ReciboOrdenPagoDetalleData : ChildData
    {

        public ChequeData Cheque { get; set; }//si es null es efectivo
        public decimal Monto { get; set; }
        public CuentaData Cuenta { get; set; }

        public ReciboOrdenPagoDetalleData()
        {
            Cheque = new ChequeData();
            Cuenta = new CuentaData();
        }
    
    }

   

   

  
}
