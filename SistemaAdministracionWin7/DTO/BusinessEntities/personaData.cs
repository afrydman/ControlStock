using System;
using DTO.BusinessEntities.Internals;

namespace DTO.BusinessEntities
{
    public class PersonaData : GenericObject
    {
        public PersonaData()
        {
            Codigo = "0";
            RazonSocial = "";
            Email = "";
            Facebook = "";
            Direccion = "";
            Telefono = "";
            NombreContacto = "";
            Descuento = "";
            IngresosBrutos = "";
            cuil = "";
            Description = "";
        }

        public PersonaData(Guid _id)
        {
            this.ID = _id;
            Codigo = "0";
            RazonSocial = "";
            Email = "";
            Facebook = "";
            Direccion = "";
            Telefono = "";
            NombreContacto = "";
            Descuento = "";
            IngresosBrutos = "";
            cuil = "";
            Description = "";
            CondicionIva = new CondicionIvaData();
        }


        public CondicionIvaData CondicionIva { get; set; }
        public string NombreContacto { get; set; }
        public string cuil { get; set; }
        public string Telefono { get; set; }
        public string RazonSocial { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Direccion { get; set; }
        public string Codigo { get; set; }
        public string Descuento { get; set; }
        public string IngresosBrutos { get; set; }
        public string cod_razon { get { return RazonSocial + Codigo; } }

        //se  usa para impresion
        public string  CondicionIVaDescripcion
        { get { return CondicionIva!=null?CondicionIva.Description:""; } }
    }

}
