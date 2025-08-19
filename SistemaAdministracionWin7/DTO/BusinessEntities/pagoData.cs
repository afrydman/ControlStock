namespace DTO.BusinessEntities
{
    public class PagoData : ChildData
    {
         public FormaPagoData FormaPago { get; set; }
        public int Cuotas { get; set; }
        public decimal Recargo { get; set; }
        public string Lote { get; set; }
        public string Cupon { get; set; }
        public decimal Importe { get; set; }


        public PagoData ()
        {
            Cuotas = 0;
            Recargo = 0;
            Lote = "";
            Cupon = "";
            Importe = 0;
            FormaPago  = new FormaPagoData();
        }

    }
}
