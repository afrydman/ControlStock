using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessComponents;
using DTO.BusinessEntities;
using Quartz;

namespace Sync.Jobs
{
    class tranfieroIngresosJob : LoggeableJob, IJob
    {
        public tranfieroIngresosJob()
            : base("tranfieroIngresosJob")
        {
        }

        public static IJobDetail Get(Guid idLocal, int prefix, bool descargar = false, int CountEjecuccionDiferencia = 99999)
        {
            string category = descargar.ToString();
            return JobBuilder.Create<tranfieroIngresosJob>()
                .WithIdentity("ingresos_" + category)
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

                context.JobDetail.JobDataMap["completo"] = (int)JobHelper.statusJob.sleep;

            }
            Close();
        }

        private void update(bool download, Guid idLocal, int prefix)
        {


            bool connLocal;
            bool aux = false;
            connLocal = download;



            retiroData retiroRemoto = BusinessComponents.ingreso.getLast(idLocal, prefix, connLocal);
            if (retiroRemoto != null)
                Log("Se encontro como ultimo el numero " + retiroRemoto.numero + " en la conexion local:" + connLocal.ToString());

            connLocal = !download;


            List<retiroData> retirosLocal = BusinessComponents.ingreso.getOlderThan(retiroRemoto.fecha, idLocal, prefix, connLocal);
            if (retirosLocal != null)
                Log("Se encontraron " + retirosLocal.Count + " en la conexion local:" + connLocal.ToString());


            List<retiroData> retirosM = BusinessComponents.ingreso.getModified(idLocal, prefix, connLocal);
            BusinessComponents.ingreso.yaviqueestabasmodificadamacho(idLocal, prefix, connLocal);
            if (retirosM != null)
                Log("Se encontraron " + retirosM.Count + " Modificados en la conexion local:" + connLocal.ToString());



            connLocal = download;

            foreach (retiroData r in retirosLocal)
            {
                aux = BusinessComponents.ingreso.insertarRetiro(r, connLocal);
                Log("Insertando: " + r.id.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
            }
            foreach (retiroData r in retirosM)
            {
                aux = BusinessComponents.ingreso.delete(r.id, connLocal);
                Log("Updateando: " + r.id.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
            }


        }
    }
}
