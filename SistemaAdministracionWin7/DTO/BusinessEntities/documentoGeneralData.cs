using System;
using System.Collections.Generic;
using System.Data;
using System.Security.AccessControl;

namespace DTO.BusinessEntities
{
    public enum TipoDocumento
    {
        NotaCredito =0,
        NotaDebito = 1,
        Pago = 2,
        Compra = 3,
        Venta = 4,
        Pedido = 5,
        Recibo = 6,
        Remito = 7
    }
    public enum ClaseDocumento 
    { A=0
        , B=1
            , C = 2
    }

    public abstract class DocumentoMonetrario<T> : DocumentoGeneralData<T> where T : ChildData
    {
        public decimal Monto { get; set; }// es el Total.
        public ClaseDocumento ClaseDocumento { get; set; }

        protected TipoDocumento _TipoDocumento;
        public TipoDocumento TipoDocumento { get { return _TipoDocumento; } }

        public decimal IVA { get; set; }
        public decimal Descuento { get; set; }

        public string CAE { get; set; }
        public DateTime CAEVto { get; set; }

    }


    public abstract class DocumentoGeneralData<T> : GenericObject where T : ChildData
    {
        public DateTime Date { get; set; }
        public PersonalData Vendedor { get; set; }
        public LocalData Local { get; set; }
      
        public int Numero { get; set; }
        public int Prefix { get; set; }
        public string NumeroCompleto { get { return Prefix.ToString("0000") + "-" + Numero.ToString("00000000"); } }

        public List<T> Children { get; set; }



     

    }
}
