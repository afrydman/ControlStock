using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessComponents;
using DTO.BusinessEntities;
using Quartz;

namespace Sync.Jobs
{
    class tranfieroCajasJob : LoggeableJob,IJob
    {
        public tranfieroCajasJob()
            : base("tranfieroCajasJob")
        {
        }

        public static IJobDetail Get(Guid idLocal, bool descargar = false,int CountEjecuccionDiferencia = 99999)
        {
            string category = descargar.ToString();
            return JobBuilder.Create<tranfieroCajasJob>()
                .WithIdentity("cajas_" + category)
                .UsingJobData("descargar", descargar)

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
                    update(dataMap.GetBoolean("descargar"), new Guid(dataMap.GetString("idLocal")));
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
         cajaData cajaRemota;
         List<cajaData> cajasLocal;
         private void update(bool download, Guid idLocal)
         {

             bool aux = false;
            bool connLocal;


            connLocal = download;

            cajaRemota = BusinessComponents.caja.getLastOne(idLocal, connLocal);

            connLocal = !download;


            cajasLocal = BusinessComponents.caja.getOlderThan(cajaRemota.fecha.AddSeconds(1), idLocal, connLocal);
            
             connLocal = download;
             if (cajasLocal != null)
                 Log("Se encontraron " +cajasLocal.Count.ToString() + " en DB_local: "+ connLocal.ToString());
             foreach (cajaData caj in cajasLocal)
            {
                
               aux =  BusinessComponents.caja.cerrarCaja(caj.fecha, caj.monto, caj.id, caj.idLocal, connLocal);
               Log("Insertando " + caj.id.ToString() + "en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
            }


        }
    }
}
