using System;

namespace DTO.BusinessEntities
{
    public class detalleStockData
    {

        public string codigo { get; set; }

        public string descripcion { get; set; }

        public DateTime fecha { get; set; }
        public decimal cantidad { get; set; }
        public ProductoData producto { get; set; }
        public ColorData color { get; set; }
        public int talle { get; set; }

      

    }
}
