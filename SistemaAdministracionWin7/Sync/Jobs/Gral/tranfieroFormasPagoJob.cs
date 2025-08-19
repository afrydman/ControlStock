using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessComponents;
using DTO.BusinessEntities;
using Quartz;
using SharedForms.Helper;

namespace Sync.Jobs
{
    class tranfieroFormasPagoJob : LoggeableJob, IJob
    {
        public tranfieroFormasPagoJob()
            : base("tranfieroFormasPagoJob")
        {
        }

        public static IJobDetail Get(bool descargar = false, int CountEjecuccionDiferencia = 99999)
        {
            string category = descargar.ToString();
            return JobBuilder.Create<tranfieroFormasPagoJob>()
                .WithIdentity("fpago_" + category)
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


            List<formaPagoData> formasPagoRemoto = BusinessComponents.formaPago.getAll(false, connLocal);
            if (formasPagoRemoto != null)
                Log("Se encontraron " + formasPagoRemoto.Count + " en DB_local: " + connLocal.ToString());
          
            
            connLocal = download;



            List<formaPagoData> formasPagoLocal = BusinessComponents.formaPago.getAll(false, connLocal);
            if (formasPagoLocal != null)
                Log("Se encontraron " + formasPagoLocal.Count + " en DB_local: " + connLocal.ToString());
          
            foreach (formaPagoData fp in formasPagoRemoto)
            {

                if ((formasPagoLocal.Find(delegate(formaPagoData f)

                { return f.id == fp.id; }
                    )) != null)
                {
                    aux = BusinessComponents.formaPago.UpdateFp(fp, connLocal);
                    Log("Actualizando " + fp.id.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
                }
                else
                {
                    aux = BusinessComponents.formaPago.newFp(fp, connLocal);
                    Log("Insertando " + fp.id.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
                }


            }
        }
    }
}
