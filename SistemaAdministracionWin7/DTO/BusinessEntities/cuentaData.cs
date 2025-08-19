namespace DTO.BusinessEntities
{

    public enum TipoCuenta { Banco, Cartera, Tarjeta, Otra };
    public class CuentaData : GenericObject
    {

        public CuentaData()
        {
            Banco= new BancoData();
            cbu = "";
            Sucursal = "";
            Cuenta = "";
            Titular = "";
             
        }

        public string cbu { get; set; }
        public TipoCuenta TipoCuenta { get; set; }
        public bool esCuentaCorriente { get; set; }
        public BancoData Banco { get; set; }
        public decimal Saldo { get; set; }
        public decimal Descubierto { get; set; }
        public string Sucursal { get; set; }
        public string Cuenta { get; set; }
        public string Titular { get; set; }

        public string Show
        {
            get
            {
                return Banco != null ? Banco.Description + " - " + Description : Description;
            }
        }
    }
}
