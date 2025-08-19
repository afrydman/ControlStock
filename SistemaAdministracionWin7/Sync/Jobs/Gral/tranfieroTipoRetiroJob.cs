using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessComponents;
using DTO.BusinessEntities;
using Quartz;

namespace Sync.Jobs
{
    class tranfieroTipoRetiroJob : LoggeableJob, IJob
    {
        public tranfieroTipoRetiroJob()
            : base("tranfieroTipoRetiroJob")
        {
        }

        public static IJobDetail Get(bool descargar = false, int CountEjecuccionDiferencia = 99999)
        {
            string category = descargar.ToString();
            return JobBuilder.Create<tranfieroTipoRetiroJob>()
                .WithIdentity("tipoRetiro_" + category)
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





            List<tipoRetiroData> retiroremoto = tipoRetiro.getAll(false,connLocal);
            if (retiroremoto != null)
                Log("Se encontraron " + retiroremoto.Count + " en la conexion local:" + connLocal.ToString());


            connLocal = download;

            List<tipoRetiroData> retirolocal = tipoRetiro.getAll(false, connLocal);
            if (retirolocal != null)
                Log("Se encontraron " + retirolocal.Count + " en la conexion local:" + connLocal.ToString());



            foreach (tipoRetiroData p in retiroremoto)
            {

                if (retirolocal.Find(delegate(tipoRetiroData pp) { return pp.id == p.id; }) == null)
                {//nuevo
                    aux = BusinessComponents.tipoRetiro.insert(p,connLocal);
                    Log("Insertando " + p.id.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
                }
                else
                {//update
                    aux = BusinessComponents.tipoRetiro.update(p,connLocal);
                    Log("Actualizando " + p.id.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
                }


            }

        }
    }
}
