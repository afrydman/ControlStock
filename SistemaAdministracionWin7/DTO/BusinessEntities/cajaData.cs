using System;

namespace DTO.BusinessEntities
{
    public class CajaData
    {

     

        public Guid ID { get; set; }
        public LocalData Local { get; set; }
        public DateTime Date { get; set; }
        public decimal Monto { get; set; }
        public CajaData()
        {
            Monto = -1;
            Date = HelperDTO.BEGINNING_OF_TIME_DATE;
            Local = new LocalData();
            ID = Guid.Empty;
        }
    }
}
