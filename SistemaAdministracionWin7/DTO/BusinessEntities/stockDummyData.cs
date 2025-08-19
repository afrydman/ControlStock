namespace DTO.BusinessEntities
{
    public class stockDummyData : StockData
    {
       


        public stockDummyData(string codigo) { _cod = codigo; }

        public stockDummyData()
        {
            this.Producto.Description = "No informado";
            this.Producto.Proveedor.RazonSocial = "No informado";
        }
     

        public string _cod { get; set; }

        public override string Codigo
        {
            get { return _cod; }
        }

       
    }
}
