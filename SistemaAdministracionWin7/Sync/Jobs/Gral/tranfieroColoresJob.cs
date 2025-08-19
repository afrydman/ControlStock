using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessComponents;
using DTO.BusinessEntities;
using Quartz;

namespace Sync.Jobs
{
    class tranfieroColoresJob : LoggeableJob, IJob
    {
        public tranfieroColoresJob()
            : base("tranfieroColoresJob")
        {
        }

        public static IJobDetail Get(bool descargar = false, int CountEjecuccionDiferencia = 99999)
        {
            string category = descargar.ToString();
            return JobBuilder.Create<tranfieroColoresJob>()
                .WithIdentity("colores_" + category)
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
            connLocal = !download;

            List<colorData> coloresRemoto = BusinessComponents.color.getAll(false,connLocal);
            if (coloresRemoto != null)
                Log("Se encontraron " + coloresRemoto.Count + " en la conexion local:" + connLocal.ToString());


            connLocal = download;
            List<colorData> coloresLocal = BusinessComponents.color.getAll(false, connLocal);
            if (coloresLocal != null)
                Log("Se encontraron " + coloresLocal.Count + " en la conexion local:" + connLocal.ToString());


            bool aux;

            for (int i = (coloresLocal != null) ? coloresLocal.Count : 0; i <= coloresRemoto.Count - 1; i++)//test!
            {
                aux = BusinessComponents.color.insert(coloresRemoto[i], connLocal);

                Log("Insertando " + coloresRemoto[i].id.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
            }
        }

    }
}
