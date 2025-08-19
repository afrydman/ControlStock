using System;
using System.Collections.Generic;

namespace DTO.BusinessEntities
{
    public class PuntoControlStockData : DocumentoGeneralData<PuntoControlStockDetalleData>
    {

        public PuntoControlStockData()
        {
            Date = HelperDTO.BEGINNING_OF_TIME_DATE;
            Children = new List<PuntoControlStockDetalleData>();
           this.Local  = new LocalData();
            this.Vendedor = new PersonalData();
            this.Numero = 1;
            Prefix = 1;
        }

    
       
    }

    public class PuntoControlStockDetalleData : ChildData, IGetteableCodigoAndCantidad
    {


        public PuntoControlStockDetalleData()
        {
            Cantidad = HelperDTO.STOCK_MINIMO_INVALIDO;
        }

        public string Codigo { get; set; }
        public Decimal Cantidad { get; set; }  
        public string GetCodigo()
        {
            return Codigo;
        }

        public decimal GetCantidad()
        {
            return Cantidad;
        }
    }
}