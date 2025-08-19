using System;

namespace DTO.BusinessEntities
{
    public class MovimientoEnCajaData : GenericObject
    {

        public MovimientoEnCajaData()
        {

            Local = new LocalData();
            Personal = new PersonalData();
            
            codigo = " ";
            Date = HelperDTO.BEGINNING_OF_TIME_DATE;
            Numero = 1;
            Prefix = 1;
        }

        public MovimientoEnCajaData(Guid _id)
        {
            ID = _id;
            Local = new LocalData();
            Personal = new PersonalData();
           
            codigo = " ";
            Date = HelperDTO.BEGINNING_OF_TIME_DATE;
            Numero = 1;
            Prefix = 1;

        }
        public DateTime Date { get; set; }

        public PersonalData Personal { get; set; }
        public LocalData Local { get; set; }


        public decimal Monto { get; set; }
        public DateTime fechaUso { get; set; }
       
        public string codigo { get; set; }
      
       
        public int Numero { get; set; }
        public int Prefix { get; set; }


        public string Show { get { return Prefix.ToString("0000") + "-" + Numero.ToString("00000000"); } }

    }

    public class RetiroData : MovimientoEnCajaData
    {

        public RetiroData() : base()
        {
            TipoRetiro = new TipoRetiroData();
        }

        public RetiroData(Guid _id) : base(_id)
        {
            TipoRetiro = new TipoRetiroData();
        }

        public TipoRetiroData TipoRetiro { get; set; }
    }
    public class IngresoData : MovimientoEnCajaData
    {

        public IngresoData()
            : base()
        {
            TipoIngreso = new TipoIngresoData();
        }

        public IngresoData(Guid _id)
            : base(_id)
        {
            TipoIngreso = new TipoIngresoData();
        }

        public TipoIngresoData TipoIngreso { get; set; }
    }
}
