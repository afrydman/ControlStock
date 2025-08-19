using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessComponents;
using DTO.BusinessEntities;
using Quartz;

namespace Sync.Jobs
{
    class tranfieroLocalesJob : LoggeableJob, IJob
    {
        public tranfieroLocalesJob()
            : base("tranfieroLocalesJob")
        {
        }

        public static IJobDetail Get(bool descargar = false, int CountEjecuccionDiferencia = 99999)
        {
            string category = descargar.ToString();
            return JobBuilder.Create<tranfieroLocalesJob>()
                .WithIdentity("locales_" + category)
                .UsingJobData("descargar", descargar)
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
                    update(dataMap.GetBoolean("descargar"));
                    
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


        private void update(bool download)
        {

            bool connLocal;
            bool aux = false;
            connLocal = download;




            List<localData> localesRemoto = BusinessComponents.local.getAll(connLocal);
            if (localesRemoto != null)
                Log("Se encontraron " + localesRemoto.Count + " en la conexion local:" + connLocal.ToString());

            connLocal = !download;

            List<localData> localeslocal = BusinessComponents.local.getAll(connLocal);
            if (localeslocal != null)
                Log("Se encontraron " + localeslocal.Count + " en la conexion local:" + connLocal.ToString());


            List<localData> listaAleNoquierePensar = new List<localData>();

            foreach (localData l in localeslocal)
            {
                if (localesRemoto.Find(delegate(localData ll) { return ll.id != l.id; }) == null)
                {
                    listaAleNoquierePensar.Add(l);
                }
            }




            connLocal = download;

            foreach (localData l in listaAleNoquierePensar)
            {
                aux = BusinessComponents.local.insert(l, connLocal);
                
                Log("Insertando " + l.id.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
            }
        }
    }
}
