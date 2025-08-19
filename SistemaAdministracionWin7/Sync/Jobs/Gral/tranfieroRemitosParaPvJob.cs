using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessComponents;
using DTO.BusinessEntities;
using Quartz;

namespace Sync.Jobs
{
    class tranfieroRemitosParaPvJob : LoggeableJob, IJob
    {
        public tranfieroRemitosParaPvJob()
            : base("tranfieroRemitosParaPvJob")
        {
        }

        public static IJobDetail Get(Guid idLocal, int CountEjecuccionDiferencia = 99999)
        {
 
            return JobBuilder.Create<tranfieroRemitosParaPvJob>()
                .WithIdentity("remitospv_" )
 
                .UsingJobData("idLocal", idLocal.ToString())
                .UsingJobData("contadorReal", 0)
                .UsingJobData("CountEjecuccionDiferencia", CountEjecuccionDiferencia)
                .UsingJobData("status", (int)JobHelper.statusJob.sleep)
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
                     getMisRemitos(new Guid(dataMap.GetString("idLocal")));
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
        //busco los q son para mi y me los traigo
        //

        private void getMisRemitos( Guid idLocal)
        {
            List<remitoData> remitosLocal;
            List<remitoData> remitosRemoto;




            bool connLocal;

            bool aux = false;


            connLocal = false;
            remitosRemoto = BusinessComponents.remito.getByLocalSinRecibir(idLocal, false,connLocal );
            if(remitosRemoto!=null)
                Log("se encontraron "+ remitosRemoto.Count.ToString()+" remitos en cloud");


            connLocal = true;
            remitosLocal = BusinessComponents.remito.getByLocal(idLocal, false, connLocal);
            if (remitosLocal != null)
                Log("se encontraron " + remitosRemoto.Count.ToString() + " remitos en local");

            foreach (remitoData r in remitosRemoto)
            {
                if (remitosLocal.Find(delegate(remitoData rr) { return rr.id == r.id; }) == null)
                {
                    
                    aux = BusinessComponents.remito.generarNuevo(r, connLocal);
                    Log("Descargando el remito: " + r.id.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
                }
            }




        }

       
    }
}
