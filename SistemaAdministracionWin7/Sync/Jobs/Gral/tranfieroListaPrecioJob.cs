using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessComponents;
using DTO.BusinessEntities;
using Quartz;


namespace Sync.Jobs
{
    class tranfieroListaPrecioJob : LoggeableJob, IJob
    {
        public tranfieroListaPrecioJob()
            : base("tranfieroListaPrecioJob")
        {
        }

        public static IJobDetail Get(bool descargar = false, int CountEjecuccionDiferencia = 99999)
        {
            string category = descargar.ToString();
            return JobBuilder.Create<tranfieroListaPrecioJob>()
                .WithIdentity("listaprecio_" + category)
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




            List<listaPrecioData> listaPrecioDataRemoto = BusinessComponents.listaPrecio.getAll(false,connLocal);
            if (listaPrecioDataRemoto != null)
                Log("Se encontraron " + listaPrecioDataRemoto.Count +" en DB_local: "+ connLocal.ToString());
          
            connLocal = download;

            List<listaPrecioData> listaPrecioDataLocal2 = BusinessComponents.listaPrecio.getAll(false,connLocal);
            if (listaPrecioDataLocal2 != null)
                Log("Se encontraron " + listaPrecioDataLocal2.Count + " en DB_local: " + connLocal.ToString());
          

            foreach (listaPrecioData lp in listaPrecioDataRemoto)
            {
                if ((
                    (listaPrecioDataLocal2.Find(delegate(listaPrecioData l)
                    { return l.id == lp.id; }))) == null)
                {
                    aux = BusinessComponents.listaPrecio.Insert(lp, connLocal);
                   Log("Insertando " + lp.id.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
                }
                else
                {
                    aux = BusinessComponents.listaPrecio.Update(lp, connLocal);
                    Log("Actualizando " + lp.id.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
                }
            }
        }
    }
}
