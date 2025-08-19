using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.BusinessEntities.AfipEntities
{
    public abstract class GenericAfip : GenericObject
    {
        
        public DateTime FchDesde { get; set; }//o datetime?
        public DateTime FchHasta { get; set; }//o datetime?
        public int idAfip { get; set; }
        
    }
}
