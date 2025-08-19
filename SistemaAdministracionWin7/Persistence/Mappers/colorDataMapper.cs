namespace Persistence
{
    public class colorDataMapper
    {
        //public static colorData getColorByID(Guid idColor)
        //{
        //    colorData c = new colorData();

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idColor));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.colores_SelectRow", ParametersList);

        //    while (dataReader.Read())
        //    {


        //        c.ID = new Guid(dataReader["id"].ToString()); 
        //        c.Description = dataReader["descripcion"].ToString(); 
        //        c.codigoInterno = dataReader["codigo"].ToString();
        //        c.enable = Convert.ToBoolean(dataReader["enable"].ToString()); 


        //    }
        //    dataReader.Close();




        //    return c;
        //}

        //public static bool insert(DTO.BusinessEntities.colorData ncolor, bool connLocal = true)
        //{
            
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", ncolor.ID));
        //    ParametersList.Add(new SqlParameter("@descripcion", ncolor.Description));
        //    ParametersList.Add(new SqlParameter("@codigo", ncolor.codigoInterno));
        //    ParametersList.Add(new SqlParameter("@enable", ncolor.enable));

        //    return Conexion.ExecuteNonQuery("dbo.colores_Insert", ParametersList, true,connLocal);
        //}

        //public static List<DTO.BusinessEntities.colorData> getAll(bool connLocal=true)
        //{



        //    colorData c; 
        //    List<colorData> cs = new List<colorData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();

        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.colores_SelectAll", null, connLocal);


        //    while (dataReader.Read())
        //    {

        //        c= new colorData();
        //        c.ID = new Guid(dataReader["id"].ToString());
        //        c.Description = dataReader["descripcion"].ToString();
        //        c.codigoInterno = dataReader["codigo"].ToString();
        //        c.enable = Convert.ToBoolean(dataReader["enable"].ToString());

        //        cs.Add(c);

        //    }
        //    dataReader.Close();




        //    return cs;






        //}

        //public static bool disable(Guid idC)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idC));
        //    return Conexion.ExecuteNonQuery("dbo.color_Disable", ParametersList);
        //}

        //public static colorData getColorByCodigo(string col)
        //{
        //    colorData c = new colorData();

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@codigo", col));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.colores_SelectbyCodigo", ParametersList);
        //    while (dataReader.Read())
        //    {


        //        c.ID = new Guid(dataReader["id"].ToString());
        //        c.Description = dataReader["descripcion"].ToString();
        //        c.codigoInterno = dataReader["codigo"].ToString();
        //        c.enable = Convert.ToBoolean(dataReader["enable"].ToString());


        //    }
        //    dataReader.Close();




        //    return c;
        //}
    }
}
