namespace BusinessComponents
{
    public static class stock
    {

        //public static decimal GetStockTotal(string codigoBarra, Guid idLocal = new Guid())
        //{
        //    stockData aux = stock.obtenerProducto(codigoBarra);
        //    if (idLocal==Guid.Empty)
        //        idLocal = helper.IDLocal;
            
        //    return GetStockTotal(aux.producto.ID, aux.color.ID, aux.talle, idLocal);
        //}
        //public static decimal GetStockTotal(Guid idProducto, Guid idColor, int talle)
        //{
        //    return GetStockTotal(idProducto, idColor, talle, helper.IDLocal);
        //}
        //public static decimal GetStockTotal(stockData s)
        //{

        //    return GetStockTotal(s.producto.ID, s.color.ID, s.talle);

        //}
        //public static decimal GetStockTotal(string codigoInterno, Guid idColor, int talle)
        //{

        //    return 0;//GetStockTotal(producto.getProductoByCodigoInterno(codigoInterno).ID, idColor, talle);
        //}
        //public static decimal GetStockTotal(Guid idProducto, Guid idColor, int talle, Guid idLocal)
        //{
        //    return helper.ConvertToDecimalSeguro(stockDataMapper.getStock(idProducto, idColor, talle, idLocal));
        //}










        //public static List<detalleStockData> getDetalleStock(string codigo)
        //{
        //    return getDetalleStock(codigo, helper.IDLocal);
        //}
        //public static List<detalleStockData> getDetalleStock(string codigo, Guid id)
        //{
        //    return getDetalleStock(codigo, id, null);
        //}




        //public static bool UpdateStock(stockData s, decimal newStock, Guid idLocal = new Guid(), bool add = true)
        //{
        //    decimal aux = 0;

        //    if (idLocal == Guid.Empty)
        //        idLocal = helper.IDLocal;




        //    aux = add ? GetStockTotal(s.producto.ID, s.color.ID, s.talle) + newStock : newStock;

        //    return UpdateStock(s.producto.ID, s.color.ID, s.talle, aux, idLocal);
        //}
        //private static bool UpdateStock(Guid idProducto, Guid idColor, int talle, decimal newStock, Guid idlocal)
        //{

        //    return stockDataMapper.updateStock(idProducto, idColor, talle, idlocal, newStock);
        //}
        //public static bool actualizarStock(string codigo, decimal modifStock, Guid idlocal= new Guid(), bool add=true)
        //{//nuevo movimiento

        //    if (idlocal == Guid.Empty)
        //        idlocal = helper.IDLocal;


        //    stockData s = stock.obtenerProducto(codigo);

        //    if (helper.validarCodigo(s.codigo))
        //    {//Si no es valido no lo intento actualizar
                
            
        //        if (s.stock >= -500)
        //        {
        //            return UpdateStock(s, modifStock, idlocal, add);
        //        }
        //        else
        //        {
        //            return insertStock(s, modifStock, idlocal);
        //        }
        //    }
        //    return true;

        //}







        //public static List<stockData> getAllbyLocalAndProducto(Guid idlocal, Guid idproducto) {

        //    List<stockData> l =  stockDataMapper.getAllbyLocalAndProducto(idlocal, idproducto);
        //   // localData loc = BusinessComponents.Local.getbyID(idlocal);
        //    List<colorData> colores = null;//color.getAll();
        //    //productoData pp = BusinessComponents.producto.getbyID(idproducto);
            
            
        //    foreach (stockData s in l)
        //    {
        //        //s.Local = loc;
        //        s.color = colores.Find(delegate(colorData cc) { return cc.ID == s.color.ID; });
        //        s.producto = null;// pp;

        //    }

        //    return l;

        ////}
        //public static stockData obtenerProducto(string codigoBarras)
        //{
        //    if (helper.talleUnico || codigoBarras.Length==10)
        //    {
        //        codigoBarras= codigoBarras.PadRight(12, '0');
        //    }

        //    productoData produc = new productoData();
        //    stockData articulo = new stockData();
        //    if (helper.consultarArticulo)
        //    {
        //        string art = codigoBarras.Substring(0, 7);
        //        string col = codigoBarras.Substring(7, 3);
        //        string tal = codigoBarras.Substring(10, 2);

        //        produc = null;// producto.getProductoByCodigoInterno(art);


        //        articulo.producto = produc;
        //        articulo.color = null;//color.getByCodigo(col);
        //        if (helper.haymts)
        //        {
        //            articulo.talle = Convert.ToInt32(StockMetros.from61ToDec(tal));
        //            articulo.talle61 = tal;
        //            articulo.metros = StockMetros.obtenerMetrosPorTalle(art, col, articulo.talle);
        //        }
        //        else
        //        {
        //            articulo.talle = Convert.ToInt32(tal);        
        //        }


        //       // articulo.producto.proveedor = (proveedorData)new proveedor().GetByID(articulo.producto.proveedor.ID);
        //        articulo.stock = GetStockTotal(articulo.producto.ID, articulo.color.ID, articulo.talle);
        //    }
        //    return articulo;
        //}
        //public static List<stockData> getAlldistintctProducts(Guid idproducto)
        //{
        //    List<stockData> l =             stockDataMapper.getAllbyProducto(idproducto);

        //    foreach (stockData s in l)
        //    {
        //       // s.Local = BusinessComponents.Local.getbyID(s.Local.ID);
        //        s.color = null;// BusinessComponents.color.getColorByID(s.color.ID);
        //        //s.producto = BusinessComponents.producto.getbyID(s.producto.ID);
                
        //    }


        //    return l;
        //}
        //public static bool insertStock(stockData s, decimal newStock)
        //{
        //    return insertStock(s, newStock, helper.IDLocal);
        //}
        //public static bool insertStock(stockData s, decimal newStock, Guid idlocal)
        //{
        //    return stockDataMapper.insertStock(s.producto.ID, s.color.ID, s.talle, idlocal, newStock);
        //}
        //public static bool setDinamicallyStock(string codigo, Guid idLocal, List<colorData> colores=null)
        //{ 
        //    string codAux = codigo;
        //    if (codigo.Length==4||codigo.Length==7||codigo.Length==10||codigo.Length==12)
        //    {
        //        codAux = codigo + "%";
        //    }

        //    List<detalleStockData> aux = stockDataMapper.getDetalleStock(codAux, idLocal);

        //    if (aux.Count>0)
        //    {
                
            
        //    if (colores==null)
        //    {
        //        colores = null;// color.getAll();
        //        }

        //    foreach (detalleStockData detalle in aux)
        //    {
        //        detalle.color = colores.Find(
        //                                    delegate(colorData c)
        //                                    {
        //                                        return c.codigoInterno == detalle.codigo.Substring(7, 3); 
        //                                    }
        //                                   );

        //        if (detalle.color == null)
        //        {
        //            detalle.color = colores.Find(delegate(colorData c)
        //                                        {
        //                                            return c.codigoInterno == "000";
        //                                        }
        //                                        );
        //        }
        //    }

        //     aux.Sort(delegate(detalleStockData x, detalleStockData y)
        //        {
        //            return x.codigo.CompareTo(y.codigo);
                    
        //        });

        //    int auxTalle;
        //    string auxColor = "";
        //    string auxCodI = "";
        //    decimal auxStock = 0;


        //    auxColor = aux[0].color.codigoInterno;
        //    auxTalle = Convert.ToInt32(aux[0].codigo.Substring(10, 2));
        //    auxCodI = aux[0].codigo.Substring(0,7);
        //    foreach (detalleStockData d in aux)
        //    {
        //        if (d.color.codigoInterno == auxColor && d.codigo.Substring(10, 2) == auxTalle.ToString("00"))
        //        {
        //            auxStock += d.cantidad;
                    
        //        }
        //        else
        //        {// cambia, -> subo datos y recolecto nuevos
        //            return stock.actualizarStock(d.codigo.Substring(0, 7) + auxColor + auxTalle.ToString("00"), auxStock, idLocal, false);

        //            auxColor = d.color.codigoInterno;
        //            auxTalle = Convert.ToInt32(d.codigo.Substring(10, 2));
        //            auxStock = d.cantidad;
        //            auxCodI = d.codigo.Substring(0, 7);
        //        }   
        //    }
        //    return  stock.actualizarStock(auxCodI + auxColor + auxTalle.ToString("00"), auxStock, idLocal, false);
        //    }
        //    return true;
        
        //}
        //public static List<detalleStockData> getDetalleStock(string codigo,Guid idlocal,List<colorData> colores) 
        //{
        //    string codAux = codigo;
        //    if (codigo.Length==4||codigo.Length==7||codigo.Length==10||codigo.Length==12)
        //    {
        //        codAux = codigo + "%";
        //    }

        //    List<detalleStockData> aux = stockDataMapper.getDetalleStock(codAux, idlocal);

        //    if (colores==null)
        //    {
        //        colores = null;//color.getAll();
        //    }
           
        //    foreach (detalleStockData detalle in aux)
        //    {
        //        detalle.color = colores.Find(
        //                                    delegate(colorData c)
        //                                    {
        //                                        return c.codigoInterno == detalle.codigo.Substring(7, 3); ;
        //                                    }
        //                                   );

        //        detalle.producto = null;//producto.getProductoByCodigoInterno(codigo);

        //        if (detalle.producto.proveedor.ID!=null)
        //        {
        //           // detalle.producto.proveedor = (proveedorData)new proveedor().GetByID(detalle.producto.proveedor.ID);
        //        }

        //    }

        //    return aux;

        //}
        //public static List<stockData> GetAll(Guid idlocal,bool onlyEnable = true)
        //{
        //    List<stockData> list =  stockDataMapper.getAll(idlocal);

        //    //List<productoData> productos = producto.getAll(true,true,false);
        //    List<colorData> colores =null;// color.getAll();


            

        //    foreach (stockData s in list)
        //    {
        //        s.color = colores.Find(c => c.ID == s.color.ID);

        //        s.producto = null;// productos.FirstOrDefault(p => p.ID == s.producto.ID);

        //    }

        //    if (onlyEnable)
        //        list = list.FindAll(x => x.producto.enable);
            
           


        //    list.Sort(delegate(stockData x, stockData y)
        //    {
        //        return x.producto.proveedor.razonSocial.CompareTo(y.producto.proveedor.razonSocial);

        //    });

        //    return list;
        ////}
        //public static object getCodigoBarra(stockData item)
        //{
        //    return item.producto.codigoInterno + item.color.codigoInterno + item.talle.ToString("00");
        //}
        //public static string getCodigoBarraDinamico(productoData productoData, colorData colorData, string talle)
        //{
        //    if (talle=="")
        //    {
        //        talle = "0";
        //    }
        //    if (helper.haymts)
        //    {
        //        return StockMetros.obtenerCodigo(productoData.codigoInterno, colorData.codigoInterno, talle);//le mando los metros
                
        //    }
        //    else
        //    {
        //        return productoData.codigoInterno + colorData.codigoInterno + talle.PadLeft(2,'0');
        //    }

            
        //}
        //public static List<stockData> search(List<stockData> list, string filtro)
        //{
        //    return list.FindAll(
        //        data =>
        //            data.producto.Description.ToLower().Contains(filtro.ToLower()) ||
        //            data.producto.proveedor.razonSocial.ToLower().Contains(filtro.ToLower()) ||
        //            data.producto.codigoProveedor.ToLower().Contains(filtro.ToLower()));

        //}
    }

   
}
