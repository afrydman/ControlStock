namespace DTO.BusinessEntities
{
    public class ComprasProveedoresdetalleData : CantidadCodigoDescriptionData, IGetteableCodigoAndCantidad
    {

        public decimal PrecioExtra { get; set; }
        /// <summary>
        /// // Si hay MTS hay que recordar multiplicar por los mts del producto. Ahora solo se usa en un formulario
        /// </summary>
        public decimal SubTotal

        {
            get { return (Cantidad * (PrecioExtra + PrecioUnidad)); }
        }

     

    }
}
