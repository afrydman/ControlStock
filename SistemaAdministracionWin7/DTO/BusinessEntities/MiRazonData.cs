using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO.BusinessEntities.Internals;

namespace DTO.BusinessEntities
{
    public class MiRazonData : GenericObject
    {

        public string RazonSocial { get; set; }
        public string Domicilio { get; set; }

        public CondicionIvaData CondicionIva { get; set; }

        public string Cuit { get; set; }

        public string IngresosBrutos { get; set; }

        public DateTime inicioActividad { get; set; }
    }
}
