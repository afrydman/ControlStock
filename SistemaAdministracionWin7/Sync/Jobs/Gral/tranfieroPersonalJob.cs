using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessComponents;
using DTO;
using Quartz;

namespace Sync.Jobs.Gral
{
    class tranfieroPersonalJob : LoggeableJob, IJob
    {
        public tranfieroPersonalJob()
            : base("tranfieroPersonalJob")
        {
        }

        public static IJobDetail Get(bool descargar = false, int CountEjecuccionDiferencia = 99999)
        {
            string category = descargar.ToString();
            return JobBuilder.Create<tranfieroPersonalJob>()
                .WithIdentity("personal_" + category)
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
            connLocal = !download;



            List<personalData> personalRemoto = personal.getAll(connLocal);
            if (personalRemoto!=null)
                Log("Se encontraron " + personalRemoto.Count + " en DB_local" + connLocal.ToString());
            connLocal = download;

            List<personalData> personalLocal = personal.getAll(connLocal);
            if (personalLocal != null)
                Log("Se encontraron " + personalLocal.Count + " en DB_local" + connLocal.ToString());


            foreach (personalData p in personalRemoto)
            {

                if (personalLocal.Find(delegate(personalData pp) { return pp.id == p.id; }) == null)
                {//nuevo
                    
                    aux = BusinessComponents.personal.insert(p, connLocal);
                    Log("Insertando " + p.id.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
                }
                else
                {//update
                    
                    aux = BusinessComponents.personal.update(p, connLocal);
                    Log("Update  " + p.id.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
                }


            }
        }
    }
}

