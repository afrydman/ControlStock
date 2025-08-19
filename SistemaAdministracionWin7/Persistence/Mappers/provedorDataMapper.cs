namespace Persistence
{
    public static class provedorDataMapper
    {
        //public static List<DTO.BusinessEntities.personaData> getAll(bool connLocal = true)
        //{

        //    proveedorData p;

        //    List<personaData> ps = new List<personaData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();

        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.proveedores_SelectAll", null, connLocal);
        //    if (dataReader != null)
        //    {


        //        while (dataReader.Read())
        //        {
        //            p = cargoProveedor(dataReader);

        //            ps.Add(p);


        //        }
        //    }

        //    if(dataReader!=null)
        //        dataReader.Close();




        //    return ps;
        //}

        //private static proveedorData cargoProveedor(SqlDataReader dataReader)
        //{
        //    proveedorData p = new proveedorData();

        //            p.ID = new Guid(dataReader["id"].ToString());
        //            p.codigo = dataReader["codigo"].ToString();
        //            p.cuil = dataReader["cuil"].ToString();
        //            p.descuento = dataReader["descuento"].ToString();
        //            p.dir = dataReader["direccion"].ToString();
        //            p.email = dataReader["email"].ToString();
        //            p.enable = Convert.ToBoolean(dataReader["enable"].ToString());
        //            p.facebook = dataReader["facebook"].ToString();
        //            p.ingresoB = dataReader["ingresobruto"].ToString();
        //            p.nombrecontacto = dataReader["nombre"].ToString();
        //            p.observaciones = dataReader["descripcion"].ToString();
        //            p.razonSocial = dataReader["razonsocial"].ToString();
        //            p.tel = dataReader["telefono"].ToString();

        //    return p;
        //}

        //public static DTO.BusinessEntities.personaData getbyCodigo(string idp)
        //{

            
        //    proveedorData p = new proveedorData();
            

        //    List<personaData> todos = getAll(true);

        //    return todos.Find(delegate(personaData data) { return data.codigo == idp; });

        //    //hola me llamo ale and welcome to jackass




        //    //List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    //ParametersList.Add(new SqlParameter("@codigo", idp));
        //    //SqlDataReader dataReader = Conexion.ExcuteReader("dbo.proveedores_Selectbycodigo", ParametersList);

        //    //while (dataReader.Read())
        //    //{


        //    //    p.ID = new Guid(dataReader["id"].ToString());
        //    //    p.codigo = dataReader["codigo"].ToString();
        //    //    p.cuil = dataReader["cuil"].ToString();
        //    //    p.descuento = dataReader["descuento"].ToString();
        //    //    p.dir = dataReader["direccion"].ToString();
        //    //    p.email = dataReader["email"].ToString();
        //    //    p.enable = Convert.ToBoolean(dataReader["enable"].ToString());
        //    //    p.facebook = dataReader["facebook"].ToString();
        //    //    p.ingresoB = dataReader["ingresobruto"].ToString();
        //    //    p.nombrecontacto = dataReader["nombre"].ToString();
        //    //    p.obs = dataReader["descripcion"].ToString();
        //    //    p.razonSocial = dataReader["razonsocial"].ToString();
        //    //    p.tel = dataReader["telefono"].ToString();


        //    //}
        //    //dataReader.Close();




        //    //return p;
        //}
        //public static DTO.BusinessEntities.personaData getByID(Guid idp, bool Local = true)
        //{

        //    proveedorData p = new proveedorData();
        //    p.ID = idp;
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idp));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.proveedores_SelectRow", ParametersList,Local);
        //    while (dataReader.Read())
        //    {

        //        p = cargoProveedor(dataReader);

                
        //    }
        //    dataReader.Close();

        //    return p;
        //}


        //public static bool disable(Guid idp) {
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idp));
        //    return Conexion.ExecuteNonQuery("dbo.proveedores_Disable", ParametersList);
        //}
        //public static bool insert(DTO.BusinessEntities.personaData p, bool connLocal = true)
        //{
        //    List<SqlParameter> ParametersList = getParameterList(p);
           

        //    return Conexion.ExecuteNonQuery("dbo.proveedores_Insert", ParametersList, true, connLocal);

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
        //    ParametersList.Add(new SqlParameter("@codigo", p.codigo));
        //    ParametersList.Add(new SqlParameter("@descuento", p.descuento));
        //    ParametersList.Add(new SqlParameter("@ingresobruto", p.ingresoB));
        //    ParametersList.Add(new SqlParameter("@enable", p.enable));
        //    return ParametersList;
        //}

        //public static bool update(personaData p, bool connLocal = true)
        //{
        //    List<SqlParameter> ParametersList = getParameterList(p);

        //    return Conexion.ExecuteNonQuery("dbo.proveedores_update", ParametersList, true, connLocal);
        //}
    }
}
