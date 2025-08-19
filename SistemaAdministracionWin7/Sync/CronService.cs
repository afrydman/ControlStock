using System;
using System.Configuration;
using System.ServiceProcess;
using Quartz;
using Quartz.Impl;
using SharedForms.LogService;
using Sync.Jobs;
using Sync.Jobs.Clientes;

namespace Sync
{
    partial class CronService : ServiceBase
    {
        protected IScheduler _scheduler { get; set; }
        public CronService()
        {
            InitializeComponent();
            _scheduler = new StdSchedulerFactory().GetScheduler();
        }

        public void OnDebug()
        {
            _scheduler = new StdSchedulerFactory().GetScheduler();
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            try
            {
            
              

                _scheduler.Start();


                if (Convert.ToBoolean(ConfigurationManager.AppSettings["SoyCentral"]))
                {//es una instancia corriendo en Central
                    


                }
                else
                {//es una instancia corriendo en Punto de venta

                    //_scheduler.ScheduleJob(tranfieroClientesJob.Get(true), DownloadTrigger.Get());
                    //_scheduler.ScheduleJob(tranfieroClientesJob.Get(false), DownloadTrigger.Get());
                    //download stuff
                    //BajoClientes(true;

                    //BajoColores(true;

                    //BajoProveedores(true;
 
                    //BajoFormasPago(true;
 

                    //BajoListasPrecio(true;
 

                    //BajoLocales(true;
 

                    //List<Guid> newProducts = new List<Guid>();
                    //newProducts = BajoProductos(true;

                    

                    //BajoProductosTalle(newProducts, true;
 
                    //BajoPrecio(true;
                     

                    //BajoRemitosPv();
                     

                    //BajoPersonal(true;
                     

                    //BajoTipoRetiro(true;
 

                    //BajoTipoIngreso(true;


                }

              
            }
            catch (Exception ex)
            {
                var logger = new Logger(String.Format(ConfigurationManager.AppSettings["CarpetaLogs"], DateTime.Today.ToString("yyyy-MM-dd"), "SERVICE_EXCEPTION"));
                logger.Log(ex.ToString());
                logger.Close();
            }
        }

        protected override void OnStop()
        {
            _scheduler.Shutdown();
        }
    }
}
