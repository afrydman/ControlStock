namespace DTO.BusinessEntities
{
    public  class StockData
    {


           public StockData()//algo muy feo pero para inicializarlo siempre con el invalid stock
        {

            Producto = new ProductoData();
            Color = new ColorData();
            Local = new LocalData();
            Metros = -1;
            Stock = HelperDTO.STOCK_MINIMO_INVALIDO;

        }

        
        public ProductoData Producto { get; set; }
        public ColorData Color { get; set; }
        public LocalData Local { get; set; }
        
        public int Talle { get; set; }
        public decimal Stock { get; set; }
        public decimal Metros { get; set; }
       
        public string Talle61 { get; set; }
        public virtual string Codigo
        {
            get
            {
                if (this.Metros == -1)//dudoso....
                {
                    return Producto.CodigoInterno + Color.Codigo + Talle.ToString("00");
                }
                else
                {
                    return Producto.CodigoInterno + Color.Codigo + Talle61.PadLeft(2, '0');
                }
            }

        }

        public bool NotInDB {
            get
            {
                return this.Stock == HelperDTO.STOCK_MINIMO_INVALIDO;
            }
        }
    }
}
