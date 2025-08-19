using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessComponents;
using DTO.BusinessEntities;
using Quartz;

namespace Sync.Jobs
{
    class tranfieroPreciosJob : LoggeableJob, IJob
    {
        public tranfieroPreciosJob()
            : base("tranfieroPreciosJob")
        {
        }

        public static IJobDetail Get(bool descargar = false, int CountEjecuccionDiferencia = 99999)
        {
            string category = descargar.ToString();
            return JobBuilder.Create<tranfieroPreciosJob>()
                .WithIdentity("precios_" + category)
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





            List<listaPrecioProductoTalleData> lista = BusinessComponents.listaPrecioProductoTalle.getAll(connLocal);
            if (lista != null)
                Log("Se encontraron " + lista.Count + " en la conexion local:" + connLocal.ToString());


            connLocal = download;


            foreach (listaPrecioProductoTalleData precio in lista)
            {
                if (BusinessComponents.listaPrecioProductoTalle.estaras(precio, connLocal))
                {
                    aux = BusinessComponents.listaPrecioProductoTalle.updatePrecio(precio, connLocal);
                    
                    Log("Actualizando " + precio.idProductoTalle.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
                }
                else
                {
                    aux = BusinessComponents.listaPrecioProductoTalle.insertPrecio(precio, connLocal);
                    Log("Insertando " + precio.idProductoTalle.ToString() + " en local:" + connLocal.ToString() + "; La operacion resulto: " + aux.ToString());
                }

            }



        }
    }
}
