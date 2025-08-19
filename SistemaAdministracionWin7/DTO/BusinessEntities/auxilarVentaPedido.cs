namespace DTO.BusinessEntities
{
    public class auxilarVentaPedido : VentaData
    {

        public auxilarVentaPedido()
        {

        }
        
        private bool _esventa { get; set; }
        public bool esventa
        {
            get { return _esventa; }
            set { _esventa = value; }

        }


       
    }
}
