namespace BusinessComponents
{
    public static class producto
    {

        //public static string generarCodigoInterno(string codigoProveedor)
        //{

        //    string lastcode = BusinessComponents.producto.getLastCode(codigoProveedor).Substring(4);

        //    int aux = 0;
        //    if (lastcode != "")
        //    {
        //        aux = Convert.ToInt32(lastcode);
        //    }
        //    aux++;

        //    return codigoProveedor+ aux.ToString("000");



        //}
        //public static productoData getProductoByCodigoInterno(string codigo)
        //{
        //    productoData p = new productoData();
        //    if (helper.consultarArticulo)
        //    {
        //        p = productoDataMapper.getProductoByCodigoInterno(codigo.Substring(0, 7));

        //    }
        //    return p;
        //}

        //public static List<productoData> getAll(bool getProveedor,bool connLocal = true,bool onlyEnable=true)
        //{
        //    List<productoData> ps = productoDataMapper.getAll(connLocal);

        //    if (onlyEnable)
        //        ps = ps.FindAll(delegate(productoData data) { return data.enable; });

        //    if (getProveedor && ps!=null && ps.Count>0)
        //    {
        //        foreach (productoData p in ps)
        //        {
        //         //   p.proveedor = (proveedorData)new proveedor().GetByID(p.proveedor.ID);
        //        }
        //    }

        //    return ps;

        //}

        //public static List<productoData> getAll()
        //{

        //    return getAll(false);

        //}

        //public static string getLastCode(string codigoProveedor)
        //{

        //    List<productoData> ps = productoDataMapper.getProductosByProveedor(codigoProveedor);


        //    if (ps.Count > 0)
        //    {
        //        ps.Sort(delegate(productoData x, productoData y)
        //        {
        //            return x.codigoInterno.CompareTo(y.codigoInterno);

        //        });

        //        return ps[ps.Count - 1].codigoInterno;
        //    }
        //    else
        //    {
        //        return "0000000";
        //    }


        //}

        //public static bool delete(Guid idproducto)
        //{
        //    return productoDataMapper.disable(idproducto);
        //}


        //public static productoData getbyID(Guid guid)
        //{

        //    productoData aux = productoDataMapper.getbyID(guid);

        //    if (aux.temporada.ID != Guid.Empty)
        //    {
                
        //        //aux.temporada = temporada.getByID(aux.temporada.ID);
        //    }

        //    if (aux.linea.ID != Guid.Empty)
        //    {
        //        //aux.linea = linea.getLineaByID(aux.linea.ID);
        //    }
        //   // aux.proveedor = (proveedorData)new proveedor().GetByID(aux.proveedor.ID);
               


        //    return aux;
        //}

        //public static List<productoData> getbyProveedor(Guid guid,bool onlyEnable = true)
        //{

        //    List<productoData> ps = productoDataMapper.getProductosByProveedor(guid);
        //    proveedorData proveedor = null;//(proveedorData)new proveedor().GetByID(guid);
            

        //    if (onlyEnable)
        //        ps = ps.FindAll(delegate(productoData data) { return data.enable; });


        //    foreach (productoData p in ps)
        //       p.proveedor = proveedor;
            
           
        //    ps.Sort((x, y) => System.String.Compare(x.codigoProveedor, y.codigoProveedor, System.StringComparison.Ordinal));


        //    return ps;
        //}

        //public static bool insert(productoData p)
        //{
        //    if (p.ID==Guid.Empty)
        //    {
        //        p.ID = Guid.NewGuid();
        //    }
        //    return insert(p, true);
        //}
        //public static bool insert(productoData p, bool insertTalles,bool connLocal = true)
        //{
        //    //creo el producto



        //    if (insertTalles)
        //    {
        //        productoTalleData ptalle;
        //        int aux = 50;
        //        if (helper.talleUnico)
        //        {
        //            aux = 0;
        //        }
        //        if (helper.haymts)
        //        {
        //            aux = 121;
        //        }

        //        for (int i = 0; i <= aux; i++)
        //        {
        //            ptalle = new productoTalleData();
        //            ptalle.IDproducto = p.ID;
        //            ptalle.talle = i;
        //            ptalle.ID = Guid.NewGuid();
        //            //BusinessComponents.productoTalle.Insert(ptalle,connLocal);
        //        }
        //    }

        //    return productoDataMapper.insert(p,connLocal);



        //}

        //public static bool update(productoData p, bool connLocal = true)
        //{
        //    return productoDataMapper.update(p,connLocal);
        //}



        //public static bool undelete(Guid guid)
        //{
        //    return productoDataMapper.enable(guid);
        //}

        //public static List<productoData> search(List<productoData> todos, string p,bool onlyEnable = true)
        //{
        //     List<productoData> aux = todos.FindAll(
        //        pp =>
        //            pp.descripcion.ToLower().Contains(p.ToLower()) ||
        //            pp.proveedor.razonSocial.ToLower().Contains(p.ToLower()) ||
        //            pp.codigoProveedor.ToLower().Contains(p.ToLower())
        //        );

        //    if (onlyEnable)
        //        aux = aux.FindAll(pp => pp.enable);
            

        //    return aux;
        //}
    }
}
