using System;
using System.Collections.Generic;
using Persistence;
using DTO.BusinessEntities;

namespace BusinessComponents
{
    public class pedidodetalle
    {

        public static bool insertDetalle(List<DTO.BusinessEntities.pedidoDetalleData> detalles)
        {
            bool tsk;
            foreach (pedidoDetalleData vd in detalles)
            {
                tsk = insertDetalle(vd);
                if (!tsk)
                    return false;
            }
            return true;

        }

        public static bool insertDetalle(DTO.BusinessEntities.pedidoDetalleData detalle)
        {


            return pedidoDetalleDataMapper.insertDetalle(detalle);
        }

        public static List<DTO.BusinessEntities.pedidoDetalleData> getById(Guid id)
        {
            return pedidoDetalleDataMapper.getbyID(id);
        }

      

      
    }
}
