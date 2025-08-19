using System;
using System.Collections.Generic;

namespace DTO.BusinessEntities
{

    public enum remitoEstado 
    {
    Anulado = 1,
        Enviado = 2,
        Recibido = 3
    }
    public class RemitoData : DocumentoMonetrario<remitoDetalleData>
    {

        public RemitoData(){
            Local = new LocalData();
            LocalDestino = new LocalData();
            Vendedor = new PersonalData();
            FechaRecibo = HelperDTO.BEGINNING_OF_TIME_DATE;

            Date = HelperDTO.BEGINNING_OF_TIME_DATE; //fechaGeneracion
            CantidadTotal = 0;
            _TipoDocumento = TipoDocumento.Remito;
            Children = new List<remitoDetalleData>();
            Numero = 1;
            Prefix = 1;
            
        }
        public decimal CantidadTotal { get; set; }
        public DateTime FechaRecibo { get; set; }
        public LocalData LocalDestino { get; set; }


        public remitoEstado estado
        {
            get { 
            
            if (!Enable)
	        {
                return remitoEstado.Anulado;
	        }
            if (FechaRecibo == HelperDTO.BEGINNING_OF_TIME_DATE)
	            {
                    return remitoEstado.Enviado;
	            }else
	            {
                    return remitoEstado.Recibido;
	            }

            }
        }


        public string Show
        {
            get { return Local.Codigo+'-'+Prefix.ToString("0000")+'-'+Numero.ToString("00000000"); }
        }
        public string ShowConFecha
        {
            get { return Date.ToShortDateString() + '-'+ Local.Codigo + '-' + Prefix.ToString("0000") + '-' + Numero.ToString("00000000"); }
        }
        

    }
}
