using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessComponents;
using DTO.BusinessEntities;
using Quartz;

namespace Sync.Jobs
{
    class tranfieroVentasJob : LoggeableJob, IJob
    {
        public tranfieroVentasJob()
            : base("tranfieroVentasJob")
        {
        }

        public static IJobDetail Get(Guid idLocal, int prefix, bool descargar = false, int CountEjecuccionDiferencia = 99999)
        {
            string category = descargar.ToString();
            return JobBuilder.Create<tranfieroVentasJob>()
                .WithIdentity("ventas_" + category)
                .UsingJobData("descargar", descargar)
                .UsingJobData("prefix", prefix)
                .UsingJobData("idLocal", idLocal.ToString())
                .UsingJobData("contadorReal", 0)
                .UsingJobData("CountEjecuccionDiferencia", CountEjecuccionDiferencia)

                .Build();

        }
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                Log("Arrancando");

                JobDataMap dataMap = context.JobDetail.JobDataMap;
                if (JobHelper.FireNow(context))
                {
                    Log("Disparando");
                    update(dataMap.GetBoolean("descargar"), new Guid(dataMap.GetString("idLocal")), dataMap.GetInt("prefix"));
                    Log("Completo");
                }
                else
                {
                    Log("No puedo disparar ahora");
                }


            }
            catch (Exception ex)
            {
                Log(ex.Message);
                Log(ex.StackTrace);

            }
            finally
            {

                context.JobDetail.JobDataMap["status"] = (int)JobHelper.statusJob.sleep;

            }
            Close();
        }

        private void update(bool download, Guid idLocal,int prefix)
        {

            //upload = download false

            bool connLocal;
            bool aux = false;


            connLocal = download;


            int ultima = 0;
            ultima = BusinessComponents.venta.getLastNumber(idLocal, prefix, connLocal);
            
            Log("Se encontro como ultima numero:" + ultima + " | DB_local: " + connLocal.ToString());
          
            connLocal = !download;


            List<ventaData> ventasLocal = BusinessComponents.venta.getBiggerThan(ultima, idLocal, prefix, connLocal);

            if (ventasLocal != null)
                Log("Se encontraron " + ventasLocal.Count + " ventas en | DB_local:"  +connLocal.ToString());
          

            //las modificaciones solo son anulaciones, pq puedo
            List<ventaData> ventasModificadas = BusinessComponents.venta.getModified(idLocal, connLocal);

            BusinessComponents.venta.yaviqueestabasmodificadamacho(idLocal, connLocal);

            if (ventasModificadas != null)
                Log("Se encontraron " + ventasModificadas.Count + " ventas modificadas en | DB_local:" + connLocal.ToString());
          




            connLocal = download;

            foreach (ventaData v in ventasLocal)
            {

               aux= BusinessComponents.venta.insertVenta(v,false,connLocal);
               Log("Insertando " + v.id.ToString() + " DB_local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
            }

            foreach (ventaData v in ventasModificadas)
            {
                aux = BusinessComponents.venta.anuloVenta(v.id, connLocal);
                Log("modificando " + v.id.ToString() + " DB_local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
            }
        }
    }
}
