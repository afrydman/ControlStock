using System;
using System.Collections.Generic;

namespace DTO.BusinessEntities
{
    public enum tipoNota
    {
        DebitoCliente=1,
        DebitoProveedor=2,
        CreditoCliente=3,
        CreditoProveedor=4
    }

    public class NotaData : DocumentoMonetrario<NotaDetalleData>
    {
        public tipoNota tipo { get; set; }
      

        public NotaData() {
            Local = new LocalData();
            Vendedor = new PersonalData();
            tercero = new PersonaData();
            Numero = 1;
            Prefix = 1;
             Tributos = new List<TributoNexoData>();
             CAE = "";
             CAEVto = HelperDTO.BEGINNING_OF_TIME_DATE;
        }

        public NotaData(Guid _id)
        {
            ID = _id;
            Local = new LocalData();
            Vendedor = new PersonalData();
            tercero = new PersonaData();
            Numero = 1;
            Prefix = 1;
            Tributos = new List<TributoNexoData>();
            CAE = "";
            CAEVto = HelperDTO.BEGINNING_OF_TIME_DATE;
        }

        public PersonaData tercero { get; set; }
        public List<TributoNexoData> Tributos { get; set; }
    }

    public class NotaDetalleData : CantidadCodigoData
    {
        public string Description { get; set; }
    }

}
