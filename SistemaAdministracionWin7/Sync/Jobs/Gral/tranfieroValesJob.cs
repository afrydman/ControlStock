using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessComponents;
using DTO.BusinessEntities;
using Quartz;

namespace Sync.Jobs
{
    class tranfieroValesJob : LoggeableJob, IJob
    {
        public tranfieroValesJob()
            : base("tranfieroValesJob")
        {
        }

        public static IJobDetail Get(Guid idLocal, int prefix, bool descargar = false, int CountEjecuccionDiferencia = 99999)
        {
            string category = descargar.ToString();
            return JobBuilder.Create<tranfieroValesJob>()
                .WithIdentity("vales_" + category)
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
        private void update(bool download, Guid idlocal, int prefix)
        {

            
            bool connLocal;
            bool aux = false;


            connLocal = !download;


            int ultima = 0;
            ultima = BusinessComponents.vale.getLast(idlocal, prefix, connLocal);
            Log("Se encontro como ultima " + ultima + " en la conexion local:" + connLocal.ToString());

            connLocal = download;

            List<valeData> valesLocal = BusinessComponents.vale.getBigger(ultima, idlocal, prefix, connLocal);
            List<valeData> valesLocalAnul = BusinessComponents.vale.getAnulados(idlocal, prefix, connLocal);
            if (valesLocal != null)
                Log("Se encontraron " + valesLocal.Count + " en la conexion local:" + connLocal.ToString());

            if (valesLocalAnul != null)
                Log("Se encontraron " + valesLocalAnul.Count + " en la conexion local:" + connLocal.ToString());


            connLocal = !download;

            foreach (valeData v in valesLocal)
            {

                aux = BusinessComponents.vale.insertarVale(v, connLocal);
                Log("Insertando " + v.id.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
            }

            foreach (valeData v in valesLocalAnul)
            {
                aux = BusinessComponents.vale.anular(v.id, connLocal);
                Log("Anulando " + v.id.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
            }

        }
    }
}
