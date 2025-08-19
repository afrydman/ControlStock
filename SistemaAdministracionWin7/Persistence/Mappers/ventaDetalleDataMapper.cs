using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO.BusinessEntities;

namespace Persistence
{
    public class ventaDetalleDataMapper
    {
        public static bool insertDetalle(DTO.BusinessEntities.ventaDetalleData detalle, bool connLocal = true)
        {
            	
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@idVenta", detalle.FatherID));
            ParametersList.Add(new SqlParameter("@codigo", detalle.codigo));
            ParametersList.Add(new SqlParameter("@precio", detalle.precioUnidad));
            ParametersList.Add(new SqlParameter("@cantidad", detalle.cantidad));
            ParametersList.Add(new SqlParameter("@alicuota", detalle.alicuota));


            return Conexion.ExecuteNonQuery("dbo.venta_detalle_Insert", ParametersList, true, connLocal);
        }



        public static List<DTO.BusinessEntities.ventaDetalleData> getbyID(Guid id, bool connLocal = true)
        {
            List<ventaDetalleData> detalles = new List<ventaDetalleData>();
            ventaDetalleData detalle;
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@id", id));

            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.ventas_detalle_getByID", ParametersList,connLocal);
            while (dataReader.Read())
            {
                detalle = CargoDetalle(dataReader);
                detalles.Add(detalle);
            }
            dataReader.Close();

            return detalles;
        }

        public static List<ventaDetalleData> getbyCodigoInterno(string p)
        {
            List<ventaDetalleData> detalles = new List<ventaDetalleData>();
            ventaDetalleData detalle;
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@codigo", p+"%"));

            //SqlDataReader dataReader = Conexion.ExcuteReader("dbo.personal_SelectAll", ParametersList);
            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.ventas_detalle_getbycodigoInterno", ParametersList);
            while (dataReader.Read())
            {

                
                detalle=CargoDetalle(dataReader);

                detalles.Add(detalle);



            }
            dataReader.Close();

            return detalles;
        }

        private static ventaDetalleData CargoDetalle(SqlDataReader dataReader)
        {
            ventaDetalleData detalle = new ventaDetalleData();

            detalle.FatherID = new Guid(dataReader["idventa"].ToString());
            detalle.precioUnidad = Convert.ToDecimal(dataReader["precio"].ToString());
            detalle.alicuota = Convert.ToDecimal(dataReader["alicuota"].ToString());
            detalle.codigo = dataReader["codigo"].ToString();
            detalle.cantidad = Convert.ToDecimal(dataReader["cantidad"].ToString());
            return detalle;
        }
    }
}
