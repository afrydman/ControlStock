using System;


namespace DTO.BusinessEntities
{
    public class remitoDetalleData : ChildData, IGetteableCodigoAndCantidad
    {
        public string Codigo { get; set; }
        public decimal Cantidad { get; set; }


        public string GetCodigo()
        {
            return Codigo;
        }

        public decimal GetCantidad()
        {
            return Cantidad;
        }

        public Guid GetFatherID()
        {
            return FatherID;
        }
    }
}
