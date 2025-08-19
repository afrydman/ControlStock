namespace Persistence
{
    public  class clienteDataMapper
    {

        //public static List<DTO.BusinessEntities.personaData> getAll(bool connLocal = true)
        //{

        //    clienteData p;

        //    List<personaData> ps = new List<personaData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();

        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.clientes_SelectAll", null,connLocal);
        //    if (dataReader != null)
        //    {
        //        while (dataReader.Read())
        //        {
        //            p = cargoCliente(dataReader);
        //            ps.Add(p);
        //        }
        //        dataReader.Close();
        //    }


        //    return ps;
        //}

        //private static clienteData cargoCliente(SqlDataReader dataReader)
        //{
        //    clienteData p = new clienteData();

        //    p.ID = new Guid(dataReader["id"].ToString());

        //    p.cuil = dataReader["cuil"].ToString();

        //    p.dir = dataReader["direccion"].ToString();
        //    p.email = dataReader["email"].ToString();
        //    p.enable = Convert.ToBoolean(dataReader["enable"].ToString());
        //    p.facebook = dataReader["facebook"].ToString();

        //    p.nombrecontacto = dataReader["nombre"].ToString();
        //    p.observaciones = dataReader["descripcion"].ToString();
        //    p.razonSocial = dataReader["razonsocial"].ToString();
        //    p.tel = dataReader["telefono"].ToString();

        //    return p;
        //}

        //public static DTO.BusinessEntities.clienteData getByID(Guid idp, bool connLocal = true)
        //{

        //    clienteData p = new clienteData();

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idp));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.clientes_SelectRow", ParametersList,connLocal);
        //    if (dataReader != null)
        //    {
        //        while (dataReader.Read())
        //        {
        //            p = cargoCliente(dataReader);
        //        }
        //        dataReader.Close();

        //    }


        //    return p;
        //}


        //public static bool disable(Guid idp) {

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idp));
        //    return Conexion.ExecuteNonQuery("dbo.clientes_Disable", ParametersList);
        //}
        //public static bool insert(DTO.BusinessEntities.personaData p, bool connLocal = true)
        //{
        //    List<SqlParameter> ParametersList = getParameterList(p);
           
        
        //    return Conexion.ExecuteNonQuery("dbo.clientes_Insert", ParametersList,true,connLocal);

        //}

        //internal static List<SqlParameter> getParameterList(personaData p)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", p.ID));
        //    ParametersList.Add(new SqlParameter("@nombre", p.nombrecontacto));
        //    ParametersList.Add(new SqlParameter("@cuil", p.cuil));
        //    ParametersList.Add(new SqlParameter("@telefono", p.tel));
        //    ParametersList.Add(new SqlParameter("@descripcion", p.observaciones));
        //    ParametersList.Add(new SqlParameter("@razonsocial", p.razonSocial));
        //    ParametersList.Add(new SqlParameter("@email", p.email));
        //    ParametersList.Add(new SqlParameter("@facebook", p.facebook));
        //    ParametersList.Add(new SqlParameter("@direccion", p.dir));
        //    return ParametersList;
        //}

        //public static bool update(personaData p, bool connLocal = true)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", p.ID));
        //    ParametersList.Add(new SqlParameter("@nombre", p.nombrecontacto));
        //    ParametersList.Add(new SqlParameter("@cuil", p.cuil));
        //    ParametersList.Add(new SqlParameter("@telefono", p.tel));
        //    ParametersList.Add(new SqlParameter("@descripcion", p.observaciones));
        //    ParametersList.Add(new SqlParameter("@razonsocial", p.razonSocial));
        //    ParametersList.Add(new SqlParameter("@email", p.email));
        //    ParametersList.Add(new SqlParameter("@facebook", p.facebook));
        //    ParametersList.Add(new SqlParameter("@direccion", p.dir));
            

        //    ParametersList.Add(new SqlParameter("@enable", p.enable));




        //    return Conexion.ExecuteNonQuery("dbo.clientes_update", ParametersList,true, connLocal);
        //}
    }
}
