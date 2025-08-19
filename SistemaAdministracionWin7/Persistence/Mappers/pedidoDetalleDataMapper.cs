using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO.BusinessEntities;

namespace Persistence
{
    public class pedidoDetalleDataMapper
    {
        public static bool insertDetalle(DTO.BusinessEntities.pedidoDetalleData detalle)
        {
            	
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@idpedido", detalle.FatherID));
            ParametersList.Add(new SqlParameter("@codigo", detalle.codigo));
            ParametersList.Add(new SqlParameter("@precio", detalle.precio));
            ParametersList.Add(new SqlParameter("@cantidad", detalle.cantidad));


            return Conexion.ExecuteNonQuery("dbo.pedido_detalle_Insert", ParametersList);
        }



        public static List<DTO.BusinessEntities.pedidoDetalleData> getbyID(Guid id)
        {
            List<pedidoDetalleData> detalles = new List<pedidoDetalleData>();
            pedidoDetalleData detalle;
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@id", id));

            //SqlDataReader dataReader = Conexion.ExcuteReader("dbo.personal_SelectAll", ParametersList);
            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.pedido_detalle_getByID", ParametersList);
            while (dataReader.Read())
            {

                detalle = new pedidoDetalleData();
                detalle.FatherID = id;
                detalle.precio = Convert.ToDecimal(dataReader["precio"].ToString());
                detalle.codigo = dataReader["codigo"].ToString();
                detalle.cantidad = Convert.ToInt32(dataReader["cantidad"].ToString());
                
                detalles.Add(detalle);



            }
            dataReader.Close();

            return detalles;
        }

        public static List<pedidoDetalleData> getbyCodigoInterno(string p)
        {
            List<pedidoDetalleData> detalles = new List<pedidoDetalleData>();
            pedidoDetalleData detalle;
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@codigo", p+"%"));

            //SqlDataReader dataReader = Conexion.ExcuteReader("dbo.personal_SelectAll", ParametersList);
            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.pedido_detalle_getbycodigoInterno", ParametersList);
            while (dataReader.Read())
            {

                detalle = new pedidoDetalleData();
                detalle.FatherID = new Guid(dataReader["idventa"].ToString());
                detalle.codigo = dataReader["codigo"].ToString();

                detalle.precio = Convert.ToDecimal(dataReader["precio"].ToString());
                detalle.cantidad = Convert.ToInt32(dataReader["cantidad"].ToString());
                detalles.Add(detalle);

            }
            dataReader.Close();

            return detalles;
        }
    }
}
