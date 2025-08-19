using System;

namespace DTO.BusinessEntities
{
    public class ChildData
    {
        public Guid FatherID { get; set; }
    }


    public interface IGetteableCodigoAndCantidad
    {
        string GetCodigo();
        decimal GetCantidad();
    }
}
