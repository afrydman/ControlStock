using System;

namespace DTO.BusinessEntities
{
   

    public class FormaPagoCuotaData : ChildData
    {
        public Guid ID { get; set; }
        public int Cuota { get; set; }
        public decimal Aumento { get; set; }
    }
}
