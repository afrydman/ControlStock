using System;

namespace DTO.BusinessEntities
{
    public class valeData : MovimientoEnCajaData
    {

       
        
        /// <summary>
        /// Indica si se genero en base a un saldo a favor del cliente en un cambio  OR  se genero porque un cliente dejo una senia
        /// </summary>
        public bool EsCambio { get; set; }
        

        public Guid idAsoc { get; set; }// si esCambio =true, entonces sera el id de la venta, sino sera el id del ingreso. Esta bien esto? o deberian de ser 2 prop
    }
}
