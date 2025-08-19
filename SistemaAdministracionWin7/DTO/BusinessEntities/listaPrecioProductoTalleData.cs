namespace DTO.BusinessEntities
{
    public  class ListaPrecioProductoTalleData : ChildData
    {

        public const decimal Precio_INVALIDO = -9999;




        public ListaPrecioProductoTalleData()
        {
            ProductoTalle = new ProductoTalleData();
            Precio = Precio_INVALIDO;
        }


        public ProductoTalleData ProductoTalle { get; set; }


        public decimal Precio { get; set; }


        public bool NotInDB
        {
            get
            {
                return this.Precio == Precio_INVALIDO;
            }
        }


    }
}
