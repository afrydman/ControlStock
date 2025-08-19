using System;

namespace DTO.BusinessEntities
{
    public class LocalData : GenericObject
    {
        public string Direccion { get; set; }
        public string Telefono { get; set; }
       
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }

        public DateTime fechaStock { get; set; }

        public LocalData()
        {
            fechaStock = HelperDTO.BEGINNING_OF_TIME_DATE;
        }


    }
}
