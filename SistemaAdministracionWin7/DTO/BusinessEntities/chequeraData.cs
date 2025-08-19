namespace DTO.BusinessEntities
{
    public class ChequeraData : GenericObject
    {

        public ChequeraData()
        {
            Cuenta = new CuentaData();
        }

        public CuentaData Cuenta { get; set; }
        public string Primero { get; set; }
        public string Ultimo { get; set; }
        public int CodigoInterno { get; set; }
        public string Show { get { return Cuenta.Show + " - " + Description; } }
        public string Siguiente { get; set; }
    }
}
