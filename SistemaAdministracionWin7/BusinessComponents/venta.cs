namespace BusinessComponents
{
    public static class venta
    {
        //public static List<ventaDetalle> GetAll()
        //{
        //    return null;
        //}
       public  enum impresiones { factura,remito };

       //public static string getLastNumber(Guid idLocal)
       // {
       //     return getNumero(idLocal, helper.firstNum, false);
        
       // }

       //public static int getLastNumber(Guid id, int myprefix,bool connLocal = true)
       //{
       //    ventaData aux = ventaDataMapper.getLast(id, myprefix, connLocal);
       //    return aux.Numero;
       //}
       // public static string getLastNumber(int myprefix)
       // {
       //     return getNumero(helper.IDLocal, myprefix, false);
       // }
       // public static string getLastNumber()
       // {
       //     return getNumero(helper.IDLocal, helper.firstNum, true); ;
       // }
       
       // public static string getNumero(Guid id,int myprefix,bool completo)
       // {

       //     ventaData aux = ventaDataMapper.getLast(id,myprefix);
       //     aux.Numero++;
       //     if (completo)
       //     {
       //         return aux.Show;
       //     }else
       //     {
       //         return aux.Numero.ToString();
       //     }

          

       // }


        //public static bool insertVenta(DTO.BusinessEntities.ventaData nuevaVenta,bool updateStock,bool connLocal = true)
        //{
        //     Guid id;
        //     bool task = false;
        //     var opts = new TransactionOptions
        //            {
        //                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        //            };

        //    using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
        //    {
               


        //        try
        //        {
        //            conexion.closeConecction(connLocal); //Para que se vuelva a abrir dentro de la trans!


        //            if (nuevaVenta.ID == new Guid())
        //            {
        //                id = Guid.NewGuid();
        //                nuevaVenta.ID = id;
        //            }
        //            else
        //            {
        //                id = nuevaVenta.ID;
        //            }
        //            foreach (pagoData f in nuevaVenta.pagos)
        //            {

        //                if (f.ID == new Guid())
        //                {
        //                    f.ID = Guid.NewGuid();
        //                }
        //                task=ventaDataMapper.insertarFormasPago(f, id,connLocal);
        //                if (!task) return false;

        //            }



        //            //
        //            // !!!! no poner nada con venta.escambio pq la cantidad ya la guarda ( negativo si es cambio)!!!!
        //            //
        //            foreach (ventaDetalleData d in nuevaVenta.detalles)
        //            {
        //                task=ventaDetalle.insertDetalle(d,connLocal);
        //                if (!task) return false;
                        
        //                if (updateStock)
        //                    task = stock.actualizarStock(d.codigo, -1 * d.cantidad, helper.IDLocal, true);    
                        
                        
        //                if (!task) return false;
        //            }
        //            task = ventaDataMapper.insertVenta(nuevaVenta,connLocal);
        //            if(task)trans.Complete();
        //        }
        //        catch (Exception)
        //        {
        //            return false;

        //        }
        //    }
        //    return task;


        //}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="fechaAyer"></param>
        /// <param name="fechaMan"></param>
        /// <param name="cambio"></param>
        /// <param name="idLocal"></param>
        /// <returns></returns>

        //public static List<DTO.BusinessEntities.ventaData> getbyfecha(DateTime fecha,
        //    bool cambio = false, Guid idLocal = new Guid())
        //{
        //    return getbyRangofecha(fecha.Date.AddDays(-1).AddHours(23).AddMinutes(59), fecha.Date.AddHours(23).AddMinutes(59), cambio, idLocal);
        //}

        //public static List<DTO.BusinessEntities.ventaData> getbyRangofecha(DateTime fechaDesde, DateTime fechaHasta, bool cambio = false,Guid idLocal = new Guid())
        //{

        //    if (idLocal == Guid.Empty)
        //        idLocal = helper.IDLocal;


        //    fechaDesde = fechaDesde.Date;
        //    fechaHasta = fechaHasta.Date.AddHours(23).AddMinutes(59);

        //    List<DTO.BusinessEntities.ventaData> ventas = ventaDataMapper.getByFecha(fechaDesde, fechaHasta, cambio, idLocal);


        //    foreach (ventaData v in ventas)
        //    {//cargo los pagos
        //        v.pagos = obtenerPagos(v.ID);
        //       // v.tercero =  new cliente().GetByID(v.tercero.ID);
        //    }

        //    return ventas;
        
        //}


        //public static List<pagoData> obtenerPagos(Guid guid, bool connLocal = true)
        //{
        //    return ventaDataMapper.obtenerPagos(guid,connLocal);
        //}

        //public static double calcularTotal(double subtotal, double descuento,double aumento)
        //{


        //    double aux = subtotal + ((subtotal * aumento) / 100);
        //    return aux - ((aux * descuento) / 100);

            
            
        //}


        //public static void imprimir(impresiones impresionType,Guid id)
        //{

            
        //    string DA_REPORT = "";

        //    if (helper.esCliente(GrupoCliente.Opiparo))
        //    {
        //        if (impresionType==impresiones.factura)
        //        {
        //            DA_REPORT = @"\Reportes\opiparo_factura.rpt";
        //        }
               
        //        if (impresionType == impresiones.remito)
        //        {
        //            DA_REPORT = @"\Reportes\opiparo_remito.rpt";
        //        }
        //    }


        //    ReportDocument cryRpt = new ReportDocument();
        //    cryRpt.Load(Application.StartupPath + DA_REPORT);
        //    ConnectionInfo crConnectionInfo = new ConnectionInfo();
        //    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        //    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();

        //    Stack<string> param = helper.getParameters();
        //    crConnectionInfo.Password = param.Pop();
        //    crConnectionInfo.UserID = param.Pop();
        //    crConnectionInfo.DatabaseName = param.Pop();
        //    crConnectionInfo.ServerName = param.Pop();
        //    Tables CrTables;

        //    CrTables = cryRpt.Database.Tables;
        //    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
        //    {
        //        crtableLogoninfo = CrTable.LogOnInfo;
        //        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
        //        CrTable.ApplyLogOnInfo(crtableLogoninfo);
        //    }

        //    ParameterFieldDefinitions crParameterFieldDefinitions;
        //    ParameterFieldDefinition crParameterFieldDefinition;
        //    ParameterValues crParameterValues = new ParameterValues();
        //    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

        //    crParameterDiscreteValue.Value = id.ToString();
        //    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
        //    crParameterFieldDefinition = crParameterFieldDefinitions["@id"];
        //    crParameterValues = crParameterFieldDefinition.CurrentValues;


        //    crParameterValues.Clear();
        //    crParameterValues.Add(crParameterDiscreteValue);
        //    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);


        //    settingPositions(DA_REPORT, cryRpt);








           

        //    cryRpt.PrintToPrinter(1, false, 0, 0);
        //    //crystalReportViewer1.Refresh(); 
        
        //}

        //private static void settingPositions(string DA_REPORT, ReportDocument cryRpt)
        //{
        //    int auxMargenRemito = 200;

        //    if (DA_REPORT == @"\Reportes\opiparo_factura.rpt")
        //    {
        //        #region fields positions

        //        cryRpt.ReportDefinition.ReportObjects["fecha1"].Top = 1767;
        //        cryRpt.ReportDefinition.ReportObjects["fecha1"].Left = 7654;

        //        cryRpt.ReportDefinition.ReportObjects["razonsocial1"].Top = 3468;
        //        cryRpt.ReportDefinition.ReportObjects["razonsocial1"].Left = 1984;

        //        cryRpt.ReportDefinition.ReportObjects["direccion1"].Top = 4035;
        //        cryRpt.ReportDefinition.ReportObjects["direccion1"].Left = 1984;

        //        cryRpt.ReportDefinition.ReportObjects["condicioniva"].Top = 4969;
        //        cryRpt.ReportDefinition.ReportObjects["condicioniva"].Left = 2834;


        //        cryRpt.ReportDefinition.ReportObjects["condicionventa"].Top = 5152;
        //        cryRpt.ReportDefinition.ReportObjects["condicionventa"].Left = 2834;

        //        cryRpt.ReportDefinition.ReportObjects["cuil1"].Top = 4969;
        //        cryRpt.ReportDefinition.ReportObjects["cuil1"].Left = 9070;

        //        cryRpt.ReportDefinition.ReportObjects["cantidad1"].Top = 120;
        //        cryRpt.ReportDefinition.ReportObjects["cantidad1"].Left = 300;

        //        cryRpt.ReportDefinition.ReportObjects["codigo1"].Top = 120;
        //        cryRpt.ReportDefinition.ReportObjects["codigo1"].Left = 2851;


        //        cryRpt.ReportDefinition.ReportObjects["codigoproveedor1"].Top = 120;
        //        cryRpt.ReportDefinition.ReportObjects["codigoproveedor1"].Left = 4040;

        //        cryRpt.ReportDefinition.ReportObjects["productodescripcion1"].Top = 120;
        //        cryRpt.ReportDefinition.ReportObjects["productodescripcion1"].Left = 4835;

        //        cryRpt.ReportDefinition.ReportObjects["colordescripcion1"].Top = 120;
        //        cryRpt.ReportDefinition.ReportObjects["colordescripcion1"].Left = 6800;

        //        cryRpt.ReportDefinition.ReportObjects["precio1"].Top = 120;
        //        cryRpt.ReportDefinition.ReportObjects["precio1"].Left = 8203;

        //        cryRpt.ReportDefinition.ReportObjects["subt1"].Top = 120;
        //        cryRpt.ReportDefinition.ReportObjects["subt1"].Left = 10204;



        //        //footer
        //        cryRpt.ReportDefinition.ReportObjects["subtotal1"].Top = 50;
        //        cryRpt.ReportDefinition.ReportObjects["subtotal1"].Left = 10204;


        //        cryRpt.ReportDefinition.ReportObjects["iva1"].Top = 1120;
        //        cryRpt.ReportDefinition.ReportObjects["iva1"].Left = 7000;


        //        cryRpt.ReportDefinition.ReportObjects["total1"].Top = 1120;
        //        cryRpt.ReportDefinition.ReportObjects["total1"].Left = 10204;

        //        cryRpt.ReportDefinition.ReportObjects["subtotal2"].Top = 1120;
        //        cryRpt.ReportDefinition.ReportObjects["subtotal2"].Left = 0;

        //        cryRpt.ReportDefinition.ReportObjects["expreso1"].Top = 1720;
        //        cryRpt.ReportDefinition.ReportObjects["expreso1"].Left = 0;


        //        cryRpt.ReportDefinition.ReportObjects["obs1"].Top = 2220;
        //        cryRpt.ReportDefinition.ReportObjects["obs1"].Left = 800;





        //        cryRpt.ReportDefinition.Sections["Section1"].Height = 0;

        //        cryRpt.ReportDefinition.Sections["Section2"].Height += 500;

        //        cryRpt.ReportDefinition.Sections["Section5"].Height = 3000;



        //        #endregion
        //    }
            
            
        //    if (DA_REPORT == @"\Reportes\opiparo_remito.rpt")
        //    {
        //        #region fields positions


        //        //header

        //        cryRpt.ReportDefinition.ReportObjects["fecha1"].Top = 1767 + auxMargenRemito;
        //        cryRpt.ReportDefinition.ReportObjects["fecha1"].Left = 7654;

        //        cryRpt.ReportDefinition.ReportObjects["razonsocial1"].Top = 3468 + auxMargenRemito;
        //        cryRpt.ReportDefinition.ReportObjects["razonsocial1"].Left = 1984;

        //        cryRpt.ReportDefinition.ReportObjects["direccion1"].Top = 4035 + auxMargenRemito;
        //        cryRpt.ReportDefinition.ReportObjects["direccion1"].Left = 1984;

        //        cryRpt.ReportDefinition.ReportObjects["condicioniva"].Top = 4969 + auxMargenRemito;
        //        cryRpt.ReportDefinition.ReportObjects["condicioniva"].Left = 2834;


        //        cryRpt.ReportDefinition.ReportObjects["condicionventa"].Top = 5152 + auxMargenRemito;
        //        cryRpt.ReportDefinition.ReportObjects["condicionventa"].Left = 2834;

        //        cryRpt.ReportDefinition.ReportObjects["cuil1"].Top = 4969 + auxMargenRemito;
        //        cryRpt.ReportDefinition.ReportObjects["cuil1"].Left = 9070;

        //        //description

        //        cryRpt.ReportDefinition.ReportObjects["cantidad1"].Top = 120  ;
        //        cryRpt.ReportDefinition.ReportObjects["cantidad1"].Left = 300;

        //        cryRpt.ReportDefinition.ReportObjects["codigo1"].Top = 120  ;
        //        cryRpt.ReportDefinition.ReportObjects["codigo1"].Left = 2851;


        //        cryRpt.ReportDefinition.ReportObjects["codigoproveedor1"].Top = 120 ;
        //        cryRpt.ReportDefinition.ReportObjects["codigoproveedor1"].Left = 4240;

        //        cryRpt.ReportDefinition.ReportObjects["productodescripcion1"].Top = 120 ;
        //        cryRpt.ReportDefinition.ReportObjects["productodescripcion1"].Left = 5035;

        //        cryRpt.ReportDefinition.ReportObjects["colordescripcion1"].Top = 120 ;
        //        cryRpt.ReportDefinition.ReportObjects["colordescripcion1"].Left = 6800;




        //        //footer


        //        //cryRpt.ReportDefinition.ReportObjects["total1"].Top = 1120;
        //        //cryRpt.ReportDefinition.ReportObjects["total1"].Left = 2851;


        //        //cryRpt.ReportDefinition.ReportObjects["propio"].Top = 1720;
        //        //cryRpt.ReportDefinition.ReportObjects["propio"].Left = 0;

        //        //cryRpt.ReportDefinition.ReportObjects["dir"].Top = 1720;
        //        //cryRpt.ReportDefinition.ReportObjects["dir"].Left = 6000;


        //        //cryRpt.ReportDefinition.ReportObjects["obs1"].Top = 2220;
        //        //cryRpt.ReportDefinition.ReportObjects["obs1"].Left = 800;





        //        cryRpt.ReportDefinition.Sections["Section1"].Height = 0;

        //        cryRpt.ReportDefinition.Sections["Section2"].Height += 950;

        //        cryRpt.ReportDefinition.Sections["Section5"].Height = 3000;



        //        #endregion
        //    }

        //}

       


        //public static ventaData getByFactura(string num,int Prefix)
        //{
        //    ventaData v = ventaDataMapper.getbyFactura(num,Prefix);
        //    List<ventaDetalleData> vd = ventaDetalle.getById(v.ID);
        //    v.detalles = vd;


        //    v.tercero =  new cliente().GetByID(v.tercero.ID);
        //    v.pagos = obtenerPagos(v.ID);
          


        //    return v;
            
        //}


        //public static bool anularVenta(ventaData v, bool updateStock, bool connLocal = true)
        //{

        //      bool task = false;
        //     var opts = new TransactionOptions
        //            {
        //                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        //            };

        //    using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
        //    {
              
        //        try
        //        {
        //            conexion.closeConecction(connLocal); //Para que se vuelva a abrir dentro de la trans!

        //            if (updateStock)
        //            {
        //                foreach (ventaDetalleData vd in v.detalles)
        //                {
        //                    task=stock.actualizarStock(vd.codigo, vd.cantidad, v.Local.ID);
        //                    if (!task) return false;
        //                }
        //            }
        //            task = anuloVenta(v.ID, connLocal);
        //            if(task)trans.Complete();

        //        }
        //        catch (Exception)
        //        {

        //            return false;
        //        }
        //    }


        //    return task;
        
        //}

        //public static bool anuloVenta(Guid id,bool connLocal)
        //{

        //    return ventaDataMapper.anularVenta(id,connLocal);
        //}




        //public static ventaData getByID(Guid id, bool connLocal = true)
        //{
        //    ventaData v = ventaDataMapper.getbyID(id, connLocal);

        //    List<ventaDetalleData> vd = ventaDetalle.getById(v.ID, connLocal);
        //    v.detalles = vd;
        //    v.tercero = new cliente().GetByID(v.tercero.ID, connLocal);

        //    v.pagos = obtenerPagos(v.ID, connLocal);


        //    return v;
        //}

     



        //public static List<ventaData> getBiggerThan(int ultimaVenta,Guid idLocal,int Prefix,bool connLocal = true)
        //{
        //    List<ventaData> ventas = ventaDataMapper.getBiggerThan(ultimaVenta,  idLocal,Prefix,connLocal);

        //    List<ventaData> ventas2 = new List<ventaData>();
        //    foreach (ventaData v in ventas)
        //    {//las trae completa, detalles formas de pago y cliente
        //        ventas2.Add(getByID(v.ID,connLocal));
                
        //    }

        //    return ventas2;
        //}

        //public static List<ventaData> getModified(Guid idLocal, bool connLocal = true)
        //{
        //    List<ventaData> ventas = ventaDataMapper.getModified( idLocal,connLocal);

        //    //foreach (ventaData v in ventas)
        //    //{
        //    //    v.formasdepago = obtenerPagos(v.ID);
        //    //    v.detalles = getByID(v.ID).detalles;
        //    //    v.tercero =  new cliente().GetByID(v.tercero.ID);
        //    //}
            

        //    return ventas;

        //}

        //public static bool yaviqueestabasmodificadamacho(Guid idLocal,bool connLocal = true)
        //{
        //    return ventaDataMapper.yaviqueestabasmodificadamacho(idLocal, connLocal);
        //}



        //public static decimal calcularTotal(List<pagoData> pagos)
        //{
        //    decimal total = 0;
        //    foreach (pagoData fp in pagos)
        //    {
        //        total += (fp.importe + (fp.importe * fp.aumento / 100));
                    
        //    }
        //    return total;
        //}

        //public static List<ventaData> getCuentaCorrientebyCliente(Guid idCliente)
        //{


        //    return ventaDataMapper.getVentasPagadasConCC(idCliente,helper.IDCC);
        //}

    



        //public static ventaData getLast()
        //{
        //    return ventaDataMapper.get

        //}
        /// <summary>
        /// solo va a funcionar para 2 Prefix por Local
        /// </summary>
        /// <param name="idLocal"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        //public static int getLastNumberOther(Guid idLocal, int p)
        //{
        //    return ventaDataMapper.getLastNumberOther(idLocal, p);
        //}

        //public static List<ventaData> getbyCliente(Guid guid)
        //{
        //    List<ventaData> ventas = ventaDataMapper.getbyCliente(guid);

        //    foreach (ventaData v in ventas)
        //    {
        //        v.pagos = obtenerPagos(v.ID);
        //        v.detalles = getByID(v.ID).detalles;
        //        //v.tercero =  new cliente().GetByID(v.tercero.ID);
        //    }


        //    return ventas;
        //}

        //public static List<pagoData> obtenerTipoPagos(Guid idTipoPago)
        //{
        //    return ventaDataMapper.obtenerTipoPagos(idTipoPago);
        //}

        //public static bool validarTrial(DateTime fecha,Guid idlocal, string global, int maxventas = 9)
        //{

        //    CultureInfo provider = CultureInfo.InvariantCulture;
            
        //    string format = "MMddyyyy";
        //    DateTime result = DateTime.ParseExact(global, format, provider);
        //    bool ok = true;

        //    if (DateTime.Now>result)
        //    {
                
            
        //    List<ventaData> ventas = getbyfecha(fecha, false, idlocal);

        //    if (ventas != null && ventas.Count > maxventas)
        //        ok = false;
        //    }
        //    return ok;
        //}
    }
}
