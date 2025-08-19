namespace Persistence
{
    public static class listaPrecioDataMapper
    {
        //public static List<DTO.BusinessEntities.listaPrecioData> getAll(bool connLocal)
        //{
        //    listaPrecioData lista;
        //    List<listaPrecioData> listas = new List<listaPrecioData>();



        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.lista_precio_SelectAll", null, connLocal);
        //    while (dataReader.Read())
        //    {

        //        lista = new listaPrecioData();
        //        lista.ID = new Guid(dataReader["id"].ToString());
        //        lista.Description= dataReader["descripcion"].ToString();
        //        try
        //        {
        //            lista.enable = Convert.ToBoolean(dataReader["enable"].ToString());

        //        }
        //        catch (Exception)
        //        {
        //            lista.enable = true;
        //        }
                

        //        listas.Add(lista);



                
        //    }
        //    dataReader.Close();




        //    return listas;
            
            
        //}


        //public static bool insertLista(listaPrecioData lista,bool connLocal)
        //{

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();

            
        //    ParametersList.Add(new SqlParameter("@id", lista.ID));
        //    ParametersList.Add(new SqlParameter("@descripcion", lista.Description));
        //    ParametersList.Add(new SqlParameter("@enable", lista.enable));



        //    return Conexion.ExecuteNonQuery("dbo.lista_precio_Insert", ParametersList, true, connLocal);


        //}


        //public static bool update(listaPrecioData lista,bool connLocal)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();

            
        //    ParametersList.Add(new SqlParameter("@id", lista.ID));
        //    ParametersList.Add(new SqlParameter("@descripcion", lista.Description));
        //    ParametersList.Add(new SqlParameter("@enable", lista.enable));


        //    return Conexion.ExecuteNonQuery("dbo.lista_precio_Update", ParametersList, true, connLocal);
        //}

        //public static bool delete(Guid guid)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();


        //    ParametersList.Add(new SqlParameter("@id", guid));

        //    return Conexion.ExecuteNonQuery("dbo.lista_precio_disable", ParametersList);
        //}
    }
}
