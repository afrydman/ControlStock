//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;

namespace BusinessComponents
{
    public static class pedido
    {
        //public static List<pedidoData> GetAll()
        //{
        //    List<pedidoData> aux = pedidoDataMapper.getall();
        //    foreach (pedidoData item in aux)
        //     {
        //         item.detalles = pedidodetalle.getById(item.ID);
        //         //item.tercero = new cliente().GetByID(item.tercero.ID);
                
                
        //     }
            

        //    return aux;
        //}
       //public  enum impresiones { factura,remito };

       // public static int getLastNumber(Guid idLocal)
       // {
       //     return getLastNumber(idLocal, helper.firstNum);
        
       // }
       // public static int getLastNumber(int myprefix)
       // {
       //     return getLastNumber(helper.IDLocal,myprefix);
       // }
       // public static int getLastNumber()
       // {
       //     return getLastNumber(helper.IDLocal,helper.firstNum); ;
       // }
       
       // public static int getLastNumber(Guid id,int myprefix)
       // {

       //   return pedidoDataMapper.getLastNumber(id,myprefix);

       // }


       // public static Guid insertVenta(DTO.BusinessEntities.pedidoData nuevaVenta)
       // {
       //     Guid id;
       //     if (nuevaVenta.ID == new Guid())
       //     {
       //         id = Guid.NewGuid();
       //         nuevaVenta.ID = id;
       //     }
       //     else
       //     {
       //         id = nuevaVenta.ID;
       //     }
            
            
       //     pedidoDataMapper.insertPedido(nuevaVenta);
       //     return id;
            
       // }

       


       // //public static void imprimir(impresiones impresionType,Guid id)
       // //{

            
       // //    string DA_REPORT = "";

       // //    if (helper.esCliente(GrupoCliente.Opiparo))
       // //    {
               
       // //        if (impresionType==impresiones.factura)
       // //        {
       // //            DA_REPORT = @"\Reportes\opiparo_factura.rpt";
       // //        }
               
       // //        if (impresionType == impresiones.remito)
       // //        {
       // //            DA_REPORT = @"\Reportes\opiparo_remito.rpt";
       // //        }
                
                
       // //    }


       // //    ReportDocument cryRpt = new ReportDocument();
       // //    cryRpt.Load(Application.StartupPath + DA_REPORT);
       // //    ConnectionInfo crConnectionInfo = new ConnectionInfo();
       // //    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
       // //    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();

       // //    Stack<string> param = helper.getParameters();
       // //    crConnectionInfo.Password = param.Pop();
       // //    crConnectionInfo.UserID = param.Pop();
       // //    crConnectionInfo.DatabaseName = param.Pop();
       // //    crConnectionInfo.ServerName = param.Pop();
       // //    Tables CrTables;

       // //    CrTables = cryRpt.Database.Tables;
       // //    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
       // //    {
       // //        crtableLogoninfo = CrTable.LogOnInfo;
       // //        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
       // //        CrTable.ApplyLogOnInfo(crtableLogoninfo);
       // //    }

       // //    ParameterFieldDefinitions crParameterFieldDefinitions;
       // //    ParameterFieldDefinition crParameterFieldDefinition;
       // //    ParameterValues crParameterValues = new ParameterValues();
       // //    ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

       // //    crParameterDiscreteValue.Value = id.ToString();
       // //    crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
       // //    crParameterFieldDefinition = crParameterFieldDefinitions["@id"];
       // //    crParameterValues = crParameterFieldDefinition.CurrentValues;


       // //    crParameterValues.Clear();
       // //    crParameterValues.Add(crParameterDiscreteValue);
       // //    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);


       // //    settingPositions(DA_REPORT, cryRpt);








           

       // //    cryRpt.PrintToPrinter(1, false, 0, 0);
       // //    //crystalReportViewer1.Refresh(); 
        
       // //}

       // //private static void settingPositions(string DA_REPORT, ReportDocument cryRpt)
       // //{

       // //    if (DA_REPORT == @"\Reportes\opiparo_factura.rpt")
       // //    {
       // //        #region fields positions

       // //        cryRpt.ReportDefinition.ReportObjects["fecha1"].Top = 2067;
       // //        cryRpt.ReportDefinition.ReportObjects["fecha1"].Left = 7654;

       // //        cryRpt.ReportDefinition.ReportObjects["razonsocial1"].Top = 3768;
       // //        cryRpt.ReportDefinition.ReportObjects["razonsocial1"].Left = 1984;

       // //        cryRpt.ReportDefinition.ReportObjects["direccion1"].Top = 4335;
       // //        cryRpt.ReportDefinition.ReportObjects["direccion1"].Left = 1984;

       // //        cryRpt.ReportDefinition.ReportObjects["condicioniva"].Top = 5269;
       // //        cryRpt.ReportDefinition.ReportObjects["condicioniva"].Left = 2834;


       // //        cryRpt.ReportDefinition.ReportObjects["condicionventa"].Top = 5552;
       // //        cryRpt.ReportDefinition.ReportObjects["condicionventa"].Left = 2834;

       // //        cryRpt.ReportDefinition.ReportObjects["cuil1"].Top = 5269;
       // //        cryRpt.ReportDefinition.ReportObjects["cuil1"].Left = 9070;

       // //        cryRpt.ReportDefinition.ReportObjects["cantidad1"].Top = 120;
       // //        cryRpt.ReportDefinition.ReportObjects["cantidad1"].Left = 600;

       // //        cryRpt.ReportDefinition.ReportObjects["codigo1"].Top = 120;
       // //        cryRpt.ReportDefinition.ReportObjects["codigo1"].Left = 2851;


       // //        cryRpt.ReportDefinition.ReportObjects["codigoproveedor1"].Top = 120;
       // //        cryRpt.ReportDefinition.ReportObjects["codigoproveedor1"].Left = 4040;

       // //        cryRpt.ReportDefinition.ReportObjects["productodescripcion1"].Top = 120;
       // //        cryRpt.ReportDefinition.ReportObjects["productodescripcion1"].Left = 4835;

       // //        cryRpt.ReportDefinition.ReportObjects["colordescripcion1"].Top = 120;
       // //        cryRpt.ReportDefinition.ReportObjects["colordescripcion1"].Left = 6800;

       // //        cryRpt.ReportDefinition.ReportObjects["precio1"].Top = 120;
       // //        cryRpt.ReportDefinition.ReportObjects["precio1"].Left = 8503;

       // //        cryRpt.ReportDefinition.ReportObjects["subt1"].Top = 120;
       // //        cryRpt.ReportDefinition.ReportObjects["subt1"].Left = 10204;



       // //        //footer
       // //        cryRpt.ReportDefinition.ReportObjects["subtotal1"].Top = 0;
       // //        cryRpt.ReportDefinition.ReportObjects["subtotal1"].Left = 10204;


       // //        cryRpt.ReportDefinition.ReportObjects["iva1"].Top = 1120;
       // //        cryRpt.ReportDefinition.ReportObjects["iva1"].Left = 7000;


       // //        cryRpt.ReportDefinition.ReportObjects["total1"].Top = 1120;
       // //        cryRpt.ReportDefinition.ReportObjects["total1"].Left = 10204;

       // //        cryRpt.ReportDefinition.ReportObjects["subtotal2"].Top = 1120;
       // //        cryRpt.ReportDefinition.ReportObjects["subtotal2"].Left = 0;

       // //        cryRpt.ReportDefinition.ReportObjects["expreso1"].Top = 1720;
       // //        cryRpt.ReportDefinition.ReportObjects["expreso1"].Left = 0;

       // //        cryRpt.ReportDefinition.ReportObjects["dir"].Top = 1720;
       // //        cryRpt.ReportDefinition.ReportObjects["dir"].Left = 6000;


       // //        cryRpt.ReportDefinition.ReportObjects["obs1"].Top = 2220;
       // //        cryRpt.ReportDefinition.ReportObjects["obs1"].Left = 800;





       // //        cryRpt.ReportDefinition.Sections["Section1"].Height = 0;

       // //        cryRpt.ReportDefinition.Sections["Section2"].Height += 500;

       // //        cryRpt.ReportDefinition.Sections["Section5"].Height = 3000;



       // //        #endregion
       // //    }
            
            
       // //    if (DA_REPORT == @"\Reportes\opiparo_remito.rpt")
       // //    {
       // //        #region fields positions


       // //        //header


       // //        cryRpt.ReportDefinition.ReportObjects["fecha1"].Top = 2067;
       // //        cryRpt.ReportDefinition.ReportObjects["fecha1"].Left = 7654;

       // //        cryRpt.ReportDefinition.ReportObjects["razonsocial1"].Top = 3768;
       // //        cryRpt.ReportDefinition.ReportObjects["razonsocial1"].Left = 1984;

       // //        cryRpt.ReportDefinition.ReportObjects["direccion1"].Top = 4335;
       // //        cryRpt.ReportDefinition.ReportObjects["direccion1"].Left = 1984;

       // //        cryRpt.ReportDefinition.ReportObjects["condicioniva"].Top = 5269;
       // //        cryRpt.ReportDefinition.ReportObjects["condicioniva"].Left = 2834;


       // //        cryRpt.ReportDefinition.ReportObjects["condicionventa"].Top = 5552;
       // //        cryRpt.ReportDefinition.ReportObjects["condicionventa"].Left = 2834;

       // //        cryRpt.ReportDefinition.ReportObjects["cuil1"].Top = 5269;
       // //        cryRpt.ReportDefinition.ReportObjects["cuil1"].Left = 9070;

       // //        //description

       // //        cryRpt.ReportDefinition.ReportObjects["cantidad1"].Top = 120;
       // //        cryRpt.ReportDefinition.ReportObjects["cantidad1"].Left = 600;

       // //        cryRpt.ReportDefinition.ReportObjects["codigo1"].Top = 120;
       // //        cryRpt.ReportDefinition.ReportObjects["codigo1"].Left = 2851;


       // //        cryRpt.ReportDefinition.ReportObjects["codigoproveedor1"].Top = 120;
       // //        cryRpt.ReportDefinition.ReportObjects["codigoproveedor1"].Left = 4040;

       // //        cryRpt.ReportDefinition.ReportObjects["productodescripcion1"].Top = 120;
       // //        cryRpt.ReportDefinition.ReportObjects["productodescripcion1"].Left = 4835;

       // //        cryRpt.ReportDefinition.ReportObjects["colordescripcion1"].Top = 120;
       // //        cryRpt.ReportDefinition.ReportObjects["colordescripcion1"].Left = 6800;




       // //        //footer


       // //        //cryRpt.ReportDefinition.ReportObjects["total1"].Top = 1120;
       // //        //cryRpt.ReportDefinition.ReportObjects["total1"].Left = 2851;


       // //        //cryRpt.ReportDefinition.ReportObjects["propio"].Top = 1720;
       // //        //cryRpt.ReportDefinition.ReportObjects["propio"].Left = 0;

       // //        //cryRpt.ReportDefinition.ReportObjects["dir"].Top = 1720;
       // //        //cryRpt.ReportDefinition.ReportObjects["dir"].Left = 6000;


       // //        //cryRpt.ReportDefinition.ReportObjects["obs1"].Top = 2220;
       // //        //cryRpt.ReportDefinition.ReportObjects["obs1"].Left = 800;





       // //        cryRpt.ReportDefinition.Sections["Section1"].Height = 0;

       // //        cryRpt.ReportDefinition.Sections["Section2"].Height += 500;

       // //        cryRpt.ReportDefinition.Sections["Section5"].Height = 3000;



       // //        #endregion
       // //    }

       // //}

       


      


       // public static bool anularPedido(pedidoData v) {

       //     return pedidoDataMapper.anularPedido(v.ID);
            
        
       // }



       // public static bool actualizarPedido(pedidoData newV)
       // {
       //     return pedidoDataMapper.actualizar(newV);
            
       // }



       // public static pedidoData getByID(Guid id)
       // {
       //     pedidoData v = pedidoDataMapper.getbyID(id);

       //     List<pedidoDetalleData> vd = pedidodetalle.getById(v.ID);
       //     v.detalles = vd;
       //    // v.tercero =  new cliente().GetByID(v.tercero.ID);
        
       //     return v;
       // }





       // public static List<pedidoData> getBiggerThan(int ultimaVenta, Guid idLocal, int Prefix)
       // {
       //     List<pedidoData> ventas = pedidoDataMapper.getBiggerThan(ultimaVenta, idLocal, Prefix);


       //     foreach (pedidoData v in ventas)
       //     {

       //         List<pedidoDetalleData> vd = pedidodetalle.getById(v.ID);
       //         v.detalles = vd;               
       //        // v.tercero =  new cliente().GetByID(v.tercero.ID);
       //     }

       //     return ventas;
       // }
       // public static List<pedidoData> getBiggerThan(int ultimaVenta)
       // {
       //     return getBiggerThan(ultimaVenta,  helper.IDLocal,helper.firstNum);
       // }




       // public static List<pedidoData> getModified()
       // {

       //     return getModified( helper.IDLocal);
       // }
       // public static List<pedidoData> getModified(Guid idLocal)
       // {
       //     List<pedidoData> ventas = pedidoDataMapper.getModified(idLocal);

       //     foreach (pedidoData v in ventas)
       //     {
       //         List<pedidoDetalleData> vd = pedidodetalle.getById(v.ID);
       //         v.detalles = vd;   
       //         //v.tercero =  new cliente().GetByID(v.tercero.ID);
       //     }
            
            

       //     return ventas;

       // }

       // public static bool yaviqueestabasmodificadamacho()
       // {
       //     return pedidoDataMapper.yaviqueestabasmodificadamacho(helper.IDLocal);
       // }

      

      

       // //public static ventaData getLast()
       // //{
       // //    return ventaDataMapper.get

       // //}
       // /// <summary>
       // /// solo va a funcionar para 2 Prefix por Local
       // /// </summary>
       // /// <param name="idLocal"></param>
       // /// <param name="p"></param>
       // /// <returns></returns>
       // public static int getLastNumberOther(Guid idLocal, int p)
       // {
       //     return ventaDataMapper.getLastNumberOther(idLocal, p);
       // }

       // public static bool marcarCompleto(Guid guid,bool completo)
       // {
       //     return pedidoDataMapper.marcarCompleto(guid, completo);
       // }
    }
}
