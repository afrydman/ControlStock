using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO.BusinessEntities;

namespace Persistence
{
    public class VentaDetalleDataMapper
    {
        public static bool insertDetalle(DTO.BusinessEntities.VentaDetalleData detalle, bool connLocal = true)
        {
            	
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@idVenta", detalle.FatherID));
            ParametersList.Add(new SqlParameter("@codigo", detalle.Codigo));
            ParametersList.Add(new SqlParameter("@precio", detalle.PrecioUnidad));
            ParametersList.Add(new SqlParameter("@cantidad", detalle.Cantidad));
            ParametersList.Add(new SqlParameter("@alicuota", detalle.Alicuota));


            return Conexion.ExecuteNonQuery("dbo.venta_detalle_Insert", ParametersList, true, connLocal);
        }



        public static List<DTO.BusinessEntities.VentaDetalleData> getbyID(Guid id, bool connLocal = true)
        {
            List<VentaDetalleData> detalles = new List<VentaDetalleData>();
            VentaDetalleData detalle;
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

        public static List<VentaDetalleData> getbyCodigoInterno(string p)
        {
            List<VentaDetalleData> detalles = new List<VentaDetalleData>();
            VentaDetalleData detalle;
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

        private static VentaDetalleData CargoDetalle(SqlDataReader dataReader)
        {
            VentaDetalleData detalle = new VentaDetalleData();

            detalle.FatherID = new Guid(dataReader["idventa"].ToString());
            detalle.PrecioUnidad = Convert.ToDecimal(dataReader["precio"].ToString());
            detalle.Alicuota = Convert.ToDecimal(dataReader["alicuota"].ToString());
            detalle.Codigo = dataReader["codigo"].ToString();
            detalle.Cantidad = Convert.ToDecimal(dataReader["cantidad"].ToString());
            return detalle;
        }
    }
}
