using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using BusinessComponents;
using DTO.BusinessEntities;
using Quartz;
using Quartz.Impl;
using SharedForms.LogService;
using Sync.Jobs;
using Sync.Jobs.Clientes;
using Sync.Jobs.Gral;


namespace Sync
{
    class Program
    {
        protected static IScheduler _scheduler { get; set; }

        private static  void Main()
        {


            loadLocalInformation();
            conexion.getLocalConection();
            helper.setPass();
            helper.writeLog("Iniciando Sistema", true);


            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            _scheduler = schedFact.GetScheduler();
           
            //bool createdNew = true;
            //using (Mutex mutex = new Mutex(true, "fc-A80737C0-8A80-4464-8F7C-AB65428B26B5", out createdNew))
            //{
            //    if (createdNew)
            //    {
                    try
                    {
                        if (helper.soyCentral)
                        {
                            UploadCentral();
                           // DownloadCentral();
                        }
                        else
                        {//soy local
                            DownloadPuntoVenta();
                            UploadPuntoVenta();
                        }
                        //DownloadPuntoVenta();
                        _scheduler.Start();
                    }
                    catch (Exception e2)
                    {
                        Console.WriteLine(e2);
                        helper.writeLog("Error: " + e2.ToString(), true);
                    }
            //    }
            //    else
            //    {
            //        Console.WriteLine("Ya hay una instancia abierta del sistema");
            //        helper.writeLog("Ya hay una instancia abierta del sistema", true);
            //    }
            //}


        }
 
        private static void loadLocalInformation()
        {
            try
            {

                helper.consultarArticulo = true;
                helper.idLocal = new Guid(ConfigurationManager.AppSettings["idLocal"]);
                helper.firstNum = Convert.ToInt32(ConfigurationManager.AppSettings["firstNum"]);
                helper.decimalSeparator = ConfigurationManager.AppSettings["decimalSeparator"];
                helper.idListaPrecio = new Guid(ConfigurationManager.AppSettings["idListaPrecio"]);
                conexion.setValues(ConfigurationManager.AppSettings["localCatalog"].ToString(), ConfigurationManager.AppSettings["localDataSource"].ToString(),
                    ConfigurationManager.AppSettings["remoteCatalog"].ToString(), ConfigurationManager.AppSettings["remoteDataSource"].ToString());

                helper.soyCentral = Convert.ToBoolean(ConfigurationManager.AppSettings["SoyCentral"]);

            }
            catch (ConfigurationErrorsException e1)
            {
                //MessageBox.Show("Error en el archivo de configuracion", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Error en el archivo de configuracion");
                helper.writeLog("Error: " + e1.ToString(), true);
                //Application.Exit();
            }
            catch (Exception e2)
            {
                //MessageBox.Show("No se encuentra el archivo de configuracion del local", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("No se encuentra el archivo de configuracion del local");
                helper.writeLog("Error: " + e2.ToString(), true);
                //Application.Exit();
            }

        }




        private static void UploadPuntoVenta()
        {
            bool download = false;

            const int whenStart = 25;
            const int interval_Caja = 9999;
            const int interval_Retiros = 9999;
            const int interval_Ingresos = 9999;
            const int interval_Ventas = 9999;
            const int interval_Remitos = 9999;
            const int interval_Vales = 9999;

            _scheduler.ScheduleJob(tranfieroCajasJob.Get(helper.idLocal, download), defaultUploadTrigger.Get("Cajas", interval_Caja, whenStart));
            _scheduler.ScheduleJob(tranfieroRetirosJob.Get(helper.idLocal, helper.firstNum, download), defaultUploadTrigger.Get("Retiros", interval_Retiros, whenStart));
            _scheduler.ScheduleJob(tranfieroIngresosJob.Get(helper.idLocal, helper.firstNum, download), defaultUploadTrigger.Get("Ingresos", interval_Ingresos, whenStart));
            _scheduler.ScheduleJob(tranfieroVentasJob.Get(helper.idLocal, helper.firstNum, download), defaultUploadTrigger.Get("Ventas", interval_Ventas, whenStart));
            _scheduler.ScheduleJob(tranfieroRemitosJob.Get(helper.idLocal, helper.firstNum, download), defaultUploadTrigger.Get("Remitos", interval_Remitos, whenStart));
            _scheduler.ScheduleJob(tranfieroValesJob.Get(helper.idLocal, helper.firstNum, download), defaultUploadTrigger.Get("Vales", interval_Vales, whenStart));
        }
        private static void DownloadPuntoVenta()
        {
            bool download = true;

            const int whenStart = 25;


            //la primera se corre whenstart segundos despues de empezar, y despues cada interval_XX minutos

            const int interval_Client = 9999;
            const int interval_Remito = 9999;
            const int interval_Color = 9999;
            const int interval_Proveedor = 9999;
            const int interval_Local = 9999;
            const int interval_Producto = 9999;
            const int interval_Personal = 9999;
            const int interval_TipoIngreso = 9999;
            const int interval_TipoRetiro = 9999;
            const int interval_Lista = 9999;
            const int interval_Precio = 9999;
            const int interval_FormasP = 9999;

            _scheduler.ScheduleJob(tranfieroClientesJob.Get(download), defaultDownloadTrigger.Get("cliente", interval_Client, whenStart));
            _scheduler.ScheduleJob(tranfieroColoresJob.Get(download), defaultDownloadTrigger.Get("colores", interval_Color, whenStart));
            _scheduler.ScheduleJob(tranfieroProveedoresJob.Get(download), defaultDownloadTrigger.Get("Proveedores", interval_Proveedor, whenStart));
            _scheduler.ScheduleJob(tranfieroLocalesJob.Get(download), defaultDownloadTrigger.Get("Locales", interval_Local, whenStart));
            _scheduler.ScheduleJob(tranfieroProductosJob.Get(download), defaultDownloadTrigger.Get("Productos", interval_Producto, whenStart));
            _scheduler.ScheduleJob(tranfieroPersonalJob.Get(download), defaultDownloadTrigger.Get("Personal", interval_Personal, whenStart));
            _scheduler.ScheduleJob(tranfieroTipoIngresoJob.Get(download), defaultDownloadTrigger.Get("TipoIngreso", interval_TipoIngreso, whenStart));
            _scheduler.ScheduleJob(tranfieroTipoRetiroJob.Get(download), defaultDownloadTrigger.Get("TipoRetiro", interval_TipoRetiro, whenStart));
            _scheduler.ScheduleJob(tranfieroFormasPagoJob.Get(download), defaultDownloadTrigger.Get("FPago", interval_FormasP, whenStart));
            _scheduler.ScheduleJob(tranfieroListaPrecioJob.Get(download), defaultDownloadTrigger.Get("ListaPrecio", interval_Lista, whenStart));


            _scheduler.ScheduleJob(tranfieroPreciosJob.Get(download), defaultDownloadTrigger.Get("Precios", interval_Precio, whenStart));
            _scheduler.ScheduleJob(tranfieroRemitosParaPvJob.Get(helper.idLocal), defaultDownloadTrigger.Get("remitoPV", interval_Remito, whenStart));


        }

        private static void UploadCentral()
        {
            bool download = false;

            const int whenStart = 25;


            //la primera se corre whenstart segundos despues de empezar, y despues cada interval_XX minutos
            const int interval_Client = 9999;
            const int interval_Remito = 9999;
            const int interval_Color = 9999;
            const int interval_Proveedor = 9999;
            const int interval_Local = 9999;
            const int interval_Producto = 9999;
            const int interval_Personal = 9999;
            const int interval_Ventas = 9999;
            const int interval_Vales = 9999;

            const int interval_Lista = 9999;
            const int interval_Precio = 9999;
            const int interval_FormasP = 9999;

            const int interval_TipoIngreso = 9999;
            const int interval_TipoRetiro = 9999;
            const int interval_Ingreso = 9999;
            const int interval_Retiro = 9999;
            const int interval_Cajas = 9999;


            const int whenStart_interval_Client = 25;
            const int whenStart_interval_Remito = 3;
            const int whenStart_interval_Color = 30;
            const int whenStart_interval_Proveedor = 32;
            const int whenStart_interval_Local = 1;
            const int whenStart_interval_Producto = 35;
            const int whenStart_interval_Personal = 37;
            const int whenStart_interval_Ventas = 40;
            const int whenStart_interval_Vales = 38;

            const int whenStart_interval_Lista = 39;
            const int whenStart_interval_Precio = 45;
            const int whenStart_interval_FormasP = 47;

            const int whenStart_interval_TipoIngreso = 50;
            const int whenStart_interval_TipoRetiro = 52;
            const int whenStart_interval_Ingreso = 54;
            const int whenStart_interval_Retiro = 56;
            const int whenStart_interval_Cajas = 58;



            _scheduler.ScheduleJob(tranfieroClientesJob.Get(download), defaultUploadTrigger.Get("Cliente", interval_Client, whenStart_interval_Client));
            _scheduler.ScheduleJob(tranfieroRemitosJob.Get(helper.idLocal, 1, download), defaultUploadTrigger.Get("Remito", interval_Remito, whenStart_interval_Remito));
            _scheduler.ScheduleJob(tranfieroColoresJob.Get(download), defaultUploadTrigger.Get("colores", interval_Color, whenStart_interval_Color));
            _scheduler.ScheduleJob(tranfieroProveedoresJob.Get(download), defaultUploadTrigger.Get("Proveedores", interval_Proveedor, whenStart_interval_Proveedor));
            _scheduler.ScheduleJob(tranfieroLocalesJob.Get(download), defaultUploadTrigger.Get("Locales", interval_Local, whenStart_interval_Local));
            _scheduler.ScheduleJob(tranfieroProductosJob.Get(download), defaultUploadTrigger.Get("Productos", interval_Producto, whenStart_interval_Producto));
            _scheduler.ScheduleJob(tranfieroPersonalJob.Get(download), defaultUploadTrigger.Get("Personal", interval_Personal, whenStart_interval_Personal));







            _scheduler.ScheduleJob(tranfieroListaPrecioJob.Get(download), defaultUploadTrigger.Get("ListaPrecio", interval_Lista, whenStart_interval_Lista));
            _scheduler.ScheduleJob(tranfieroPreciosJob.Get(download), defaultUploadTrigger.Get("Precios", interval_Precio, whenStart_interval_Precio));
            _scheduler.ScheduleJob(tranfieroFormasPagoJob.Get(download), defaultUploadTrigger.Get("FormasPago", interval_FormasP, whenStart_interval_FormasP));


            _scheduler.ScheduleJob(tranfieroTipoIngresoJob.Get(download), defaultUploadTrigger.Get("TipoIngreso", interval_TipoIngreso, whenStart_interval_TipoIngreso));
            _scheduler.ScheduleJob(tranfieroTipoRetiroJob.Get(download), defaultUploadTrigger.Get("TipoRetiro", interval_TipoRetiro, whenStart_interval_TipoRetiro));


            _scheduler.ScheduleJob(tranfieroVentasJob.Get(helper.idLocal, helper.firstNum, download), defaultUploadTrigger.Get("Ventas", interval_Ventas, whenStart_interval_Ventas));
            //_scheduler.ScheduleJob(tranfieroValesJob.Get(helper.idLocal, helper.firstNum, download), defaultUploadTrigger.Get("Vales", interval_Vales, whenStart_interval_Vales));

            _scheduler.ScheduleJob(tranfieroRetirosJob.Get(helper.idLocal, helper.firstNum, download), defaultUploadTrigger.Get("Retiro", interval_Retiro, whenStart_interval_Ingreso));
            _scheduler.ScheduleJob(tranfieroIngresosJob.Get(helper.idLocal, helper.firstNum, download), defaultUploadTrigger.Get("Ingreso", interval_Ingreso, whenStart_interval_Retiro));
            _scheduler.ScheduleJob(tranfieroCajasJob.Get(helper.idLocal, download), defaultUploadTrigger.Get("Cajas", interval_Cajas, whenStart_interval_Cajas));


        }

        private static void DownloadCentral()
        {

            //_scheduler.ScheduleJob(tranfieroRemitosParaPvJob.Get(helper.idLocal), DownloadTrigger.Get());
            bool download = true;

            const int whenStart = 25;
            const int interval_Caja = 9999;
            const int interval_Retiros = 9999;
            const int interval_Ingresos = 9999;
            const int interval_Ventas = 9999;
            const int interval_Remitos = 9999;
            const int interval_Vales = 9999;
            const int interval_Local = 9999;

            
            _scheduler.ScheduleJob(tranfieroLocalesJob.Get(download), defaultDownloadTrigger.Get("Locales", interval_Local, whenStart));
            JobKey aux = new JobKey("locales_" + download.ToString());
            var jobLocales = _scheduler.GetJobDetail(aux);

            while (jobLocales.JobDataMap.GetInt("contadorReal")==0)//espero que termine el jobs de locales para arrancar el resto.
            {
                jobLocales = _scheduler.GetJobDetail(aux);
                Thread.Sleep(500);
            }

            
            List<localData> locales = BusinessComponents.local.getAll();

            foreach (localData local in locales)
            {
                _scheduler.ScheduleJob(tranfieroCajasJob.Get(local.id, download), defaultDownloadTrigger.Get("Cajas_" + local.id.ToString(), interval_Caja, whenStart));
                _scheduler.ScheduleJob(tranfieroRetirosJob.Get(local.id, helper.firstNum, download), defaultDownloadTrigger.Get("Retiros_" + local.id.ToString(), interval_Retiros, whenStart));
                _scheduler.ScheduleJob(tranfieroIngresosJob.Get(local.id, helper.firstNum, download), defaultDownloadTrigger.Get("Ingresos_" + local.id.ToString(), interval_Ingresos, whenStart));
                _scheduler.ScheduleJob(tranfieroVentasJob.Get(local.id, helper.firstNum, download), defaultDownloadTrigger.Get("Ventas_" + local.id.ToString(), interval_Ventas, whenStart));
                _scheduler.ScheduleJob(tranfieroRemitosJob.Get(local.id, helper.firstNum, download), defaultDownloadTrigger.Get("Remitos_" + local.id.ToString(), interval_Remitos, whenStart));
                _scheduler.ScheduleJob(tranfieroValesJob.Get(local.id, helper.firstNum, download), defaultDownloadTrigger.Get("Vales_" + local.id.ToString(), interval_Vales, whenStart));
            }

            // aca tambien tendria que descargar exactamente lo opuesto a uploadcentral? ( por si hay 2 centrales )


        }

    }

   
}
