using System;
using System.Collections.Generic;

namespace DTO.BusinessEntities
{
    public class FormaPagoData : GenericObject
    {
        public FormaPagoData() {
            Credito = false;
            Cuotas = new List<FormaPagoCuotaData>();
        }

        public FormaPagoData(Guid _id)
        {
            ID = _id;
            Credito = false;
        }
      
        public bool Credito { get; set; }
        
        public List<FormaPagoCuotaData> Cuotas { get; set; }//12!

    }

   
}
