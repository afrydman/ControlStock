using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;

namespace DTO.BusinessEntities
{
    public class TributoData : GenericObject
    {
        public int idAfip { get; set; }
    }

    public class TributoNexoData : ChildData
    {
        public TributoData Tributo { get; set; }
        public decimal Importe { get; set; }
        public decimal Alicuota { get; set; }// Si la alicuota es 0 es porque se fijo el importe a mano, de lo contrario se calculo en base a esta.
        // la base es el neto del documento.
        public decimal Base { get; set; }





        //para la impresion.
        public string Description { get { return _description; } }

        private string _description;
        public void SetDescription(string d)
        {
            _description = d;
        }

    }
}
